//**************************************************
// PlayerManager.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 27 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class PlayerManager : MonoBehaviour
	{
        private MainManager _mainManager = null;
        [SerializeField]
        private PlayerCharacter _playerCharacterToSpawn = null;

        [SerializeField]
        private int _score = 0;
        public int _Score
        {
            get => _score;
            set => _score = value;
        }

        public void InitializeManager (MainManager mainManager)
        {
            _mainManager = mainManager;
        }

        public PlayerCharacter CreatePlayerCharacter()
        {
            return Instantiate(_playerCharacterToSpawn) as PlayerCharacter;
        }
	}
}
