using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SMM2_RTA_AssistTool.SMMMessageBox;

namespace SMM2_RTA_AssistTool
{
	public partial class Form1 : Form
	{

		VideoChecker mVideoChecker = null;
		//AudioChecker mAudioChecker = null;

		List<GameState> mGameStateHistory = new List<GameState>();

		private IGraphBuilder graphBuilder; //基本的なフィルタグラフマネージャ
		private ICaptureGraphBuilder2 captureGraphBuilder;//ビデオキャプチャ＆編集用のメソッドを備えたキャプチャグラフビルダ

		private IVideoWindow videoWindow; //オーナーウィンドウの位置やサイズなどの設定用のインタフェース．
		private IMediaControl mediaControl;//データのストリーミングの移動、ポーズ、停止などの処理用のインタフェース．
		private IMediaEventEx mediaEvent; //DirectShowイベント処理用のインタフェース

		int result; //エラー判定用フラグ

		private const int WS_CHILD = 0x40000000;
		private const int WS_CLIPCHILDREN = 0x02000000;

		public const int WM_GRAPHNOTIFY = 0x00008001;//DirectShowイベントの発生を表すWindows メッセージ.


		public Form1()
		{
			InitializeComponent();
		}

		private async void Form1_Load(object sender, EventArgs e)
		{

			DeathSetting.Instance.initialize();
			MainSetting.Instance.initialize();
			OptionSetting.Instance.initialize();
			bool ret = directShowInitialize();
			if (!ret)
			{
				Close();
				return;
			}

			ResetRun();
			UpdateDisplay();

			while (true)
			{
				await update();
			}
		}

		public void ResetRun()
		{
			mGameStateHistory.Clear();
			UpdateDisplay();
		}

		private bool directShowInitialize()
		{

			mVideoChecker = new VideoChecker(this);
			//mAudioChecker = new AudioChecker(this);

			//mVideoChecker.Test();

			// 3. フィルタグラフマネージャを作成し，各種操作を行うためのインタフェースを取得する．

			//graphBuilderを作成．
			graphBuilder = GraphFactory.MakeGraphBuilder();

			//各種インタフェースを取得．
			mediaControl = (IMediaControl)graphBuilder;
			videoWindow = (IVideoWindow)graphBuilder;
			mediaEvent = (IMediaEventEx)graphBuilder;


			// 4. キャプチャグラフビルダと，サンプルグラバフィルタ（個々のビデオデータを取得するフィルタ）を作成する．

			//キャプチャグラフビルダ(captureGraphBuilder)を作成．
			captureGraphBuilder = GraphFactory.MakeCaptureGraphBuilder();

			// 5. 基本となるフィルタグラフマネージャ(graphBuilder)にキャプチャグラフビルダと各フィルタを追加する．

			//captureGraphBuilder（キャプチャグラフビルダ）をgraphBuilder（フィルタグラフマネージャ）に追加．
			result = captureGraphBuilder.SetFiltergraph(graphBuilder);
			if (result < 0) Marshal.ThrowExceptionForHR(result);


			// 1. デバイスを取得し、2. キャプチャデバイスをソースフィルタに対応づける．
			// 6. 各フィルタの接続を行い，入力画像のキャプチャとプレビューの準備を行う．
			bool ret = false;
			ret = mVideoChecker.AddVideoFilters(graphBuilder, captureGraphBuilder);
			if (!ret)
			{
				SMMMessageBox.Show("エラー：映像入力デバイスが見つかりませんでした。\nプログラムを終了します。Error: Any video devices are not found.\n Closing this application.", SMMMessageBoxIcon.Error);
				return false;
			}

			//ret = mAudioChecker.AddAudioFilters(graphBuilder, captureGraphBuilder);
			//if (!ret) {
			//	SMMMessageBox.Show("エラー：音声入力デバイスが見つかりませんでした。\nプログラムを終了します。Error: Any audio devices are not found.\n Closing this application.", SMMMessageBoxIcon.Error);
			//	return false;
			//}

			// 7. プレビュー映像（レンダラフィルタの出力）の出力場所を設定する.

			//プレビュー映像を表示するパネルを指定．
			result = videoWindow.put_Owner(this.panel1.Handle);
			if (result < 0) Marshal.ThrowExceptionForHR(result);
			//ビデオ表示領域のスタイルを指定．
			result = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
			if (result < 0) Marshal.ThrowExceptionForHR(result);
			//ビデオパネルのサイズを変更する．
			Rectangle rect = this.panel1.ClientRectangle;
			videoWindow.SetWindowPosition(0, 0, rect.Right, rect.Bottom);

			//レンダラフィルタの出力を可視化する．
			result = videoWindow.put_Visible(OABool.True);
			if (result < 0) Marshal.ThrowExceptionForHR(result);



			// 8. DirectShowイベントを，Windowsメッセージを通して通知するための設定を行う．

			// mediaEvent(DirectShowイベント)をWM_GRAPHNOTIFY(Windowsメッセージ)に対応付ける． 
			result = mediaEvent.SetNotifyWindow(this.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
			if (result < 0) Marshal.ThrowExceptionForHR(result);



			// 9. プレビューを開始する．

			result = mediaControl.Run();
			if (result < 0) Marshal.ThrowExceptionForHR(result);

			return true;
		}

		private void Form1_FormClosed_1(object sender, FormClosedEventArgs e)
		{
			//// 閉じるときの処理
			CloseInterfaces();
		}

		//COMやインタフェースの解放を行う
		public void CloseInterfaces()
		{
			try
			{
				if (mediaControl != null)
				{
					mediaControl.Stop();
					Thread.Sleep(100);
					mediaControl = null;
				}

				if (mediaEvent != null)
				{
					mediaEvent.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);
					mediaEvent = null;
				}

				if (videoWindow != null)
				{
					videoWindow.put_Visible(OABool.False);
					videoWindow.put_Owner(IntPtr.Zero);
					videoWindow = null;
				}

				if (captureGraphBuilder != null)
				{
					Marshal.ReleaseComObject(captureGraphBuilder);
					captureGraphBuilder = null;
				}

				if (graphBuilder != null)
				{
					Marshal.ReleaseComObject(graphBuilder);
					graphBuilder = null;
				}

				mVideoChecker.CloseInterfaces();
				//mAudioChecker.CloseInterfaces();

			}
			catch (Exception e)
			{
				SMMMessageBox.Show("エラー：インタフェースの終了に失敗しました。Error: An error ocouured when closing video interface. " + e.ToString(), SMMMessageBoxIcon.Error);
			}
		}

		private Task update()
		{

			// サンプルグラバーのコールバックメソッド を有効にし，サンプリングを開始する．
			if (mVideoChecker != null)
			{
				mVideoChecker.update();
			}
			//if (mAudioChecker != null) {
			//	mAudioChecker.update();
			//}

			if (mVideoChecker != null/* && mAudioChecker != null*/)
			{

				if (!CheckBox_Pause.Checked)
				{
					VideoGameState videoGameState = mVideoChecker.GetVideoGameState();

					GameState curState = mGameStateHistory.Count == 0 ? null : mGameStateHistory[mGameStateHistory.Count - 1];

					// コースのプレイ開始を検出した
					if (curState == null || curState.GetState() == GameState.STATE.CASTLE)
					{
						if (videoGameState.mLevelNo != "")
						{
							mGameStateHistory.Add(new GameState());
							string lastLevelNo = "";
							int lastSerialIdx = -1;
							int lastCumulativeCoinNum = 0;
							if (mGameStateHistory.Count >= 2)
							{
								GameState prev = mGameStateHistory[mGameStateHistory.Count - 2];
								lastLevelNo = prev.GetLevelData().mLevelNo;
								lastSerialIdx = prev.GetSerialIdx();
								lastCumulativeCoinNum = prev.GetCumulativeCoinNum();
							}
							mGameStateHistory[mGameStateHistory.Count - 1].UpdateLevel(
								videoGameState.mLevelNo, lastLevelNo, lastSerialIdx, lastCumulativeCoinNum);
							UpdateDisplay();
						}
					}
					else if (curState != null && curState.GetState() == GameState.STATE.LEVEL_PLAYING)
					{
						// 所持コイン枚数が変化した
						if (videoGameState.mReward >= 0 || videoGameState.mInLevelCoinNum >= 0)
						{
							curState.UpdateCoin(videoGameState.mReward, videoGameState.mInLevelCoinNum);
							UpdateDisplay();
						}
					}
				}
			}

			return Task.Run(() => Thread.Sleep(100));
		}

		private void UpdateDisplay()
		{
			CheckBox_PreviewVideo.Checked = MainSetting.Instance.PreviewVideo == 1;
			CheckBox_PlayAudio.Checked = MainSetting.Instance.PlayAudio == 1;

			Dictionary<string, Label> labels = new Dictionary<string, Label>();
			labels.Add(Label_Idx1.Name, Label_Idx1);
			labels.Add(Label_LevelCode1.Name, Label_LevelCode1);
			labels.Add(Label_LevelTitle1.Name, Label_LevelTitle1);
			labels.Add(Label_ChartCoin1.Name, Label_ChartCoin1);
			labels.Add(Label_CurCoin1.Name, Label_CurCoin1);
			labels.Add(Label_CurDiff1.Name, Label_CurDiff1);
			labels.Add(Label_TotalCoin1.Name, Label_TotalCoin1);
			labels.Add(Label_TotalDiff1.Name, Label_TotalDiff1);

			labels.Add(Label_Idx2.Name, Label_Idx2);
			labels.Add(Label_LevelCode2.Name, Label_LevelCode2);
			labels.Add(Label_LevelTitle2.Name, Label_LevelTitle2);
			labels.Add(Label_ChartCoin2.Name, Label_ChartCoin2);
			labels.Add(Label_CurCoin2.Name, Label_CurCoin2);
			labels.Add(Label_CurDiff2.Name, Label_CurDiff2);
			labels.Add(Label_TotalCoin2.Name, Label_TotalCoin2);
			labels.Add(Label_TotalDiff2.Name, Label_TotalDiff2);

			const int DISP_NUM = 2;
			for (int i = 0; i < DISP_NUM; ++i)
			{
				int index = mGameStateHistory.Count - DISP_NUM + i;
				bool found = false;
				if (index >= 0)
				{
					GameState state = mGameStateHistory[index];
					LevelData levelData = state.GetLevelData();
					if (levelData != null)
					{
						found = true;

						string levelCode = levelData.mLevelCode;
						string levelName = levelData.mJpTitle; // コース名
						int chartCoin = levelData.mInLevelCoin;
						labels["Label_Idx" + (i + 1)].Text = state.GetAllSerialIdx().ToString();
						labels["Label_LevelCode" + (i + 1)].Text = levelCode;
						labels["Label_LevelTitle" + (i + 1)].Text = levelName;
						labels["Label_ChartCoin" + (i + 1)].Text = chartCoin.ToString();

						int curCoin = state.GetCurCoinNum(); // 実際に獲得したコイン
						if (curCoin >= 0)
						{
							int curCoinDiff = state.GetCurCoinDiff(); // チャートとの差
							int gotCumulativeCoin = state.GetCumulativeCoinNum(); // 実際に獲得したコイン（累計）
							int cumulativeCoinDiff = state.GetCumulativeCoinDiff(); // チャートとの差（累計）

							string curCoinSign = (curCoinDiff >= 0) ? "+" : "";
							string cumulativeCoinSign = (cumulativeCoinDiff >= 0) ? "+" : "";
							labels["Label_CurCoin" + (i + 1)].Text = curCoin.ToString();
							labels["Label_CurDiff" + (i + 1)].Text = curCoinSign + curCoinDiff.ToString();
							labels["Label_TotalCoin" + (i + 1)].Text = gotCumulativeCoin.ToString();
							labels["Label_TotalDiff" + (i + 1)].Text = cumulativeCoinSign + cumulativeCoinDiff.ToString();

							if (curCoinDiff >= 0)
							{
								labels["Label_CurDiff" + (i + 1)].ForeColor = Color.Green;
							}
							else
							{
								labels["Label_CurDiff" + (i + 1)].ForeColor = Color.Red;
							}
							if (cumulativeCoinDiff >= 0)
							{
								labels["Label_TotalDiff" + (i + 1)].ForeColor = Color.Green;
							}
							else
							{
								labels["Label_TotalDiff" + (i + 1)].ForeColor = Color.Red;
							}
						}
						else
						{
							labels["Label_CurCoin" + (i + 1)].Text = "-";
							labels["Label_CurDiff" + (i + 1)].Text = "-";
							labels["Label_TotalCoin" + (i + 1)].Text = "-";
							labels["Label_TotalDiff" + (i + 1)].Text = "-";

							labels["Label_CurDiff" + (i + 1)].ForeColor = Color.Black;
							labels["Label_TotalDiff" + (i + 1)].ForeColor = Color.Black;
						}
					}
				}
				if (!found)
				{
					labels["Label_Idx" + (i + 1)].Text = "-";
					labels["Label_LevelCode" + (i + 1)].Text = "-";
					labels["Label_LevelTitle" + (i + 1)].Text = "-";
					labels["Label_ChartCoin" + (i + 1)].Text = "-";
					labels["Label_CurCoin" + (i + 1)].Text = "-";
					labels["Label_CurDiff" + (i + 1)].Text = "-";
					labels["Label_TotalCoin" + (i + 1)].Text = "-";
					labels["Label_TotalDiff" + (i + 1)].Text = "-";

					labels["Label_CurDiff" + (i + 1)].ForeColor = Color.Black;
					labels["Label_TotalDiff" + (i + 1)].ForeColor = Color.Black;
				}
			}

			// 補助コマンド表示
			{
				LevelData levelData = null;
				if (mGameStateHistory.Count > 0)
				{
					GameState state = mGameStateHistory[mGameStateHistory.Count - 1];
					levelData = state.GetLevelData();
				}
				if (levelData != null)
				{
					Label_CastleList.Text = levelData.mCastleList;
					Label_LevelSelectCommand.Text = levelData.mLevelSelectCommand;
				}
				else
				{
					Label_CastleList.Text = "";
					Label_LevelSelectCommand.Text = "";
				}
			}
		}

		private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void resetConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingForm settingForm = new SettingForm();
			settingForm.ShowDialog(this);
		}

		private void CheckBox_PreviewVideo_CheckedChanged(object sender, EventArgs e)
		{
			MainSetting.Instance.PreviewVideo = CheckBox_PreviewVideo.Checked ? 1 : 0;
			MainSetting.Instance.saveToFile();

			//レンダラフィルタの出力の可視化設定を変更．
			if (MainSetting.Instance.PreviewVideo == 1)
			{
				result = videoWindow.put_Visible(OABool.True);
			}
			else
			{
				result = videoWindow.put_Visible(OABool.False);
			}
			if (result < 0) Marshal.ThrowExceptionForHR(result);
		}

		private void CheckBox_PlayAudio_CheckedChanged(object sender, EventArgs e)
		{
			MainSetting.Instance.PlayAudio = CheckBox_PlayAudio.Checked ? 1 : 0;
			MainSetting.Instance.saveToFile();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OptionForm form = new OptionForm();
			form.ShowDialog(this);
		}

		private void Button_Reset_Click(object sender, EventArgs e)
		{
			ResetRun();
		}

		private void Button_CSVOutput_Click(object sender, EventArgs e)
		{
			string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + "SMM2AnyCoins.csv";
			string[] header = new string[] { "Idx", "No.", "SerialIdx", "JpTitle", "EnTitle", "Reward", "Target", "Cur", "CurDiff", "Total", "TotalDiff" };
			const int COLUMN_NUM = 11;

			// ファイルを開く
			using (StreamWriter file = new StreamWriter(fileName, false, Encoding.GetEncoding("Shift_JIS")))
			{
				string headerStr = "";
				for (int i = 0; i < COLUMN_NUM; ++i)
				{
					if (i > 0)
					{
						headerStr += ",";
					}
					headerStr += "\"" + header[i] + "\"";
				}
				file.WriteLine(headerStr);

				int count = 0;
				foreach (GameState g in mGameStateHistory)
				{
					string str = "";
					if (count > 0)
					{
						str += ",";
					}
					str += "\"" + g.GetAllSerialIdx() + "\"";
					str += ",\"" + g.GetLevelData().mLevelNo + "\"";
					str += ",\"" + g.GetSerialIdx() + "\"";
					str += ",\"" + g.GetLevelData().mJpTitle + "\"";
					str += ",\"" + g.GetLevelData().mEnTitle + "\"";
					str += ",\"" + g.GetCurReward() + "\"";
					str += ",\"" + g.GetLevelData().mInLevelCoin + "\"";
					str += ",\"" + g.GetCurCoinNum() + "\"";
					str += ",\"" + g.GetCurCoinDiff() + "\"";
					str += ",\"" + g.GetCumulativeCoinNum() + "\"";
					str += ",\"" + g.GetCumulativeCoinDiff() + "\"";
					file.WriteLine(str);
					++count;
				}
				Console.WriteLine("CSV Saved. (" + count + " Lines.)");
				Label_CSVMessage.Text = "Saved. " + fileName;
			}
		}

		private void Button_CSVLoad_Click(object sender, EventArgs e)
		{
			// ファイル選択ウィンドウを開く

			OpenFileDialog ofd = new OpenFileDialog();

			// 設定する
			ofd.FileName = "*.csv"; // はじめに「ファイル名」で表示される文字列を指定する
			ofd.InitialDirectory = "";
			ofd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
			ofd.FilterIndex = 1; // [ファイルの種類]ではじめに選択されるものを指定する（1番目の「CSVファイル」が選択されているようにする）
			ofd.Title = "開くファイルを選択してください"; // タイトルを設定する
			ofd.RestoreDirectory = true; // ダイアログボックスを閉じる前に現在のディレクトリを記憶するようにする

			//ダイアログを表示する
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				//OKボタンがクリックされたとき
				Console.WriteLine(ofd.FileName);

				List<List<string>> csvData = CsvReader.ReadCsv(ofd.FileName, true, true);

				ResetRun();

				foreach (List<string> line in csvData)
				{
					GameState state = new GameState();
					state.SetFromCSVLine(line);
					mGameStateHistory.Add(state);
				}

				UpdateDisplay();

				Label_CSVMessage.Text = "Loaded. " + ofd.FileName;
			}

		}
	}
}
