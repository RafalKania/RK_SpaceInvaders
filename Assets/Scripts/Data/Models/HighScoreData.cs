//**************************************************
// HighScoreData.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 28 June 2021
//**************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
    [System.Serializable]
	public class HighScoreData
	{
        public string _ScoreDate;
        public int _Score;

        public HighScoreData(string scoreDate, int score)
        {
            _ScoreDate = scoreDate;
            _Score = score;
        }
	}
}
