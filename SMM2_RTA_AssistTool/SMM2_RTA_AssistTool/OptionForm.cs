using DirectShowLib;
using System;
using System.Windows.Forms;

namespace SMM2_RTA_AssistTool
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();

            // 現環境のデバイスリスト取得
            string[] videoDeviceNameList = GraphFactory.GetDeviceNameList(FilterCategory.VideoInputDevice);
            string[] audioDeviceNameList = GraphFactory.GetDeviceNameList(FilterCategory.AudioInputDevice);

            // デバイスリストをコンボボックスにセット
            ComboBox_VideoDeviceName.Items.AddRange(videoDeviceNameList);
            ComboBox_AudioDeviceName.Items.AddRange(audioDeviceNameList);

            // 設定ファイルの内容を画面に反映
            OptionSetting option = OptionSetting.Instance;
            CheckBox_EnableDefaultDevices.Checked = option.EnableDefaultDevices == 1 ? true : false;
            ComboBox_VideoDeviceName.Text = option.DefaultVideoDevice;
            ComboBox_AudioDeviceName.Text = option.DefaultAudioDevice;
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {

            // 画面の内容を設定ファイルに保存
            OptionSetting option = OptionSetting.Instance;
            option.EnableDefaultDevices = CheckBox_EnableDefaultDevices.Checked ? 1 : 0;
            option.DefaultVideoDevice = ComboBox_VideoDeviceName.Text.ToString();
            option.DefaultAudioDevice = ComboBox_AudioDeviceName.Text.ToString();

            option.saveToFile();

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
