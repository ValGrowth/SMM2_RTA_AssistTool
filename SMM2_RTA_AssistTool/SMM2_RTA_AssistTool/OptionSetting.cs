using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool {
	class OptionSetting {
		private static OptionSetting mInstance;
		private OptionSetting () {} // Private Constructor
		public static OptionSetting Instance {
			get {
				if ( mInstance == null ) mInstance = new OptionSetting();
				return mInstance;
			}
		}

		public const int ENABLE_DEFAULT_DEVICES = 1;
		public const string DEFAULT_VIDEO_DEVICE = "";
		public const string DEFAULT_AUDIO_DEVICE = "";

		private int mEnableDefaultDevices = ENABLE_DEFAULT_DEVICES;
		private string mDefaultVideoDevice = DEFAULT_VIDEO_DEVICE;
		private string mDefaultAudioDevice = DEFAULT_AUDIO_DEVICE;

		public int EnableDefaultDevices {
			get { return mEnableDefaultDevices; }
			set { mEnableDefaultDevices = value; }
		}
		public string DefaultVideoDevice {
			get { return mDefaultVideoDevice; }
			set { mDefaultVideoDevice = value; }
		}
		public string DefaultAudioDevice {
			get { return mDefaultAudioDevice; }
			set { mDefaultAudioDevice = value; }
		}

		public void initialize() {
 			if (!File.Exists(@"DeathCounterOption.ini")) {
				using(StreamWriter w = new StreamWriter(@"DeathCounterOption.ini", false, Encoding.UTF8))
				{
					w.WriteLine("ENABLE_DEFAULT_DEVICES=" + ENABLE_DEFAULT_DEVICES);
					w.WriteLine("DEFAULT_VIDEO_DEVICE=" + DEFAULT_VIDEO_DEVICE);
					w.WriteLine("DEFAULT_AUDIO_DEVICE=" + DEFAULT_AUDIO_DEVICE);
				}
			} else {
				List<string> settingList = new List<string>();
				using(StreamReader r = new StreamReader(@"DeathCounterOption.ini", Encoding.UTF8))
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
					case "ENABLE_DEFAULT_DEVICES":
						int enableDefaultDevices = 0;
						if (int.TryParse(param, out enableDefaultDevices)) {
							mEnableDefaultDevices = enableDefaultDevices;
						}
						break;
					case "DEFAULT_VIDEO_DEVICE":
						mDefaultVideoDevice = param;
						break;
					case "DEFAULT_AUDIO_DEVICE":
						mDefaultAudioDevice = param;
						break;
				}
			}
		}

		public void saveToFile() {
				using(StreamWriter w = new StreamWriter(@"DeathCounterOption.ini", false, Encoding.UTF8))
				{
					w.WriteLine("ENABLE_DEFAULT_DEVICES=" + mEnableDefaultDevices);
					w.WriteLine("DEFAULT_VIDEO_DEVICE=" + mDefaultVideoDevice);
					w.WriteLine("DEFAULT_AUDIO_DEVICE=" + mDefaultAudioDevice);
				}
		}
	}
}
