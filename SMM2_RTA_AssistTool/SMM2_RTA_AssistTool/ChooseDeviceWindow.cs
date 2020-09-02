using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM2_RTA_AssistTool {
	public partial class ChooseDeviceWindow : Form {

		private string mDeviceName = "";
		private GraphFactory.DEVICE_TYPE mDeviceType;

		public string DeviceName {
			get { return mDeviceName; }
			set { mDeviceName = value; }
		}

		public ChooseDeviceWindow(string[] deviceNames, GraphFactory.DEVICE_TYPE deviceType) {
			InitializeComponent();

			mDeviceType = deviceType;
			
			if (deviceType == GraphFactory.DEVICE_TYPE.VIDEO) {
				Label_ChooseDevice.Text = "ChooseVideoDevice";
			}
			if (deviceType == GraphFactory.DEVICE_TYPE.AUDIO) {
				Label_ChooseDevice.Text = "ChooseAudioDevice";
			}
			
			ComboBox_DeviceName.Items.AddRange(deviceNames);
			ComboBox_DeviceName.SelectedIndex = 0;
		}

		private void Button_OK_Click(object sender, EventArgs e) {
			if (ComboBox_DeviceName.SelectedItem != null) {
				DeviceName = ComboBox_DeviceName.SelectedItem.ToString();

				OptionSetting option = OptionSetting.Instance;
				if (option.EnableDefaultDevices == 1) {
					if (mDeviceType == GraphFactory.DEVICE_TYPE.VIDEO) {
						option.DefaultVideoDevice = DeviceName;
					}
					if (mDeviceType == GraphFactory.DEVICE_TYPE.AUDIO) {
						option.DefaultAudioDevice = DeviceName;
					}
					option.saveToFile();
				}
			}
			Close();
		}
	}
}
