using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
	class VideoAnalyzer
	{

		Bitmap[] mNumberImagesOriginal = new Bitmap[10];
		FastBitmap[] mNumberImages = new FastBitmap[10];
		List<List<List<Color>>> mNumRGBs = new List<List<List<Color>>>();
		int mNumMaxHeight;
		int mNumMaxWidth;

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
				mNumMaxHeight = Math.Max(mNumMaxHeight, mNumberImages[i].Height);
				mNumMaxWidth = Math.Max(mNumMaxWidth, mNumberImages[i].Width);
			}
		}

		// コースNo.を取得する。
		public string DetectLevelNo(FastBitmap gameImage)
		{
			// タイトル文字の場所だけ比較すればよい
			int xmin = 800;
			int xmax = 1100;
			int ymin = 150;
			int ymax = 200;

			foreach (LevelData level in LevelManager.Instance.GetAllLevels())
			{
				FastBitmap levelImage = level.mTitleImage;
				if (levelImage.Height != gameImage.Height || levelImage.Width != gameImage.Width)
				{
					continue;
				} else
				{
					int count = 0;
					int totalPixel = (ymax - ymin) * (xmax - xmin) / 4;
					int allowPixel = (int)(totalPixel * (1.0 - 0.9));
					bool same = true;
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
							if (count > allowPixel)
							{
								same = false;
							}
						}
					}
					if (!same)
					{
						continue;
					}
					return level.mLevelNo;
				}
			}
			return "";
		}

		// 獲得コイン枚数を取得する
		public int DetectCoinNum(FastBitmap gameImage)
		{
			// コイン枚数表示部付近だけ調べれば良い
			// 報酬コイン
			int rxmin = 900;
			int rxmax = 1100;
			int rymin = 600;
			int rymax = 650;

			bool[] rFoundTable = new bool[1920];
			IDictionary<int, int> rFoundNumbers = new SortedDictionary<int, int>();
			for (int ay = rymin; ay < rymax; ++ay)
			{
				for (int ax = rxmin; ax < rxmax; ++ax)
				{
					if (rFoundTable[ax])
					{
						continue;
					}
					int num = TestNumber(gameImage, ax, ay);
					if (num != -1)
					{
						rFoundNumbers[ax] = num;
						for (int dx = -10; dx <= 10; ++dx)
						{
							rFoundTable[ax + dx] = true;
						}
					}
				}
			}

			int reward = 0;
			foreach (KeyValuePair<int, int> data in rFoundNumbers)
			{
				reward *= 10;
				reward += data.Value;
			}

			// 報酬ありの場合のコース内コイン
			int ixmin1 = 900;
			int ixmax1 = 1100;
			int iymin1 = 375;
			int iymax1 = 425;

			bool[] iFoundTable1 = new bool[1920];
			IDictionary<int, int> iFoundNumbers1 = new SortedDictionary<int, int>();
			for (int ay = iymin1; ay < iymax1; ++ay)
			{
				for (int ax = ixmin1; ax < ixmax1; ++ax)
				{
					if (iFoundTable1[ax])
					{
						continue;
					}
					int num = TestNumber(gameImage, ax, ay);
					if (num != -1)
					{
						iFoundNumbers1[ax] = num;
						for (int dx = -10; dx <= 10; ++dx)
						{
							iFoundTable1[ax + dx] = true;
						}
					}
				}
			}

			int coinInLevel = 0;
			foreach (KeyValuePair<int, int> data in iFoundNumbers1)
			{
				coinInLevel *= 10;
				coinInLevel += data.Value;
			}

			bool[] iFoundTable2 = new bool[1920];
			IDictionary<int, int> iFoundNumbers2 = new SortedDictionary<int, int>();
			if (iFoundNumbers1.Count == 0)
			{
				// 報酬なしの場合のコース内コイン
				int ixmin2 = 900;
				int ixmax2 = 1100;
				int iymin2 = 525;
				int iymax2 = 575;

				for (int ay = iymin2; ay < iymax2; ++ay)
				{
					for (int ax = ixmin2; ax < ixmax2; ++ax)
					{
						if (iFoundTable2[ax])
						{
							continue;
						}
						int num = TestNumber(gameImage, ax, ay);
						if (num != -1)
						{
							iFoundNumbers2[ax] = num;
							for (int dx = -10; dx <= 10; ++dx)
							{
								iFoundTable2[ax + dx] = true;
							}
						}
					}
				}

				coinInLevel = 0;
				foreach (KeyValuePair<int, int> data in iFoundNumbers2)
				{
					coinInLevel *= 10;
					coinInLevel += data.Value;
				}
			}

			if (rFoundNumbers.Count == 0 && iFoundNumbers1.Count == 0 && iFoundNumbers2.Count == 0)
			{
				return -1;
			}

			return reward + coinInLevel;
		}

		private int TestNumber(FastBitmap image, int ax, int ay)
		{
			int totalPixel = mNumMaxHeight * mNumMaxWidth / 4;
			int allowPixel = (int)(totalPixel * (1.0 - 0.9));
			int[] counts = new int[10];
			List<int> numbers = new List<int>();
			for (int i = 0; i < 10; ++i)
			{
				counts[i] = 0;
				numbers.Add(i);
			}
			for (int dy = 0; dy < mNumMaxHeight; dy += 2)
			{
				for (int dx = 0; dx < mNumMaxWidth; dx += 2)
				{
					Color ac = image.GetPixel(ax + dx, ay + dy);
					for (int i = numbers.Count - 1; i >= 0; --i)
					{
						int num = numbers[i];
						Color bc = mNumRGBs[num][dy][dx];
						int diff = 0;
						diff += Math.Abs(ac.R - bc.R);
						diff += Math.Abs(ac.G - bc.G);
						diff += Math.Abs(ac.B - bc.B);
						if (diff >= 150)
						{
							++counts[num];
						}
						if (counts[num] > allowPixel)
						{
							numbers.RemoveAt(i);
							if (numbers.Count == 0)
							{
								return -1;
							}
						}
					}
				}
			}
			numbers.Sort((a, b) => counts[b] - counts[a]);
			return numbers[0];
		}

	}
}
