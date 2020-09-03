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

        Bitmap[] mNumberImages = new Bitmap[10];

        public VideoAnalyzer()
        {
            for (int i = 0; i < 10; ++i)
            {
                string path = "./Image/Number/" + i + ".png";
                mNumberImages[i] = new Bitmap(path);
            }
        }

        // コースNo.を取得する。
        public string DetectLevelNo(Bitmap bitmap)
        {
            foreach (LevelData level in LevelManager.Instance.GetAllLevels())
            {
                Bitmap levelImage = level.mTitleImage;
                bool same = true;
                if (levelImage.Height != bitmap.Height || levelImage.Width != bitmap.Width)
                {
                    same = false;
                } else
                {
                    // TODO: タイトル文字の場所だけ比較すればよい
                    for (int y = 0; y < levelImage.Height; ++y)
                    {
                        for (int x = 0; x < levelImage.Width; ++x)
                        {
                            if (levelImage.GetPixel(x, y) != bitmap.GetPixel(x, y))
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
        public int DetectCoinNum(Bitmap bitmap)
        {
            // TODO:
            int reward = 0;
            int coinInLevel = 0;
            // ３桁の場合
            // ２桁の場合
            // １桁の場合
            return reward + coinInLevel;

            return -1;
        }

    }
}
