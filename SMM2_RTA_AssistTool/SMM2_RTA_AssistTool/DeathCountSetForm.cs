using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SMM2_RTA_AssistTool.SMMMessageBox;

namespace SMM2_RTA_AssistTool {
	public partial class DeathCountSetForm : Form {
		public DeathCountSetForm() {
			InitializeComponent();
		}

		private void DeathCountSetForm_Load(object sender, EventArgs e) {
			TextBox_DeathCount.Text = DeathCountManager.Instance.DeathCount.ToString();
		}

		private void SaveButton_Click(object sender, EventArgs e) {

			string message = checkInput();
			if (message != "") {
				SMMMessageBox.Show(message, SMMMessageBoxIcon.Information);
				return;
			}

			DeathCountManager.Instance.DeathCount = int.Parse(TextBox_DeathCount.Text);
			DeathCountManager.Instance.saveToFile();

			this.Close();
		}

		private string checkInput() {
			int tempInt = 0;
			bool ret = false;
			ret = int.TryParse(TextBox_DeathCount.Text, out tempInt);
			if (!ret) {
				return "DeathCount is invalid.";
			}

			if (int.Parse(TextBox_DeathCount.Text) < 0) {
				return "DeathCount must be 0 or greater.";
			}
			return "";
		}

		private void CancelButton_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void TextBox_DeathCount_KeyPress(object sender, KeyPressEventArgs e) {
			//押されたキーが 0～9でない場合は、イベントをキャンセルする
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b') {
				e.Handled = true;
			}
		}

		private void TextBox_DeathCount_Leave(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(((TextBox)sender).Text)) {
				TextBox_DeathCount.Text = "0";
			}
		}

	}
}
