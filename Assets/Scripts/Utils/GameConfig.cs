//**************************************************
// GameConfig.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 28 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
    public class GameConfig : ScriptableObject
	{
        [Header("GAME CONST")]
        public int _MaxSavedHighScores = 0;

        [Header("PLAYER CONST")]
        public int _MaxPlayerLives = 0;
        public int _PlayerMovementBounds = 0;     
        public float _PlayerInvunerableTime = 0;
        public float _PlayerMovingVelocity = 1;

        [Header("ENEMY CONST")]
        public int _EnemyLivePoints = 0;
        public float _EnemyShotPauseTime = 0f;

        [Header("ENEMY WAVES")]
        public int _WaveColumns = 0;
        public int _WaveColumnElements = 0;
        public float _EnemyXOffset = 0;
        public float _EnemyYOffset = 0;
        public float _EnemyZOffset = 0;
        public float _EnemyCreateWaveDelayTime = 0;
        public float _EnemyWaveMoveHorizontalPauseTime = 0;
        public float _EnemyWaveMoveVerticalPauseTime = 0;
        [Range(1f, 6.0f)]
        public float _EnemyWaveMovementVelocity = 0;

        [Header("BULLETS")]
        public float _BulletMovementVelocity = 0;
        public int _PlayerBulletPoolCount = 0;
        public int _EnemyBulletPoolCount = 0;
    }
}
