using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool {
	class LevelManager {

		private static LevelManager mInstance;
		private LevelManager() // Private Constructor
		{
			Init();
		}
		public static LevelManager Instance {
			get {
				if ( mInstance == null ) mInstance = new LevelManager();
				return mInstance;
			}
		}

		private IDictionary<string, LevelData> mLevelDataList = new Dictionary<string, LevelData>();

		private void Init()
		{
			List<List<string>> csvData = CsvReader.ReadCsv("./LevelData/LevelData.csv", true);

			foreach (List<string> line in csvData)
			{
				LevelData levelData = new LevelData(line);
				mLevelDataList.Add(levelData.mLevelCode, levelData);
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
