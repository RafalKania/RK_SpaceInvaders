//**************************************************
// ResultScreen.cs
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
using TMPro;

namespace CodeSoldiers
{
	public class ResultScreen : UIScreen
    {
        public IMainMenuButton _MainMenuButtonListener = null;

        [SerializeField]
        private TextMeshProUGUI _scoreText = null;
        public TextMeshProUGUI _ScoreText
        {
            get => _scoreText;
            set => _scoreText = value;
        }

        [SerializeField]
        private TextMeshProUGUI _wavesText = null;
        public TextMeshProUGUI _WavesText
        {
            get => _wavesText;
            set => _wavesText = value;
        }

        [SerializeField]
        private Button _mainMenuButton = null;
        public override void ShowScreen()
        {
            base.ShowScreen();

            _mainMenuButton.onClick.AddListener(() => OnMainMenuClick());
        }

        public override void HideScreen()
        {
            base.HideScreen();

            _mainMenuButton.onClick.RemoveListener(() => OnMainMenuClick());
        }

        private void OnMainMenuClick()
        {
            _MainMenuButtonListener.SetState();
        }
    }
}
