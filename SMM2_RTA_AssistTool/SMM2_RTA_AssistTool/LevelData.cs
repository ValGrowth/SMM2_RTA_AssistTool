using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
    class LevelData
    {
        public string mLevelCode;
        public int mCoinNum;
        public string mJpTitle;
        public string mEnTitle;

        public LevelData()
        {
            mLevelCode = "";
            mCoinNum = 0;
            mJpTitle = "";
            mEnTitle = "";
        }

        public LevelData(List<string> list)
        {
            mLevelCode = list[0];
            if (!int.TryParse(list[1], out mCoinNum))
            {
                mCoinNum = -1;
            }
            mJpTitle = list[2];
            mEnTitle = list[3];
        }
    }
}
