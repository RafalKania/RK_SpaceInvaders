//**************************************************
// HighScoreScreen.cs
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
	public class HighScoreScreen : UIScreen
    {
        public IMainMenuButton _MainMenuButtonListener = null;

        [SerializeField]
        private TextMeshProUGUI _highScoreTextPrefab = null;
        public TextMeshProUGUI _HighScoreTextPrefab => _highScoreTextPrefab;

        [SerializeField]
        private Transform _highScoreTextParent = null;
        public Transform _HighScoreTextParent => _highScoreTextParent;

        [SerializeField]
        private Button _mainMenuButton = null;

        public override void SetupScreen(MainManager mainManager)
        {
            base.SetupScreen(mainManager);

            if (_highScoreTextParent.childCount == 0)
            {
                for (int i = 0; i < mainManager._GameConfig._MaxSavedHighScores; i++)
                {
                    var t = Object.Instantiate(_highScoreTextPrefab) as TextMeshProUGUI;
                    t.transform.SetParent(_highScoreTextParent);
                    t.transform.localScale = Vector3.one;

                    t.text = string.Empty;

                    t.gameObject.SetActive(false);
                }

                for (int i = 0; i < mainManager._HighScoreDataToSave.Count; i++)
                {
                    var textField = _highScoreTextParent.GetChild(i).GetComponent<TextMeshProUGUI>();

                    textField.gameObject.SetActive(true);
                    textField.text = string.Format("{0:00}.\t{1}\t\t\t{2}", i + 1, mainManager._HighScoreDataToSave[i]._ScoreDate, mainManager._HighScoreDataToSave[i]._Score);
                }
            }
            else
            {
                for (int i = 0; i < mainManager._HighScoreDataToSave.Count; i++)
                {
                    var textField = _highScoreTextParent.GetChild(i).GetComponent<TextMeshProUGUI>();

                    textField.gameObject.SetActive(true);
                    textField.text = string.Format("{0:00}.\t{1}\t\t\t{2}", i + 1, mainManager._HighScoreDataToSave[i]._ScoreDate, mainManager._HighScoreDataToSave[i]._Score);
                }
            }
        }

        public override void ShowScreen()
        {
            base.ShowScreen();

            _mainMenuButton?.onClick.AddListener(() => OnMainMenuClick());
        }

        public override void HideScreen()
        {
            base.HideScreen();
        }

        private void OnMainMenuClick()
        {
            _MainMenuButtonListener.HideScreen(this);
        }
    }
}
