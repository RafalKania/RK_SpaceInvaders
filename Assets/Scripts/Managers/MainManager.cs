//**************************************************
// MainManager.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 23 June 2021
//**************************************************

//#define MAINMANAGER_DEBUG

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeSoldiers
{
	public class MainManager : MonoBehaviour, IStateMachineCore<MainManager>, IMainManager
	{
        private IState<MainManager> _currentGameState;

        [Header("AUTOMATIC ADDED COMPONENTS")]
        [SerializeField]
        private DataStorage _dataStorage = null;
        public DataStorage _DataStorage
        {
            get => _dataStorage;
            set => _dataStorage = value;
        }

        [SerializeField]
        private InputManager _inputManager = null;
        public InputManager _InputManager
        {
            get => _inputManager;
            set => _inputManager = value;
        }

        [SerializeField]
        private CameraManager _cameraManager = null;
        public CameraManager _CameraManager => _cameraManager;

        [SerializeField]
        private PlayerManager _playerManager = null;
        public PlayerManager _PlayerManager
        {
            get => _playerManager;
            set => _playerManager = value;
        }

        [SerializeField]
        private EnemyManager _enemyManager = null;
        public EnemyManager _EnemyManager => _enemyManager;

        [Header("MANUAL ADDED COMPONENTS")]
        [SerializeField]
        private UIManager _uiManager = null;
        public UIManager _UIManager => _uiManager;

        [Header("SPAWNED OR MANUALLY CREATED OBJECTS")]
        [SerializeField]
        private PlayerCharacter _playerCharacter = null;
        public PlayerCharacter _PlayerCharacter
        {
            get => _playerCharacter;
            set => _playerCharacter = value;
        }

        [SerializeField]
        private Transform _playerSpawnPoint = null;
        public Transform _PlayerSpawnPoint => _playerSpawnPoint;

        [SerializeField]
        private Transform _enemiesSpawnPoint = null;
        public Transform _EnemiesSpawnPoint => _enemiesSpawnPoint;

        [SerializeField]
        private Bullet _bulletPrefab = null;
        public Bullet _BulletPrefab => _bulletPrefab;

        [Header("DATA")]
        public List<HighScoreData> _HighScoreDataToSave = new List<HighScoreData>();

        [Header("CONFIG")]
        [SerializeField]
        private GameConfig _gameConfig = null;
        public GameConfig _GameConfig => _gameConfig;

        void Awake()
		{
            //Initialize();
            
        }

		void Update()
		{
            _currentGameState?.UpdateState(this);
		}

        void FixedUpdate()
        {
            _currentGameState?.FixedUpdateState(this);
        }

        void OnApplicationQuit()
        {
            _dataStorage?.SaveGameData();
        }

        public void SetDontDestroy()
        {
            DontDestroyOnLoad(this);
        }

        public void Initialize()
        {
            _cameraManager = FindObjectOfType<CameraManager>();

            _dataStorage = GetComponent<DataStorage>();
            _inputManager = GetComponent<InputManager>();
            _playerManager = GetComponent<PlayerManager>();
            _enemyManager = GetComponent<EnemyManager>();
            _uiManager = FindObjectOfType<UIManager>();

            _uiManager?.InitializeUIManager();

            _dataStorage?.InitializeData(this);

            _cameraManager?.InitializeCamera();

            if (_playerSpawnPoint == null)
            {
                _playerSpawnPoint = CreateSpawnPoint("PlayerSpawnPoint", new Vector3(0, 0, -3));
            }

            if(_enemiesSpawnPoint == null)
            {
                _enemiesSpawnPoint = CreateSpawnPoint("EnemyWavesParent", Vector3.zero);
            }

            _playerManager.InitializeManager(this);

            _playerCharacter = _playerManager?.CreatePlayerCharacter();
            _playerCharacter.InitializeCharacter(this);
            _playerCharacter.CreateBulletsPool(BulletParentType.PLAYER);
            _playerCharacter.gameObject.SetActive(false);

            _enemyManager.InitializeManager(this);

            SetMainMenuState();

            _HighScoreDataToSave = _HighScoreDataToSave.OrderByDescending(highScoreData => highScoreData._Score).ToList();
        }

        #region IMainManager implementation
        public void SetMainMenuState()
        {
            var state = new MainMenuState();
            state._MainMenuScreensController = _uiManager._MainMenuScreensController;
            _uiManager._MainManagerListener = this;

            ChangeState(state);
        }

        public void SetGameplayState()
        {
            var state = new GameplayState();
            state._GameplayScreensController = _uiManager._GameplayScreensController;
            _uiManager._MainManagerListener = this;

            ChangeState(state);
        }

        public void SetResultState()
        {
            var state = new ResultState();
            state._ResultScreensController = _uiManager._ResultScreensController;
            _uiManager._MainManagerListener = this;

            ChangeState(state);
        }

        public void RestartGame()
        {
            SetGameplayState();
        }
        #endregion

        #region IStateMachineCore<T> implementation
        public void ChangeState(IState<MainManager> newState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.DeinitState(this);
            }

            _currentGameState = newState;

            if (_currentGameState != null)
            {
                _currentGameState.InitState(this);
            }

#if MAINMANAGER_DEBUG
            Debug.Log(string.Format($"[GAME STATE]: {_currentGameState}"));
#endif
        }
        #endregion

        public void AddNewHighScore(HighScoreData highScoreData)
        {
            var lastDataIndex = _HighScoreDataToSave.Count - 1;

            if (_HighScoreDataToSave.Count == _gameConfig._MaxSavedHighScores)
            {
                if (highScoreData._Score > _HighScoreDataToSave[lastDataIndex]._Score)
                {
                    _HighScoreDataToSave.RemoveAt(lastDataIndex);
                    _HighScoreDataToSave.Add(highScoreData);
                }
            }
            else if (_HighScoreDataToSave.Count > 0 && _HighScoreDataToSave.Count < _gameConfig._MaxSavedHighScores)
            {
                if (highScoreData._Score > _HighScoreDataToSave[lastDataIndex]._Score)
                {
                    _HighScoreDataToSave.Add(highScoreData);
                }
            }
            else if (_HighScoreDataToSave.Count == 0)
            {
                _HighScoreDataToSave.Add(highScoreData);
            }

            _HighScoreDataToSave = _HighScoreDataToSave.OrderByDescending(highScoreData => highScoreData._Score).ToList();
        }

        private Transform CreateSpawnPoint(string name, Vector3 position)
        {
            GameObject t = new GameObject(name);
            t.transform.position = position;

            return t.transform;
        }
    }
}
