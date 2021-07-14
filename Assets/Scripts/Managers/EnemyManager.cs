//**************************************************
// EnemyManager.cs
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
	public class EnemyManager : MonoBehaviour
	{
        private MainManager _mainManager = null;

        [SerializeField]
        private List<EnemyCharacter> _enemiesToSpawn = new List<EnemyCharacter>(); 

        [SerializeField]
        private List<EnemyCharacter> _enemies = new List<EnemyCharacter>();
        public List<EnemyCharacter> _Enemies => _enemies;
        [SerializeField]
        private List<EnemyCharacter> _deadEnemies = new List<EnemyCharacter>();
        public List<EnemyCharacter> _DeadEnemies
        {
            get => _deadEnemies;
            set => _deadEnemies = value;
        }

        [SerializeField]
        private int _wavesCount = 0;
        public int _WavesCount
        {
            get => _wavesCount;
            set => _wavesCount = value;
        }

        [SerializeField]
        private Transform _parentPoint = null;

        [SerializeField]
        private float _moveBounds = 0;
        [SerializeField]
        private float _moveHorizontalPauseTime = 0;
        [SerializeField]
        private float _moveVerticalPauseTime = 0;

        [SerializeField]
        private bool _canEnemyWaveMove = false;
        public bool _CanEnemyWaveMove
        {
            get => _canEnemyWaveMove;
            set => _canEnemyWaveMove = value;
        }

        [SerializeField]
        private bool _isOnLeftSide = false;

        public void InitializeManager(MainManager mainManager)
        {
            _mainManager = mainManager;

            _parentPoint = mainManager._EnemiesSpawnPoint;
            _moveBounds = mainManager._CameraManager.GetCameraBounds().x - 1;
            _isOnLeftSide = false;

            _enemies.Clear();

            EnemyCharacter enemy;

            for (int i = 0; i < mainManager._GameConfig._WaveColumns; i++)
            {
                for (int j = 0; j < mainManager._GameConfig._WaveColumnElements; j++)
                {
                    if (j != 0)
                    {
                        if (j % 2 == 0)
                        {
                             enemy = Instantiate(_enemiesToSpawn[1]) as EnemyCharacter;
                        }
                        else
                        {
                            enemy = Instantiate(_enemiesToSpawn[2]) as EnemyCharacter;
                        }
                    }
                    else
                    {
                        enemy = Instantiate(_enemiesToSpawn[0]) as EnemyCharacter;
                    }

                    enemy.InitializeCharacter(mainManager);

                    _enemies.Add(enemy);
                    enemy.transform.SetParent(_parentPoint);

                    enemy.transform.localPosition = new Vector3(
                        -(mainManager._GameConfig._WaveColumns * 0.5f) + i + _mainManager._GameConfig._EnemyXOffset,
                        0,
                        j + _mainManager._GameConfig._EnemyZOffset
                        );

                    enemy._StartPosition = enemy.transform.localPosition;

                    enemy.gameObject.SetActive(false);
                }
            }

            _moveHorizontalPauseTime = mainManager._GameConfig._EnemyWaveMoveHorizontalPauseTime;
            _moveVerticalPauseTime = mainManager._GameConfig._EnemyWaveMoveVerticalPauseTime;
        }

        public void UpdateEnemyManager(MainManager mainManager)
        {
            CheckEnemiesCount();

            foreach (EnemyCharacter e in _enemies)
            {
                e.UpdateCharacter(mainManager);

                if(e.transform.localPosition.x > _moveBounds)
                {
                    _isOnLeftSide = false;
                }
                else if (e.transform.localPosition.x < -_moveBounds)
                {
                    _isOnLeftSide = true;
                }
            }
        }

        public void FixedUpdateEnemyManager(MainManager mainManager)
        {
            if (_canEnemyWaveMove)
            {
                if (_isOnLeftSide)
                {
                    MoveEnemyWaveHorizontally(Vector3.right);
                }
                else
                {
                    MoveEnemyWaveHorizontally(-Vector3.right);
                }

                MoveEnemyWaveVertically();
            }

            foreach (EnemyCharacter e in _enemies)
            {
                e.FixedUpdateCharacter(mainManager);
            }
        }

        public void CreateEnemiesWave()
        {
            _wavesCount++;

            if (_deadEnemies.Count > 0)
            {
                foreach (EnemyCharacter e in _deadEnemies)
                {
                    _enemies.Add(e);
                }

                _deadEnemies.Clear();
            }

            foreach (EnemyCharacter e in _enemies)
            {
                e.transform.localPosition = e._StartPosition;
                e._LivePoints = _mainManager._GameConfig._EnemyLivePoints;
                e.gameObject.SetActive(true);
            }
        }

        public void CheckEnemiesCount()
        {
            if (_enemies.Count == 0)
            {
                CreateEnemiesWave();
            }
        }

        public EnemyCharacter SelectRandomEnemy()
        {
            var r = Random.Range(0, _enemies.Count);
            return _enemies[r];
        }

        public void EnemyShot()
        {
            SelectRandomEnemy().Shot();
        }

        public void MoveEnemyWaveHorizontally(Vector3 direction)
        {
            foreach (EnemyCharacter e in _enemies)
            {
                e._Rigidbody.MovePosition(e.transform.localPosition + (direction * _mainManager._GameConfig._EnemyWaveMovementVelocity
                * Time.deltaTime));
            }
        }

        public void MoveEnemyWaveVertically()
        {
            _moveVerticalPauseTime -= Time.deltaTime;

            if (_moveVerticalPauseTime < 0)
            {
                foreach (EnemyCharacter e in _enemies)
                {
                    e._Rigidbody.MovePosition(e.transform.localPosition + (-Vector3.forward * _mainManager._GameConfig._EnemyWaveMovementVelocity
                        * Time.deltaTime));
                }

                _moveVerticalPauseTime = _mainManager._GameConfig._EnemyWaveMoveVerticalPauseTime;
            }
        }

        public void DeinitAllEnemies(MainManager mainManager)
        {
            foreach (EnemyCharacter e in _enemies)
            {
                e.DeinitCharacter(mainManager);
            }
        }

        public void ClearDeadEnemies()
        {
            if(_deadEnemies.Count > 0)
            {
                _deadEnemies.Clear();
            }
        }
    }
}
