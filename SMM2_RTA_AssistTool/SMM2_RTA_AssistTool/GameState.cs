﻿using System;
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
		private Dictionary<string, int> mSerialIdx = new Dictionary<string, int>();
		private int mCurReward;
		private int mCurCoinNum;
		private int mCumulativeCoinNum;
		private STATE mState;

		public int GetAllSerialIdx() { return mAllSerialIdx; }
		public int GetCurReward() { return mCurReward; }
		public int GetCurCoinNum() { return mCurCoinNum; }
		public int GetCumulativeCoinNum() { return mCumulativeCoinNum; }
		public Dictionary<string, int> GetSerialIdx() { return mSerialIdx; }
		public STATE GetState() { return mState; }

		public GameState()
		{
			Reset();
		}

		public void Reset()
		{
			mAllSerialIdx = 0;
			mLevelNo = "";
			mSerialIdx.Clear();
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
			mSerialIdx.Clear();
			if (!string.IsNullOrEmpty(list[2]))
			{
				string[] strList = list[2].Split(':');
				foreach (string str in strList)
				{
					string[] strList2 = str.Split('_');
					int serialIdx;
					if (!int.TryParse(strList2[1], out serialIdx))
					{
						serialIdx = -1;
					}
					if (serialIdx != -1)
					{
						mSerialIdx.Add(strList2[0], serialIdx);
					}
				}
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

		public string MakeCSVLine()
		{
			string str = "";
			str += "\"" + mAllSerialIdx + "\"";
			str += ",\"" + GetLevelData().mLevelNo + "\"";

			str += ",\"";
			int c = 0;
			foreach (KeyValuePair<string, int> serialIdxPr in mSerialIdx)
			{
				if (c > 0)
				{
					str += ":";
				}
				str += serialIdxPr.Key + "_" + serialIdxPr.Value;
				++c;
			}
			str += "\"";

			str += ",\"" + GetLevelData().mJpTitle + "\"";
			str += ",\"" + GetLevelData().mEnTitle + "\"";
			str += ",\"" + mCurReward + "\"";
			str += ",\"" + GetLevelData().mInLevelCoin + "\"";
			str += ",\"" + mCurCoinNum + "\"";
			str += ",\"" + GetCurCoinDiff() + "\"";
			str += ",\"" + mCumulativeCoinNum + "\"";
			str += ",\"" + GetCumulativeCoinDiff() + "\"";
			return str;
		}

		private string GetLevelCode()
		{
			return LevelData.GetLevelCode(mLevelNo, mSerialIdx.ContainsKey(mLevelNo) ? mSerialIdx[mLevelNo] : 1);
		}

		public void UpdateLevel(string levelNo, int lastAllSerialIdx, Dictionary<string, int> lastSerialIdx, int cumulativeCoinNum)
		{
			mAllSerialIdx = lastAllSerialIdx + 1;
			mLevelNo = levelNo;
			mSerialIdx.Clear();
			foreach (KeyValuePair<string, int> pr in lastSerialIdx)
			{
				mSerialIdx[pr.Key] = pr.Value;
			}
			if (mSerialIdx.ContainsKey(mLevelNo))
			{
				mSerialIdx[mLevelNo] += 1;
			} else
			{
				mSerialIdx[mLevelNo] = 1;
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
			mCumulativeCoinNum += reward + inLevelCoin + GetAdditionalCoin();
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

		private int GetAdditionalCoin()
		{
			LevelData levelData = GetLevelData();
			if (levelData == null)
			{
				return 0;
			}
			return levelData.mAdditionalCoin;
		}

	}
}
