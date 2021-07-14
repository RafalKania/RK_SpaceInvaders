//**************************************************
// MainMenuState.cs
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
	public class MainMenuState : BaseState, IMainMenuScreen, IMainMenuButton
    {
        private MainManager _mainManager = null;

        [SerializeField]
        private MainMenuScreensController _mainMenuScreensController = null;
        public MainMenuScreensController _MainMenuScreensController
        {
            get => _mainMenuScreensController;
            set => _mainMenuScreensController = value;
        }

        public override void InitState(MainManager mainManager)
        {
            base.InitState(mainManager);

            _mainManager = mainManager;

            _mainMenuScreensController._MainMenuScreen._MainMenuScreenListener = this;
            _mainMenuScreensController._HighScoreScreen._MainMenuButtonListener = this;

            _mainMenuScreensController?.ShowController();
        }

        public override void UpdateState(MainManager mainManager)
        {
            base.UpdateState(mainManager);
        }

        public override void FixedUpdateState(MainManager mainManager)
        {
            base.FixedUpdateState(mainManager);
        }

        public override void DeinitState(MainManager mainManager)
        {
            base.DeinitState(mainManager);

            _mainMenuScreensController?.HideController();
        }

        #region IMainMenuScreen implementation
        public void StartGame()
        {
            _mainMenuScreensController?._UIManager._MainManagerListener.SetGameplayState();
        }

        public void ShowHighScores()
        {
            _mainMenuScreensController?._HighScoreScreen.ShowScreen();

            _mainMenuScreensController._HighScoreScreen.SetupScreen(_mainManager);
        }
        #endregion

        #region IMainMenuButton implementation
        public void HideScreen(UIScreen uiScreen)
        {
            uiScreen.HideScreen();
        }

        public void SetState()
        {
            
        }
        #endregion
    }
}
