//**************************************************
// GameplayScreensController.cs
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
	public class GameplayScreensController : UIScreenController
	{
        [SerializeField]
        private GameplayScreen _gameplayScreen = null;
        public GameplayScreen _GameplayScreen
        {
            get => _gameplayScreen;
            set => _gameplayScreen = value;
        }

        public override void PrepareController(UIManager uiManager)
        {
            base.PrepareController(uiManager);

            HideController();
        }

        public override void ShowController()
        {
            base.ShowController();

            _gameplayScreen?.ShowScreen();
        }

        public override void HideController()
        {
            base.HideController();

            _gameplayScreen?.HideScreen();
        }
    }
}
