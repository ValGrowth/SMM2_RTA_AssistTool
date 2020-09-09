using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
    class LevelData
    {
        public string mLevelNo;
        public int mSerialIdx;
        public string mLevelCode;
        public string mJpTitle;
        public string mEnTitle;
        public int mReward; // 報酬コイン枚数
        public int mInLevelCoin; // コース内コイン枚数
        public int mTotalCoin; // Reward + InLevelCoin
        public int mCumulativeCoin; // 累計コイン枚数
        public Bitmap mImage;
        public FastBitmap mTitleImage; // タイトルが表示される画面の画像
        public string mCastleList; // 城建設リスト（名称とコマンド）
        public string mLevelSelectCommand; // コース選択コマンド

        public LevelData()
        {
            mLevelNo = "";
            mSerialIdx = 0;
            mLevelCode = "";
            mJpTitle = "";
            mEnTitle = "";
            mReward = 0;
            mInLevelCoin = 0;
            mTotalCoin = 0;
            mCumulativeCoin = 0;
            mImage = null;
            mTitleImage = null;
            mCastleList = "";
            mLevelSelectCommand = "";
        }

        public LevelData(List<string> list)
        {
            mLevelNo = list[0];
            if (!int.TryParse(list[1], out mSerialIdx))
            {
                mSerialIdx = -1;
            }
            mLevelCode = GetLevelCode(mLevelNo, mSerialIdx);
            mJpTitle = list[2];
            mEnTitle = list[3];
            if (!int.TryParse(list[4], out mReward))
            {
                mReward = 0;
            }
            if (!int.TryParse(list[5], out mInLevelCoin))
            {
                mInLevelCoin = 0;
            }
            mTotalCoin = mReward + mInLevelCoin;
            if (!int.TryParse(list[6], out mCumulativeCoin))
            {
                mCumulativeCoin = 0;
            }
            mCastleList = list[7];
            mLevelSelectCommand = list[8];
            string imagePath = "./Images/Levels/" + list[9];
            mImage = new Bitmap(imagePath);
            mTitleImage = new FastBitmap(mImage);
        }

        public static string GetLevelCode(string levelNo, int serialIdx)
        {
            if (levelNo == "" || serialIdx <= 0)
            {
                return "";
            }
            if (serialIdx == 1)
            {
                return levelNo;
            }
            else if (serialIdx >= 2)
            {
                return levelNo + "-" + serialIdx;
            }
            return "";
        }

    }
}
