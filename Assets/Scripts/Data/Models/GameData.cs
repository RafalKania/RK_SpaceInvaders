//**************************************************
// GameData.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 28 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
    [System.Serializable]
	public class GameData
	{
        public List<HighScoreData> _HighScoresData;

        public GameData(List<HighScoreData> highScoresData)
        {
            _HighScoresData = highScoresData;
        }
	}
}
