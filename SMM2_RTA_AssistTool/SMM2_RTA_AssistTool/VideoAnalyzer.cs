using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
	class VideoAnalyzer
	{

		FastBitmap[] mNumberImages = new FastBitmap[10];

		public VideoAnalyzer()
		{
			for (int i = 0; i < 10; ++i)
			{
				string path = "./Images/Numbers/" + i + ".png";
				Bitmap image = new Bitmap(path);
				mNumberImages[i] = new FastBitmap(image);
			}
		}

		// コースNo.を取得する。
		public string DetectLevelNo(FastBitmap gameImage)
		{
			int xmin = 200;
			int xmax = 1600;
			int ymin = 200;
			int ymax = 800;

			foreach (LevelData level in LevelManager.Instance.GetAllLevels())
			{
				FastBitmap levelImage = level.mTitleImage;
				bool same = true;
				if (levelImage.Height != gameImage.Height || levelImage.Width != gameImage.Width)
				{
					same = false;
				} else
				{
					// TODO: タイトル文字の場所だけ比較すればよい
					for (int y = ymin; y < ymax; ++y)
					{
						for (int x = xmin; x < xmax; ++x)
						{
							if (levelImage.GetPixel(x, y) != gameImage.GetPixel(x, y))
							{
								same = false;
								break;
							}
						}
						if (!same)
						{
							break;
						}
					}
				}
				if (same)
				{
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
			int rymax = 625;

			IDictionary<int, int> rFoundNumbers = new SortedDictionary<int, int>();
			for (int ay = rymin; ay < rymax; ++ay)
			{
				for (int ax = rxmin; ax < rxmax; ++ax)
				{
					for (int num = 0; num < 10; ++num)
					{
						bool result = TestNumber(gameImage, ax, ay, num);
						if (result)
						{
							if (rFoundNumbers.ContainsKey(ax))
							{
								throw new Exception("同じ場所で複数の数字が検出されました。");
							}
							rFoundNumbers[ax] = num;
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
			int iymax1 = 400;

			IDictionary<int, int> iFoundNumbers1 = new SortedDictionary<int, int>();
			for (int ay = iymin1; ay < iymax1; ++ay)
			{
				for (int ax = ixmin1; ax < ixmax1; ++ax)
				{
					for (int num = 0; num < 10; ++num)
					{
						bool result = TestNumber(gameImage, ax, ay, num);
						if (result)
						{
							if (iFoundNumbers1.ContainsKey(ax))
							{
								throw new Exception("同じ場所で複数の数字が検出されました。");
							}
							iFoundNumbers1[ax] = num;
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

			IDictionary<int, int> iFoundNumbers2 = new SortedDictionary<int, int>();
			if (iFoundNumbers1.Count == 0)
			{
				// 報酬なしの場合のコース内コイン
				int ixmin2 = 900;
				int ixmax2 = 1100;
				int iymin2 = 525;
				int iymax2 = 550;

				for (int ay = iymin2; ay < iymax2; ++ay)
				{
					for (int ax = ixmin2; ax < ixmax2; ++ax)
					{
						for (int num = 0; num < 10; ++num)
						{
							bool result = TestNumber(gameImage, ax, ay, num);
							if (result)
							{
								if (iFoundNumbers2.ContainsKey(ax))
								{
									throw new Exception("同じ場所で複数の数字が検出されました。");
								}
								iFoundNumbers2[ax] = num;
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

		private bool TestNumber(FastBitmap image, int ax, int ay, int num)
		{
			FastBitmap numImage = mNumberImages[num];
			for (int dy = 0; dy < numImage.Height; ++dy)
			{
				for (int dx = 0; dx < numImage.Width; ++dx)
				{
					if (image.GetPixel(ax + dx, ay + dy) != numImage.GetPixel(dx, dy))
					{
						return false;
					}
				}
			}
			return true;
		}

	}
}
