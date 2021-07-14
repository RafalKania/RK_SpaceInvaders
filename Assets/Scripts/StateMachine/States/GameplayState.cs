//**************************************************
// GameplayState.cs
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
	public class GameplayState : BaseState, IMainMenuButton, IShotButton
    {
        private MainManager _mainManager = null;

        [SerializeField]
        private GameplayScreensController _gameplayScreensController = null;
        public GameplayScreensController _GameplayScreensController
        {
            get => _gameplayScreensController;
            set => _gameplayScreensController = value;
        }

        private float _enemyShotPauseTime = 0f;
        public override void InitState(MainManager mainManager)
        {
            base.InitState(mainManager);

            _mainManager = mainManager;

            _gameplayScreensController._GameplayScreen.SetupScreen(mainManager);

            _gameplayScreensController._GameplayScreen._MainMenuButtonListener = this;
            _gameplayScreensController._GameplayScreen._ShotButtonListener = this;
            _gameplayScreensController._GameplayScreen._MoveListener = mainManager._PlayerCharacter;

            _gameplayScreensController?.ShowController();

            _enemyShotPauseTime = mainManager._GameConfig._EnemyShotPauseTime;

            mainManager._PlayerCharacter._Move = false;
            mainManager._PlayerManager._Score = 0;

            mainManager._PlayerCharacter?.gameObject.SetActive(true);
            mainManager._PlayerCharacter._CanMove = true;

            mainManager._EnemyManager._WavesCount = 0;
            mainManager._EnemyManager.CreateEnemiesWave();
            mainManager._EnemyManager._CanEnemyWaveMove = true;
        }

        public override void UpdateState(MainManager mainManager)
        {
            base.UpdateState(mainManager);

            mainManager._InputManager.UpdateInputs(mainManager._PlayerCharacter);

            if(mainManager._PlayerCharacter._LivePoints == 0)
            {
                mainManager.SetResultState();
            }

            _gameplayScreensController._GameplayScreen._WavesText.text = $"{mainManager._EnemyManager._WavesCount}";
            _gameplayScreensController._GameplayScreen._ScoreText.text = $"{mainManager._PlayerManager._Score}";
            _gameplayScreensController._GameplayScreen._PlayerLivePointsText.text = $"{mainManager._PlayerCharacter._LivePoints}";

            mainManager._PlayerCharacter?.UpdateCharacter(mainManager);

            _enemyShotPauseTime -= Time.deltaTime;
            _gameplayScreensController._GameplayScreen._TimeText.text = $"{/*(int)*/_enemyShotPauseTime} s";

            if (_enemyShotPauseTime < 0)
            {
                mainManager._EnemyManager.EnemyShot();
                _enemyShotPauseTime = mainManager._GameConfig._EnemyShotPauseTime;
            }

            mainManager._EnemyManager.UpdateEnemyManager(mainManager);
        }

        public override void FixedUpdateState(MainManager mainManager)
        {
            base.FixedUpdateState(mainManager);

            mainManager._PlayerCharacter.FixedUpdateCharacter(mainManager);

            mainManager._EnemyManager.FixedUpdateEnemyManager(mainManager);
        }

        public override void DeinitState(MainManager mainManager)
        {
            base.DeinitState(mainManager);

            _gameplayScreensController?.HideController();

            mainManager._PlayerCharacter?.gameObject.SetActive(false);

            mainManager._PlayerCharacter?.DeinitCharacter(mainManager);

            mainManager._EnemyManager._CanEnemyWaveMove = true;
            mainManager._EnemyManager.DeinitAllEnemies(mainManager);
        }

        #region IMainMenuButton implementation
        public void HideScreen(UIScreen uiScreen)
        {
            uiScreen.HideScreen();
        }

        public void SetState()
        {
            _gameplayScreensController?._UIManager._MainManagerListener.SetMainMenuState();
        }
        #endregion

        #region IShotButton implementation
        public void SendShotRequest()
        {
            _mainManager._PlayerCharacter.Shot();
        }
        #endregion
    }
}
