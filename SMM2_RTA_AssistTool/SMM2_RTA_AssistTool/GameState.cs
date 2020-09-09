using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SMM2_RTA_AssistTool
{
	class GameState
	{

		public enum STATE { 
			CASTLE,
			LEVEL_PLAYING,
		};

		private int mAllSerialIdx;
		private string mLevelNo;
		private int mSerialIdx;
		private int mCurReward;
		private int mCurCoinNum;
		private int mCumulativeCoinNum;
		private STATE mState;

		public int GetAllSerialIdx() { return mAllSerialIdx; }
		public int GetCurReward() { return mCurReward; }
		public int GetCurCoinNum() { return mCurCoinNum; }
		public int GetCumulativeCoinNum() { return mCumulativeCoinNum; }
		public int GetSerialIdx() { return mSerialIdx; }
		public STATE GetState() { return mState; }

		public GameState()
		{
			Reset();
		}

		public void Reset()
		{
			mAllSerialIdx = 0;
			mLevelNo = "";
			mSerialIdx = 0;
			mCurReward = 0;
			mCurCoinNum = 0;
			mCumulativeCoinNum = 0;
			mState = STATE.CASTLE;
		}

		public void SetFromCSVLine(List<string> list)
		{
			Reset();

			if (!int.TryParse(list[0], out mAllSerialIdx))
			{
				mAllSerialIdx = -1;
			}
			mLevelNo = list[1];
			if (!int.TryParse(list[2], out mSerialIdx))
			{
				mSerialIdx = -1;
			}
			if (!int.TryParse(list[5], out mCurReward))
			{
				mCurReward = -1;
			}
			if (!int.TryParse(list[7], out mCurCoinNum))
			{
				mCurCoinNum = -1;
			}
			if (!int.TryParse(list[9], out mCumulativeCoinNum))
			{
				mCumulativeCoinNum = -1;
			}
			if (mCurReward == -1)
			{
				mState = STATE.LEVEL_PLAYING;
			} else
			{
				mState = STATE.CASTLE;
			}
		}

		private string GetLevelCode()
		{
			return LevelData.GetLevelCode(mLevelNo, mSerialIdx);
		}

		public void UpdateLevel(string levelNo, string lastLevelNo, int lastSerialIdx, int cumulativeCoinNum)
		{
			++mAllSerialIdx;
			mLevelNo = levelNo;
			if (levelNo == lastLevelNo)
			{
				mSerialIdx = lastSerialIdx + 1;
			} else
			{
				mSerialIdx = 1;
			}
			mCurReward = -1; // コースプレイ中は-1
			mCurCoinNum = -1; // コースプレイ中は-1
			mCumulativeCoinNum = cumulativeCoinNum;
			mState = STATE.LEVEL_PLAYING;
		}

		public void UpdateCoin(int reward, int inLevelCoin)
		{
			mCurReward = reward;
			mCurCoinNum = inLevelCoin;
			mCumulativeCoinNum += reward + inLevelCoin;
			mState = STATE.CASTLE;
		}

		public int GetCurCoinDiff()
		{
			if (mCurCoinNum < 0)
			{
				return -1;
			}
			LevelData levelData = GetLevelData();
			if (levelData == null)
			{
				return -1;
			}
			int coinDiff = mCurCoinNum - levelData.mInLevelCoin;
			return coinDiff;
		}

		public int GetCumulativeCoinDiff()
		{
			if (mCurCoinNum < 0)
			{
				return -1;
			}
			LevelData levelData = GetLevelData();
			if (levelData == null)
			{
				return -1;
			}
			int coinDiff = mCumulativeCoinNum - levelData.mCumulativeCoin;
			return coinDiff;
		}

		public LevelData GetLevelData()
		{
			if (mLevelNo == "")
			{
				return null;
			}
			string curLevelCode = GetLevelCode();
			if (curLevelCode == "")
			{
				return null;
			}
			LevelData levelData = LevelManager.Instance.GetLevelData(curLevelCode);
			return levelData;
		}

	}
}
