//**************************************************
// GameplayScreen.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 25 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace CodeSoldiers
{
	public class GameplayScreen : UIScreen, IUIButtonPointer<UIButtonsPointerHandler>
    {
        public IMainMenuButton _MainMenuButtonListener = null;
        public IShotButton _ShotButtonListener = null;
        public IMove _MoveListener = null;

        [SerializeField]
        private UIButtonsPointerHandler _moveLeftButton = null;

        [SerializeField]
        private UIButtonsPointerHandler _moveRightButton = null;

        [SerializeField]
        private UIButtonsPointerHandler _shotButton = null;

        [SerializeField]
        private Button _closeButton = null;

        [SerializeField]
        private TextMeshProUGUI _wavesText = null;
        public TextMeshProUGUI _WavesText
        {
            get => _wavesText;
            set => _wavesText = value;
        }

        [SerializeField]
        private TextMeshProUGUI _scoreText = null;
        public TextMeshProUGUI _ScoreText
        {
            get => _scoreText;
            set => _scoreText = value;
        }

        [SerializeField]
        private TextMeshProUGUI _playerLivePointsText = null;
        public TextMeshProUGUI _PlayerLivePointsText
        {
            get => _playerLivePointsText;
            set => _playerLivePointsText = value;
        }

        [SerializeField]
        private TextMeshProUGUI _timeText = null;
        public TextMeshProUGUI _TimeText
        {
            get => _timeText;
            set => _timeText = value;
        }

        public override void SetupScreen(MainManager mainManager)
        {
            base.SetupScreen(mainManager);

            _moveLeftButton._UIButtonPointerListener = this;
            _moveRightButton._UIButtonPointerListener = this;
            _shotButton._UIButtonPointerListener = this;
        }

        public override void ShowScreen()
        {
            base.ShowScreen();

            _closeButton.onClick.AddListener(() => OnCloseClick());
        }

        public override void HideScreen()
        {
            base.HideScreen();

            _closeButton.onClick.RemoveListener(() => OnCloseClick());
        }

        private void OnCloseClick()
        {
            _MainMenuButtonListener.SetState();
        }


        #region IUIButtonsPointer implementation
        public void SendPointerDownRequest(UIButtonsPointerHandler handler)
        {
            if(handler == _moveLeftButton)
            {
                _MoveListener.DirectionMove(-Vector3.right);
            }
            else if (handler == _moveRightButton)
            {
                _MoveListener.DirectionMove(Vector3.right);
            }
            else if(handler == _shotButton)
            {
                _ShotButtonListener.SendShotRequest();
            }
        }

        public void SendPointerUpRequest(UIButtonsPointerHandler handler)
        {
            if (handler == _moveLeftButton || handler == _moveRightButton)
            {
                _MoveListener.StopMoving();
            }
            else if (handler == _shotButton)
            {
                
            }
        }
        #endregion
    }
}
