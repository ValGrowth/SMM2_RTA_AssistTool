using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
    class VideoGameState
    {
        public string mLevelNo;
        public int mCoinNum;

        public VideoGameState()
        {
            Reset();
        }

        public void Reset()
        {
            mLevelNo = "";
            mCoinNum = 0;
        }

    }
}
