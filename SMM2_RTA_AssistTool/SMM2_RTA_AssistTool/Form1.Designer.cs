namespace SMM2_RTA_AssistTool {
	partial class Form1 {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckBox_PreviewVideo = new System.Windows.Forms.CheckBox();
            this.CheckBox_PlayAudio = new System.Windows.Forms.CheckBox();
            this.CheckBox_Pause = new System.Windows.Forms.CheckBox();
            this.TextBox_CurCoin = new System.Windows.Forms.Label();
            this.TextBox_CurCoinDiff = new System.Windows.Forms.Label();
            this.TextBox_LeftBracket1 = new System.Windows.Forms.Label();
            this.TextBox_RightBracket1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(7, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 308);
            this.panel1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.configToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem1});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.ファイルToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeToolStripMenuItem1_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.configToolStripMenuItem.Text = "Setting";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.optionsToolStripMenuItem.Text = "Devices";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // CheckBox_PreviewVideo
            // 
            this.CheckBox_PreviewVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_PreviewVideo.AutoSize = true;
            this.CheckBox_PreviewVideo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CheckBox_PreviewVideo.Location = new System.Drawing.Point(9, 366);
            this.CheckBox_PreviewVideo.Name = "CheckBox_PreviewVideo";
            this.CheckBox_PreviewVideo.Size = new System.Drawing.Size(117, 20);
            this.CheckBox_PreviewVideo.TabIndex = 8;
            this.CheckBox_PreviewVideo.Text = "PreviewVideo";
            this.CheckBox_PreviewVideo.UseVisualStyleBackColor = true;
            this.CheckBox_PreviewVideo.CheckedChanged += new System.EventHandler(this.CheckBox_PreviewVideo_CheckedChanged);
            // 
            // CheckBox_PlayAudio
            // 
            this.CheckBox_PlayAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_PlayAudio.AutoSize = true;
            this.CheckBox_PlayAudio.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CheckBox_PlayAudio.Location = new System.Drawing.Point(9, 392);
            this.CheckBox_PlayAudio.Name = "CheckBox_PlayAudio";
            this.CheckBox_PlayAudio.Size = new System.Drawing.Size(93, 20);
            this.CheckBox_PlayAudio.TabIndex = 9;
            this.CheckBox_PlayAudio.Text = "PlayAudio";
            this.CheckBox_PlayAudio.UseVisualStyleBackColor = true;
            this.CheckBox_PlayAudio.Visible = false;
            this.CheckBox_PlayAudio.CheckedChanged += new System.EventHandler(this.CheckBox_PlayAudio_CheckedChanged);
            // 
            // CheckBox_Pause
            // 
            this.CheckBox_Pause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox_Pause.AutoSize = true;
            this.CheckBox_Pause.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CheckBox_Pause.Location = new System.Drawing.Point(469, 392);
            this.CheckBox_Pause.Name = "CheckBox_Pause";
            this.CheckBox_Pause.Size = new System.Drawing.Size(68, 20);
            this.CheckBox_Pause.TabIndex = 10;
            this.CheckBox_Pause.Text = "Pause";
            this.CheckBox_Pause.UseVisualStyleBackColor = true;
            // 
            // TextBox_CurCoin
            // 
            this.TextBox_CurCoin.AutoSize = true;
            this.TextBox_CurCoin.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox_CurCoin.Location = new System.Drawing.Point(164, 366);
            this.TextBox_CurCoin.Name = "TextBox_CurCoin";
            this.TextBox_CurCoin.Size = new System.Drawing.Size(46, 16);
            this.TextBox_CurCoin.TabIndex = 11;
            this.TextBox_CurCoin.Text = "label1";
            // 
            // TextBox_CurCoinDiff
            // 
            this.TextBox_CurCoinDiff.AutoSize = true;
            this.TextBox_CurCoinDiff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox_CurCoinDiff.Location = new System.Drawing.Point(235, 366);
            this.TextBox_CurCoinDiff.Name = "TextBox_CurCoinDiff";
            this.TextBox_CurCoinDiff.Size = new System.Drawing.Size(46, 16);
            this.TextBox_CurCoinDiff.TabIndex = 12;
            this.TextBox_CurCoinDiff.Text = "label1";
            this.TextBox_CurCoinDiff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBox_LeftBracket1
            // 
            this.TextBox_LeftBracket1.AutoSize = true;
            this.TextBox_LeftBracket1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox_LeftBracket1.Location = new System.Drawing.Point(216, 366);
            this.TextBox_LeftBracket1.Name = "TextBox_LeftBracket1";
            this.TextBox_LeftBracket1.Size = new System.Drawing.Size(13, 16);
            this.TextBox_LeftBracket1.TabIndex = 13;
            this.TextBox_LeftBracket1.Text = "(";
            this.TextBox_LeftBracket1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBox_RightBracket1
            // 
            this.TextBox_RightBracket1.AutoSize = true;
            this.TextBox_RightBracket1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox_RightBracket1.Location = new System.Drawing.Point(287, 366);
            this.TextBox_RightBracket1.Name = "TextBox_RightBracket1";
            this.TextBox_RightBracket1.Size = new System.Drawing.Size(13, 16);
            this.TextBox_RightBracket1.TabIndex = 14;
            this.TextBox_RightBracket1.Text = ")";
            this.TextBox_RightBracket1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 419);
            this.Controls.Add(this.TextBox_RightBracket1);
            this.Controls.Add(this.TextBox_LeftBracket1);
            this.Controls.Add(this.TextBox_CurCoinDiff);
            this.Controls.Add(this.TextBox_CurCoin);
            this.Controls.Add(this.CheckBox_Pause);
            this.Controls.Add(this.CheckBox_PlayAudio);
            this.Controls.Add(this.CheckBox_PreviewVideo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(317, 249);
            this.Name = "Form1";
            this.Text = "SMM2_RTA_AssistTool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed_1);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
		private System.Windows.Forms.CheckBox CheckBox_PreviewVideo;
		private System.Windows.Forms.CheckBox CheckBox_PlayAudio;
		private System.Windows.Forms.CheckBox CheckBox_Pause;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Label TextBox_CurCoin;
        private System.Windows.Forms.Label TextBox_CurCoinDiff;
        private System.Windows.Forms.Label TextBox_LeftBracket1;
        private System.Windows.Forms.Label TextBox_RightBracket1;
    }
}

