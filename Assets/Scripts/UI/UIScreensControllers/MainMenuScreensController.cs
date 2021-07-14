//**************************************************
// MainMenuScreensController.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 25 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class MainMenuScreensController : UIScreenController
	{
        [SerializeField]
        private MainMenuScreen _mainMenuScreen = null;
        public MainMenuScreen _MainMenuScreen => _mainMenuScreen;

        [SerializeField]
        private HighScoreScreen _highScoreScreen = null;
        public HighScoreScreen _HighScoreScreen => _highScoreScreen;

        public override void PrepareController(UIManager uiManager)
        {
            base.PrepareController(uiManager);

            HideController();
        }

        public override void ShowController()
        {
            base.ShowController();

            _MainMenuScreen?.ShowScreen();
        }

        public override void HideController()
        {
            base.HideController();

            _mainMenuScreen?.HideScreen();
            _highScoreScreen?.HideScreen();
        }
    }
}