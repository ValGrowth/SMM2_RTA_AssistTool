using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM2_RTA_AssistTool
{
	class VideoAnalyzer
	{
		private const int BIN_THRESH = 128;
		private const int X_SPAN = 20;
		private const int SCAN_INTERVAL = 2;
		private const int SCAN_INTERVAL2 = SCAN_INTERVAL * SCAN_INTERVAL;

		private Bitmap[] mNumberImagesOriginal = new Bitmap[10];
		private FastBitmap[] mNumberImages = new FastBitmap[10];
		private List<List<List<Color>>> mNumRGBs = new List<List<List<Color>>>();
		private List<List<List<bool>>> mNumBinFlgs = new List<List<List<bool>>>();
		private int[] mNumTotalPixel = new int[10];
		private int[] mNumAllowPixel = new int[10];
		private int mNumMaxHeight;
		private int mNumMaxWidth;

		private long mLastCheckPassLevelNo = 0;
		private long mLastCheckPassCoinNum = 0;
		private long mCheckPassCountLevelNo = 0;
		private long mCheckPassCountCoinNum = 0;

		public VideoAnalyzer()
		{
			mNumMaxHeight = 0;
			mNumMaxWidth = 0;
			for (int i = 0; i < 10; ++i)
			{
				string path = "./Images/Numbers/" + i + ".png";
				mNumberImagesOriginal[i] = new Bitmap(path);
				mNumberImages[i] = new FastBitmap(mNumberImagesOriginal[i]);
				mNumRGBs.Add(new List<List<Color>>());
				for (int y = 0; y < mNumberImages[i].Height; ++y)
				{
					mNumRGBs[i].Add(new List<Color>());
					for (int x = 0; x < mNumberImages[i].Width; ++x)
					{
						mNumRGBs[i][y].Add(mNumberImages[i].GetPixel(x, y));
					}
				}
				mNumBinFlgs.Add(new List<List<bool>>());
				for (int y = 0; y < mNumberImages[i].Height; ++y)
				{
					mNumBinFlgs[i].Add(new List<bool>());
					for (int x = 0; x < mNumberImages[i].Width; ++x)
					{
						Color pixel = mNumberImages[i].GetPixel(x, y);
						bool flg = CalcBinFlg(pixel);
						mNumBinFlgs[i][y].Add(flg);
					}
				}
				mNumMaxHeight = Math.Max(mNumMaxHeight, mNumberImages[i].Height);
				mNumMaxWidth = Math.Max(mNumMaxWidth, mNumberImages[i].Width);
				mNumTotalPixel[i] = mNumberImages[i].Height * mNumberImages[i].Width / SCAN_INTERVAL2;
				mNumAllowPixel[i] = (int)(mNumTotalPixel[i] * (1.0 - 0.8));
			}
		}

		// コースNo.を取得する。
		public string DetectLevelNo(FastBitmap gameImage)
		{
			int chxmin = 150;
			int chxmax = 500;
			int chymin = 100;
			int chymax = 200;
			for (int y = chymin; y < chymax; y += 5)
			{
				for (int x = chxmin; x < chxmax; x += 5)
				{
					Color color = gameImage.GetPixel(x, y);
					if (Math.Abs(color.R - 255) + Math.Abs(color.G - 205) + Math.Abs(color.B - 0) > 150)
					{
						return "";
					}
				}
			}

			long nowTime = Timer.GetUnixTime(DateTime.Now);
			if (mCheckPassCountLevelNo == 1)
			{
				if (nowTime - mLastCheckPassLevelNo <= 1000)
				{
					return "";
				}
				mCheckPassCountLevelNo = 0;
			} else
			{
				mCheckPassCountLevelNo = 1;
				mLastCheckPassLevelNo = nowTime;
				return "";
			}

			int ch2xmin = 100;
			int ch2xmax = 600;
			int ch2ymin = 400;
			int ch2ymax = 700;
			for (int y = ch2ymin; y < ch2ymax; y += 5)
			{
				for (int x = ch2xmin; x < ch2xmax; x += 5)
				{
					Color color = gameImage.GetPixel(x, y);
					if (Math.Abs(color.R - 0) + Math.Abs(color.G - 0) + Math.Abs(color.B - 0) > 150)
					{
						return "";
					}
				}
			}

			// タイトル文字の場所だけ比較すればよい
			int xmin = 800;
			int xmax = 1100;
			int ymin = 150;
			int ymax = 200;

			List<int> diffs = new List<int>();
			int levelIdx = 0;
			foreach (LevelData level in LevelManager.Instance.GetAllLevels())
			{
				diffs.Add(0);
				FastBitmap levelImage = level.mTitleImage;
				if (levelImage.Height != gameImage.Height || levelImage.Width != gameImage.Width)
				{
					continue;
				} else
				{
					int count = 0;
					for (int y = ymin; y < ymax; y += 2)
					{
						for (int x = xmin; x < xmax; x += 2)
						{
							int diff = 0;
							Color ac = levelImage.GetPixel(x, y);
							Color bc = gameImage.GetPixel(x, y);
							diff += Math.Abs(ac.R - bc.R);
							diff += Math.Abs(ac.G - bc.G);
							diff += Math.Abs(ac.B - bc.B);
							if (diff >= 150)
							{
								++count;
							}
						}
					}
					diffs[levelIdx] = count;
				}
				++levelIdx;
			}
			string ret = "";
			int minCount = 99999999;
			levelIdx = 0;
			foreach (LevelData level in LevelManager.Instance.GetAllLevels())
			{
				if (diffs[levelIdx] < minCount)
				{
					minCount = diffs[levelIdx];
					ret = level.mLevelNo;
				}
				++levelIdx;
			}
			return ret;
		}

		// 獲得コイン枚数を取得する
		public Tuple<int, int> DetectCoinNum(FastBitmap gameImage)
		{
			if (gameImage.Height != 1080 || gameImage.Width != 1920)
			{
				return new Tuple<int, int>(-1, -1);
			}
			int chxmin = 500;
			int chxmax = 550;
			int chymin = 500;
			int chymax = 600;
			for (int y = chymin; y < chymax; y += 5)
			{
				for (int x = chxmin; x < chxmax; x += 5)
				{
					Color color = gameImage.GetPixel(x, y);
					if (Math.Abs(color.R - 255) + Math.Abs(color.G - 205) + Math.Abs(color.B - 0) > 150)
					{
						return new Tuple<int, int>(-1, -1);
					}
				}
			}

			long nowTime = Timer.GetUnixTime(DateTime.Now);
			if (mCheckPassCountCoinNum == 1)
			{
				if (nowTime - mLastCheckPassCoinNum <= 1000)
				{
					return new Tuple<int, int>(-1, -1);
				}
				mCheckPassCountCoinNum = 0;
			}
			else
			{
				mCheckPassCountCoinNum = 1;
				mLastCheckPassCoinNum = nowTime;
				return new Tuple<int, int>(-1, -1);
			}

			// 先にRewardがあるかどうかを検出する

			bool hasReward = true;
			int chrxmin = 600;
			int chrxmax = 1200;
			int chrymin = 250;
			int chrymax = 300;
			for (int y = chrymin; y < chrymax; y += 5)
			{
				for (int x = chrxmin; x < chrxmax; x += 5)
				{
					Color color = gameImage.GetPixel(x, y);
					if (Math.Abs(color.R - 255) + Math.Abs(color.G - 205) + Math.Abs(color.B - 0) > 150)
					{
						hasReward = false;
						break;
					}
				}
				if (!hasReward)
				{
					break;
				}
			}
			Console.WriteLine("HasReward: " + hasReward.ToString());

			int reward = 0;
			int coinInLevel = 0;

			bool rewardFound = false;
			bool coinInLevelFound = false;

			if (hasReward)
			{
				// コイン枚数表示部付近だけ調べれば良い
				// 報酬コイン
				int rxmin = 900;
				int rxmax = 1100;
				int rymin = 600;
				int rymax = 650;

				reward = IdentifyNumbers(gameImage, rxmin, rxmax, rymin, rymax);
				if (reward >= 0)
				{
					rewardFound = true;
				} else
				{
					reward = 0;
				}

				// 報酬ありの場合のコース内コイン
				int ixmin1 = 900;
				int ixmax1 = 1100;
				int iymin1 = 375;
				int iymax1 = 425;

				coinInLevel = IdentifyNumbers(gameImage, ixmin1, ixmax1, iymin1, iymax1);
				if (coinInLevel >= 0)
				{
					coinInLevelFound = true;
				}
				else
				{
					coinInLevel = 0;
				}

			}
			else
			{
				// 報酬なしの場合のコース内コイン
				int ixmin2 = 900;
				int ixmax2 = 1100;
				int iymin2 = 525;
				int iymax2 = 575;

				coinInLevel = IdentifyNumbers(gameImage, ixmin2, ixmax2, iymin2, iymax2);
				if (coinInLevel >= 0)
				{
					coinInLevelFound = true;
				}
				else
				{
					coinInLevel = 0;
				}

			}

			Console.WriteLine("Reward: " + reward);
			Console.WriteLine("Coin in Level: " + coinInLevel);

			if (!rewardFound && !coinInLevelFound)
			{
				return new Tuple<int, int>(-1, -1);
			}

			return new Tuple<int, int>(reward, coinInLevel);
		}

		// 数字列を識別する
		private int IdentifyNumbers(FastBitmap gameImage, int xmin, int xmax, int ymin, int ymax)
		{
			List<Tuple<int, int, double>> tempFoundNumbers = new List<Tuple<int, int, double>>(); // x座標、数字、一致度
			for (int ax = xmin; ax < xmax; ++ax)
			{
				for (int ay = ymin; ay < ymax; ++ay)
				{
					Tuple<int, double> numInfo = TestNumber(gameImage, ax, ay);
					if (numInfo.Item1 != -1)
					{
						tempFoundNumbers.Add(new Tuple<int, int, double>(ax, numInfo.Item1, numInfo.Item2));
					}
				}
			}

			IDictionary<int, int> foundNumbers = new SortedDictionary<int, int>();
			bool[] xCheck = new bool[xmax - xmin];
			if (tempFoundNumbers.Count > 0)
			{
				// 一致度が高い順に並び替え
				tempFoundNumbers.Sort((a, b) => a.Item3 < b.Item3 ? 1 : (a.Item3 > b.Item3 ? -1 : 0));

				// 一致度が高い順に数字を選ぶ
				int idx = 0;
				while (true)
				{
					while (idx < tempFoundNumbers.Count && xCheck[tempFoundNumbers[idx].Item1 - xmin])
					{
						++idx;
					}
					if (idx >= tempFoundNumbers.Count)
					{
						break;
					}
					int x = tempFoundNumbers[idx].Item1;
					int num = tempFoundNumbers[idx].Item2;
					foundNumbers[x] = num;
					++idx;

					for (int dx = -X_SPAN; dx < X_SPAN; ++dx)
					{
						if (x + dx < xmin || x + dx >= xmax)
						{
							continue;
						}
						xCheck[x + dx - xmin] = true;
					}
				}
			}

			if (foundNumbers.Count == 0)
			{
				return -1;
			}

			int coinNum = 0;
			foreach (KeyValuePair<int, int> data in foundNumbers)
			{
				coinNum *= 10;
				coinNum += data.Value;
			}

			return coinNum;
		}

		// 最も一致度の高い数字を検出する
		// ２値画像で比較するバージョン
		private Tuple<int, double> TestNumber(FastBitmap image, int ax, int ay)
		{
			int[] counts = new int[10];
			List<int> numbers = new List<int>();
			for (int i = 0; i < 10; ++i)
			{
				counts[i] = 0;
				numbers.Add(i);
			}
			for (int dy = 0; dy < mNumMaxHeight; dy += SCAN_INTERVAL)
			{
				for (int dx = 0; dx < mNumMaxWidth; dx += SCAN_INTERVAL)
				{
					Color ac = image.GetPixel(ax + dx, ay + dy);
					bool acFlg = CalcBinFlg(ac);
					for (int i = numbers.Count - 1; i >= 0; --i)
					{
						int num = numbers[i];
						if (dy >= mNumberImages[num].Height || dx >= mNumberImages[num].Width)
						{
							continue;
						}
						bool bcFlg = mNumBinFlgs[num][dy][dx];
						if (acFlg != bcFlg)
						{
							++counts[num];
						}
						if (counts[num] > mNumAllowPixel[num])
						{
							numbers.RemoveAt(i);
							if (numbers.Count == 0)
							{
								return new Tuple<int, double>(-1, -1);
							}
						}
					}
				}
			}
			double maxSimilarity = -1.0;
			int bestNum = -1;
			foreach (int num in numbers)
			{
				double similarity = 1.0 - ((double)counts[num] / mNumTotalPixel[num]);
				if (similarity > maxSimilarity)
				{
					maxSimilarity = similarity;
					bestNum = num;
				}
			}
			return new Tuple<int, double>(bestNum, maxSimilarity);
		}

		// RGB画素値を使って比較するバージョン
		//private int TestNumber(FastBitmap image, int ax, int ay)
		//{
		//	int totalPixel = mNumMaxHeight * mNumMaxWidth / SCAN_INTERVAL2;
		//	int allowPixel = (int)(totalPixel * (1.0 - 0.9));
		//	int[] counts = new int[10];
		//	List<int> numbers = new List<int>();
		//	for (int i = 0; i < 10; ++i)
		//	{
		//		counts[i] = 0;
		//		numbers.Add(i);
		//	}
		//	for (int dy = 0; dy < mNumMaxHeight; dy += SCAN_INTERVAL)
		//	{
		//		for (int dx = 0; dx < mNumMaxWidth; dx += SCAN_INTERVAL)
		//		{
		//			Color ac = image.GetPixel(ax + dx, ay + dy);
		//			for (int i = numbers.Count - 1; i >= 0; --i)
		//			{
		//				int num = numbers[i];
		//				Color bc = mNumRGBs[num][dy][dx];
		//				int diff = 0;
		//				diff += Math.Abs(ac.R - bc.R);
		//				diff += Math.Abs(ac.G - bc.G);
		//				diff += Math.Abs(ac.B - bc.B);
		//				if (diff >= 150)
		//				{
		//					++counts[num];
		//				}
		//				if (counts[num] > allowPixel)
		//				{
		//					numbers.RemoveAt(i);
		//					if (numbers.Count == 0)
		//					{
		//						return -1;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	numbers.Sort((a, b) => counts[b] - counts[a]);
		//	return numbers[0];
		//}

		private bool CalcBinFlg(Color color)
		{
			return color.R + color.G + color.B >= BIN_THRESH * 3;
		}
	}
}
