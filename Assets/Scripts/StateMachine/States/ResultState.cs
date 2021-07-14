//**************************************************
// ResultState.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 5 July 2021
//**************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class ResultState : BaseState, IMainMenuButton
	{
        private MainManager _mainManager = null;

        [SerializeField]
        private ResultScreensController _resultScreensController = null;
        public ResultScreensController _ResultScreensController
        {
            get => _resultScreensController;
            set => _resultScreensController = value;
        }

        private int _currentScore = 0;
        private int _currentWaves = 0;

        private string _scoreDate = string.Empty;

        public override void InitState(MainManager mainManager)
        {
            base.InitState(mainManager);

            _mainManager = mainManager;

            _currentScore = mainManager._PlayerManager._Score;
            _currentWaves = mainManager._EnemyManager._WavesCount;

            _scoreDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

            _resultScreensController._ResultScreen._MainMenuButtonListener = this;

            _resultScreensController.ShowController();

            _resultScreensController._ResultScreen._ScoreText.text = $"{Keys.ResultsText.SCORE_TEXT}\t\t{_currentScore}";
            _resultScreensController._ResultScreen._WavesText.text = $"{Keys.ResultsText.WAVES_TEXT}\t\t{_currentWaves}";

            mainManager.AddNewHighScore(new HighScoreData(_scoreDate, _currentScore));
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

            _resultScreensController.HideController();
        }

        #region IMainMenuButton implementation
        public void HideScreen(UIScreen uiScreen)
        {
            uiScreen.HideScreen();
        }

        public void SetState()
        {
            _resultScreensController?._UIManager._MainManagerListener.SetMainMenuState();
        }
        #endregion
    }
}
