//**************************************************
// IMainMenuScreen.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 26 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public interface IMainMenuScreen
	{
        void StartGame();
        void ShowHighScores();
	}
}
