using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
    class GameState
    {
        public string mLevelCode;
        public int mCoinNum;

        private List<LevelData> mLevelDataList;

        public GameState()
        {
            Init();
            Reset();
        }

        private void Init()
        {
            List<List<string>> csvData = CsvReader.ReadCsv("CourseData.csv", true);
            // TODO: エラーチェック

            foreach (List<string> line in csvData)
            {
                mLevelDataList.Add(new LevelData(line));
            }

        }

        public void Reset()
        {
            mLevelCode = "";
            mCoinNum = 0;
        }

    }
}
