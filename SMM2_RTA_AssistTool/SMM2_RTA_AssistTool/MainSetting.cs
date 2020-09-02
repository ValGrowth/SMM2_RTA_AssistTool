using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool {
	class MainSetting {

		private static MainSetting mInstance;
		private MainSetting () {} // Private Constructor
		public static MainSetting Instance {
			get {
				if ( mInstance == null ) mInstance = new MainSetting();
				return mInstance;
			}
		}

		public const int PREVIEW_VIDEO = 1;
		public const int PLAY_AUDIO = 0;

		private int mPreviewVideo = PREVIEW_VIDEO;
		private int mPlayAudio = PLAY_AUDIO;

		public int PreviewVideo {
			get { return mPreviewVideo; }
			set { mPreviewVideo = value; }
		}
		public int PlayAudio {
			get { return mPlayAudio; }
			set { mPlayAudio = value; }
		}

		public void initialize() {
 			if (!File.Exists(@"DeathCounterMain.ini")) {
				using(StreamWriter w = new StreamWriter(@"DeathCounterMain.ini", false, Encoding.UTF8))
				{
					w.WriteLine("PREVIEW_VIDEO=" + PREVIEW_VIDEO);
					w.WriteLine("PLAY_AUDIO=" + PLAY_AUDIO);
				}
			} else {
				List<string> settingList = new List<string>();
				using(StreamReader r = new StreamReader(@"DeathCounterMain.ini", Encoding.UTF8))
				{
					while (r.Peek() >= 0) {
						settingList.Add(r.ReadLine());
					}
				}
				loadSetting(settingList);
			}
		}

		private void loadSetting(List<string> settingList) {
			foreach (string line in settingList) {
				int equalIndex = line.IndexOf("=");
				if (equalIndex < 0) {
					continue;
				}
				if (line.Length <= equalIndex + 1) {
					continue;
				}
				string item = line.Substring(0, equalIndex);
				string param = line.Substring(equalIndex + 1);

				switch (item) {
					case "PREVIEW_VIDEO":
						int previewVideo = 0;
						if (int.TryParse(param, out previewVideo)) {
							mPreviewVideo = previewVideo;
						}
						break;
					case "PLAY_AUDIO":
						int playAudio = 0;
						if (int.TryParse(param, out playAudio)) {
							mPlayAudio = playAudio;
						}
						break;
				}
			}
		}

		public void saveToFile() {
				using(StreamWriter w = new StreamWriter(@"DeathCounterMain.ini", false, Encoding.UTF8))
				{
					w.WriteLine("PREVIEW_VIDEO=" + mPreviewVideo);
					w.WriteLine("PLAY_AUDIO=" + mPlayAudio);
				}
		}

	}
}
