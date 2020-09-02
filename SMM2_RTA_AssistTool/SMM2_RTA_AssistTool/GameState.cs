using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
    class GameState
    {
        private string mLevelNo;
        private int mSerialIdx;
        private int mCoinNum;

        private IDictionary<string, LevelData> mLevelDataList = new Dictionary<string, LevelData>();

        public GameState()
        {
            Init();
            Reset();
        }

        private void Init()
        {
            // TODO: 別のシングルトンクラスに分ける
            List<List<string>> csvData = CsvReader.ReadCsv("LevelData.csv", true);

            foreach (List<string> line in csvData)
            {
                LevelData levelData = new LevelData(line);
                mLevelDataList.Add(levelData.mLevelCode, levelData);
            }

        }

        public void Reset()
        {
            mLevelNo = "";
            mSerialIdx = 0;
            mCoinNum = 0;
        }

        private string getLevelCode()
        {
            if (mLevelNo == "" || mSerialIdx <= 0)
            {
                return "";
            }
            return mLevelNo + "_" + mSerialIdx;
        }

        public void updateLevel(string levelNo)
        {
            if (levelNo == mLevelNo)
            {
                mSerialIdx += 1;
            } else
            {
                mLevelNo = levelNo;
                mSerialIdx = 1;
            }
            mCoinNum = -1; // コースプレイ中は-1
        }

        public void updateCoin(int coin)
        {
            mCoinNum = coin;
        }

        public int getCurCoinDiff()
        {
            if (mLevelNo == "")
            {
                return -1;
            }
            if (mCoinNum < 0)
            {
                return -1;
            }
            string curLevelCode = getLevelCode();
            if (curLevelCode == "")
            {
                return -1;
            }
            int coinDiff = mCoinNum - mLevelDataList[curLevelCode].mTotalCoin;
            return coinDiff;
        }

    }
}
