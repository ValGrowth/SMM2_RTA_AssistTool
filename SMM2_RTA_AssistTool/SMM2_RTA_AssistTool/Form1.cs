using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
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

		GameState mGameState = new GameState();
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
			mGameState.Reset();
			mGameStateHistory.Clear();
			mGameStateHistory.Add(mGameState);
		}

		private bool directShowInitialize()
		{

			mVideoChecker = new VideoChecker(this);
			//mAudioChecker = new AudioChecker(this);


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

					// コースのプレイ開始を検出した
					if (mGameState.GetState() == GameState.STATE.CASTLE)
					{
						if (videoGameState.mLevelNo != "")
						{
							mGameState.UpdateLevel(videoGameState.mLevelNo);
							UpdateDisplay();
						}
					}
					else if (mGameState.GetState() == GameState.STATE.LEVEL_PLAYING)
					{
						// 所持コイン枚数が変化した
						if (videoGameState.mCoinNum >= 0)
						{
							mGameState.UpdateCoin(videoGameState.mCoinNum);
							mGameStateHistory.Add(mGameState);
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

			const int DISP_NUM = 2;
			for (int i = 0; i < DISP_NUM; ++i)
			{
				int index = mGameStateHistory.Count - (DISP_NUM - i);
				if (index < 0)
				{
					continue;
				}
				GameState state = mGameStateHistory[index];
				LevelData levelData = state.GetLevelData();
				string levelName = levelData.mJpTitle; // コース名
				int gotCoin = state.GetCurCoinNum(); // 実際に獲得したコイン
				int curCoinDiff = state.GetCurCoinDiff(); // チャートとの差
				int gotCumulativeCoin = state.GetCumulativeCoinNum(); // 実際に獲得したコイン（累計）
				int cumulativeCoinDiff = state.GetCumulativeCoinDiff(); // チャートとの差（累計）

				// TODO: コース名とコイン枚数を表示する
				// 獲得コイン(+-差)　累計コイン(+-差)

				string curCoinSign = (curCoinDiff >= 0) ? "+" : "-";
				TextBox_CurCoin.Text = gotCoin.ToString();
				TextBox_CurCoinDiff.Text = curCoinSign + curCoinDiff;
				if (curCoinDiff >= 0)
				{
					TextBox_CurCoinDiff.ForeColor = Color.LimeGreen;
				} else
				{
					TextBox_CurCoinDiff.ForeColor = Color.Red;
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
	}
}
