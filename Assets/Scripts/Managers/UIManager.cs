//**************************************************
// UIManager.cs
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
	public class UIManager : MonoBehaviour
	{
        public IMainManager _MainManagerListener = null;

        [SerializeField]
        private MainMenuScreensController _mainMenuScreensController = null;
        public MainMenuScreensController _MainMenuScreensController => _mainMenuScreensController;

        [SerializeField]
        private GameplayScreensController _gameplayScreensController = null;
        public GameplayScreensController _GameplayScreensController => _gameplayScreensController;

        [SerializeField]
        private ResultScreensController _resultScreensController = null;
        public ResultScreensController _ResultScreensController => _resultScreensController;

        public void InitializeUIManager()
        {
            _mainMenuScreensController?.PrepareController(this);
            _gameplayScreensController?.PrepareController(this);
            _resultScreensController?.PrepareController(this);
        }
	}
}
