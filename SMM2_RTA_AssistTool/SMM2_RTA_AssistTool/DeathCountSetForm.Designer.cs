namespace SMM2_RTA_AssistTool {
	partial class DeathCountSetForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.TextBox_DeathCount = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Button_Save = new System.Windows.Forms.Button();
			this.Button_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TextBox_DeathCount
			// 
			this.TextBox_DeathCount.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.TextBox_DeathCount.Location = new System.Drawing.Point(51, 73);
			this.TextBox_DeathCount.MaxLength = 9;
			this.TextBox_DeathCount.Name = "TextBox_DeathCount";
			this.TextBox_DeathCount.Size = new System.Drawing.Size(153, 39);
			this.TextBox_DeathCount.TabIndex = 0;
			this.TextBox_DeathCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TextBox_DeathCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_DeathCount_KeyPress);
			this.TextBox_DeathCount.Leave += new System.EventHandler(this.TextBox_DeathCount_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(21, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(209, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Type Death Count Value";
			// 
			// Button_Save
			// 
			this.Button_Save.Location = new System.Drawing.Point(93, 148);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Button_Save.TabIndex = 2;
			this.Button_Save.Text = "Save";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// Button_Cancel
			// 
			this.Button_Cancel.Location = new System.Drawing.Point(174, 148);
			this.Button_Cancel.Name = "Button_Cancel";
			this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Button_Cancel.TabIndex = 3;
			this.Button_Cancel.Text = "Cancel";
			this.Button_Cancel.UseVisualStyleBackColor = true;
			this.Button_Cancel.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// DeathCountSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(261, 183);
			this.Controls.Add(this.Button_Cancel);
			this.Controls.Add(this.Button_Save);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TextBox_DeathCount);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DeathCountSetForm";
			this.Text = "SetDeathCount";
			this.Load += new System.EventHandler(this.DeathCountSetForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBox_DeathCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button Button_Save;
		private System.Windows.Forms.Button Button_Cancel;
	}
}