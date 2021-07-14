//**************************************************
// MainMenuScreen.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: <Date>
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeSoldiers
{
	public class MainMenuScreen : UIScreen
	{
        public IMainMenuScreen _MainMenuScreenListener = null;

        [SerializeField]
        private Button _startGameButton = null;

        [SerializeField]
        private Button _highScoresButton = null;
        public override void ShowScreen()
        {
            base.ShowScreen();

            _startGameButton?.onClick.AddListener(() => OnStartGameClick());
            _highScoresButton?.onClick.AddListener(() => OnShowHighScoresClick());
        }

        public override void HideScreen()
        {
            base.HideScreen();

            _startGameButton?.onClick.RemoveListener(() => OnStartGameClick());
            _highScoresButton?.onClick.RemoveListener(() => OnShowHighScoresClick());
        }

        public void OnStartGameClick()
        {
            _MainMenuScreenListener?.StartGame();
        }

        public void OnShowHighScoresClick()
        {
            _MainMenuScreenListener?.ShowHighScores();
        }
    }
}
