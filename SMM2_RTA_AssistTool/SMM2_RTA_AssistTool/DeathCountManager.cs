using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool {
	class DeathCountManager {

		private static DeathCountManager mInstance;
		private DeathCountManager () {} // Private Constructor
		public static DeathCountManager Instance {
			get {
				if ( mInstance == null ) mInstance = new DeathCountManager();
				return mInstance;
			}
		}

		public const int DEFAULT_DEATH_COUNT = 0;

		private int mDeathCount = DEFAULT_DEATH_COUNT;

		public int DeathCount {
			get { return mDeathCount; }
			set { mDeathCount = value; }
		}

		public void initialize() {
 			if (!File.Exists(@"DeathCount.txt")) {
				using(StreamWriter w = new StreamWriter(@"DeathCount.txt", false, Encoding.UTF8))
				{
					w.WriteLine(mDeathCount);
				}
			} else {
				using(StreamReader r = new StreamReader(@"DeathCount.txt", Encoding.UTF8))
				{
					loadData(r.ReadToEnd());
				}
			}
		}

		private void loadData(string data) {
			int deathCount = 0;
			if (int.TryParse(data, out deathCount)) {
				mDeathCount = deathCount;
			}
		}

		public void saveToFile() {
			using(StreamWriter w = new StreamWriter(@"DeathCount.txt", false, Encoding.UTF8))
			{
				w.WriteLine(mDeathCount);
			}
		}

		public void addDeathCount(int num) {
			mDeathCount += num;
		}

	}
}
