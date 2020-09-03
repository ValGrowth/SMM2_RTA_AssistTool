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

        private string mLevelNo;
        private int mSerialIdx;
        private int mCurCoinNum;
        private int mCumulativeCoinNum;
        private STATE mState;

        public int GetCurCoinNum() { return mCurCoinNum; }
        public int GetCumulativeCoinNum() { return mCumulativeCoinNum; }
        public STATE GetState() { return mState; }

        public GameState()
        {
            Reset();
        }

        public void Reset()
        {
            mLevelNo = "";
            mSerialIdx = 0;
            mCurCoinNum = 0;
            mCumulativeCoinNum = 0;
            mState = STATE.CASTLE;
        }

        private string GetLevelCode()
        {
            if (mLevelNo == "" || mSerialIdx <= 0)
            {
                return "";
            }
            return mLevelNo + "_" + mSerialIdx;
        }

        public void UpdateLevel(string levelNo)
        {
            if (levelNo == mLevelNo)
            {
                mSerialIdx += 1;
            } else
            {
                mLevelNo = levelNo;
                mSerialIdx = 1;
            }
            mCurCoinNum = -1; // コースプレイ中は-1
            mState = STATE.LEVEL_PLAYING;
        }

        public void UpdateCoin(int coin)
        {
            mCurCoinNum = coin;
            mCumulativeCoinNum += mCurCoinNum;
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
            int coinDiff = mCurCoinNum - levelData.mTotalCoin;
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