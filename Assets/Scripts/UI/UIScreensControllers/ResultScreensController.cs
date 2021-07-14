//**************************************************
// ResultScreensController.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 5 July 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class ResultScreensController : UIScreenController
	{
        [SerializeField]
        private ResultScreen _resultScreen = null;
        public ResultScreen _ResultScreen
        {
            get => _resultScreen;
            set => _resultScreen = value;
        }

        public override void PrepareController(UIManager uiManager)
        {
            base.PrepareController(uiManager);

            HideController();
        }

        public override void ShowController()
        {
            base.ShowController();

            _resultScreen?.ShowScreen();
        }

        public override void HideController()
        {
            base.HideController();

            _resultScreen?.HideScreen();
        }
    }
}
