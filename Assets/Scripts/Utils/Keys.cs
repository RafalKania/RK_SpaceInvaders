//**************************************************
// Keys.cs
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
	public class Keys
	{
        public class FileSave
        {
            public const string FILE_DESTINATION = "save.data";
        }

        public class Tags
        {
            public const string PLAYER_TAG = "Player";
            public const string ENEMY_TAG = "Enemy";
            public const string BULLET_TAG = "Bullet";
        }

        public class ResultsText
        {
            public const string SCORE_TEXT = "SCORE:"; 
            public const string WAVES_TEXT = "WAVES:"; 
        }

        public class AddressableAdresses
        {
            public const string MAINMANAGER_ADDRESS = "Assets/GameElements/Prefabs/Managers/MainManager.prefab";
            public const string PLAYER_ADDRESS = "Assets/GameElements/Prefabs/PlayerCharacter.prefab";
            public const string NPC1_ADDRESS = "Assets/GameElements/Prefabs/NPC_1_Character.prefab";
            public const string NPC2_ADDRESS = "Assets/GameElements/Prefabs/NPC_2_Character.prefab";
            public const string NPC3_ADDRESS = "Assets/GameElements/Prefabs/NPC_3_Character.prefab";
            public const string BULLET_ADDRESS = "Assets/GameElements/Prefabs/Projectile.prefab";
        }
	}
}
