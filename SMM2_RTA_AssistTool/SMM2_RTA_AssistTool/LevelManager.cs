using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool {
	class LevelManager {

		private static LevelManager mInstance;
		private LevelManager() { } // Private Constructor
		public static LevelManager Instance {
			get {
				if ( mInstance == null ) mInstance = new LevelManager();
				return mInstance;
			}
		}

		public List<List<string>> mOriginalCsvData = new List<List<string>>();
		private IDictionary<string, LevelData> mLevelDataList = new Dictionary<string, LevelData>();

		public void Initialize()
		{
			List<List<string>> csvData = CsvReader.ReadCsv("./LevelData/LevelData.csv", true, true);
			mOriginalCsvData = csvData;
			
			mLevelDataList.Clear();

			int coin = 0;
			int idx = 1;
			LevelData lastLevelData = null;
			foreach (List<string> line in csvData)
			{
				LevelData levelData = new LevelData(line);
				mLevelDataList.Add(levelData.mLevelCode, levelData);
				coin += levelData.mReward + levelData.mInLevelCoin - levelData.mNeededCoin;
				levelData.mAllowedLoss = new Tuple<int, int>(9999, 9999);
				if (coin <= 30)
				{
					foreach (KeyValuePair<string, LevelData> pr in mLevelDataList)
					{
						if (pr.Value.mAllowedLoss.Item1 > idx)
						{
							pr.Value.mAllowedLoss = new Tuple<int, int>(idx, coin);
						}
					}
				}
				if (idx == csvData.Count)
				{
					foreach (KeyValuePair<string, LevelData> pr in mLevelDataList)
					{
						pr.Value.mFinalAllowedLoss = new Tuple<int, int>(idx, coin);
					}
				}
				if (lastLevelData != null)
				{
					lastLevelData.mNextLevel = levelData;
				}
				lastLevelData = levelData;
				++idx;
			}

		}

		public LevelData GetLevelData(string levelCode)
		{
			if (!mLevelDataList.ContainsKey(levelCode))
            {
				return null;
            }
			return mLevelDataList[levelCode];
		}

		public ICollection<LevelData> GetAllLevels()
        {
			return mLevelDataList.Values;
        }
	}
}
