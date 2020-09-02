using System;
using System.Collections.Generic;
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
        public int mReward;
        public int mInLevelCoin;
        public int mTotalCoin;


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
        }

        public LevelData(List<string> list)
        {
            mLevelNo = list[0];
            if (!int.TryParse(list[1], out mSerialIdx))
            {
                mSerialIdx = -1;
            }
            mLevelCode = "";
            if (mSerialIdx > 0) {
                mLevelCode = mLevelNo + "_" + mSerialIdx;
            }
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
        }
    }
}
