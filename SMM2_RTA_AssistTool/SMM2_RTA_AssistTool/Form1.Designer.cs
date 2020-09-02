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
            this.TextBox_DeathCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deathCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckBox_PreviewVideo = new System.Windows.Forms.CheckBox();
            this.CheckBox_PlayAudio = new System.Windows.Forms.CheckBox();
            this.CheckBox_Pause = new System.Windows.Forms.CheckBox();
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
            // TextBox_DeathCount
            // 
            this.TextBox_DeathCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_DeathCount.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox_DeathCount.Location = new System.Drawing.Point(377, 356);
            this.TextBox_DeathCount.Name = "TextBox_DeathCount";
            this.TextBox_DeathCount.ReadOnly = true;
            this.TextBox_DeathCount.Size = new System.Drawing.Size(155, 36);
            this.TextBox_DeathCount.TabIndex = 3;
            this.TextBox_DeathCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(282, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "DeathCount";
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
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeToolStripMenuItem1_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetConfigToolStripMenuItem,
            this.deathCountToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.configToolStripMenuItem.Text = "Setting";
            // 
            // resetConfigToolStripMenuItem
            // 
            this.resetConfigToolStripMenuItem.Name = "resetConfigToolStripMenuItem";
            this.resetConfigToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.resetConfigToolStripMenuItem.Text = "Parameters...";
            this.resetConfigToolStripMenuItem.Click += new System.EventHandler(this.resetConfigToolStripMenuItem_Click);
            // 
            // deathCountToolStripMenuItem
            // 
            this.deathCountToolStripMenuItem.Name = "deathCountToolStripMenuItem";
            this.deathCountToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.deathCountToolStripMenuItem.Text = "DeathCount";
            this.deathCountToolStripMenuItem.Click += new System.EventHandler(this.deathCountToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 419);
            this.Controls.Add(this.CheckBox_Pause);
            this.Controls.Add(this.CheckBox_PlayAudio);
            this.Controls.Add(this.CheckBox_PreviewVideo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_DeathCount);
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
		private System.Windows.Forms.TextBox TextBox_DeathCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetConfigToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
		private System.Windows.Forms.CheckBox CheckBox_PreviewVideo;
		private System.Windows.Forms.ToolStripMenuItem deathCountToolStripMenuItem;
		private System.Windows.Forms.CheckBox CheckBox_PlayAudio;
		private System.Windows.Forms.CheckBox CheckBox_Pause;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
	}
}

