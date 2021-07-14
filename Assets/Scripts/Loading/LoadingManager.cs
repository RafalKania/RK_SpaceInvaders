//**************************************************
// LoadingManager.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 5 July 2021
//**************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CodeSoldiers
{
    public class LoadingManager : MonoBehaviour
    {
        //Singleton
        public static LoadingManager Instance;

        [SerializeField]
        private Image _loadingBar = null;

        [SerializeField]
        private AssetReference _mainManagerReference = null;
        [SerializeField]
        private AssetReference _mainSceneReference;

        [SerializeField]
        private GameObject _mainManagerObject;
        [SerializeField]
        private MainManager _mainManager;

        [SerializeField]
        private float _loadingStep = 1;

        [SerializeField]
        private bool _loadingInProgress = true;

        void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Addressables.LoadAssetAsync<GameObject>(Keys.AddressableAdresses.MAINMANAGER_ADDRESS).Completed += handle =>
                {
                    _mainManagerObject = handle.Result;
                };

            Addressables.LoadAssetAsync<GameObject>(Keys.AddressableAdresses.PLAYER_ADDRESS).Completed += PlayerLoaded;

            Addressables.LoadAssetAsync<GameObject>(Keys.AddressableAdresses.NPC1_ADDRESS).Completed += NPCLoaded; 
            Addressables.LoadAssetAsync<GameObject>(Keys.AddressableAdresses.NPC2_ADDRESS).Completed += NPCLoaded; 
            Addressables.LoadAssetAsync<GameObject>(Keys.AddressableAdresses.NPC3_ADDRESS).Completed += NPCLoaded;
            Addressables.LoadAssetAsync<GameObject>(Keys.AddressableAdresses.BULLET_ADDRESS).Completed += BulletLoaded; ; 

            Addressables.InstantiateAsync(_mainManagerReference, Vector3.zero, Quaternion.identity).Completed += OnMainManagerSpawned;

            Addressables.LoadSceneAsync(_mainSceneReference, UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += OnMainSceneLoaded;
        }

        private void Update()
        {
            if (_loadingInProgress)
            {
                _loadingBar.fillAmount += 1.0f / _loadingStep;
            }
        }

        private void OnMainSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            _loadingStep++;

            _mainManager?.Initialize();
        }

        private void OnMainManagerSpawned(AsyncOperationHandle<GameObject> handle)
        {
            _loadingStep++;
            
            if (_mainManagerObject != null)
            {
                _loadingInProgress = false;
                _mainManager = handle.Result.GetComponent<MainManager>();
                _mainManager.SetDontDestroy();
            }
        }

        private void PlayerLoaded(AsyncOperationHandle<GameObject> obj)
        {
            _loadingStep++;
        }

        private void NPCLoaded(AsyncOperationHandle<GameObject> obj)
        {
            _loadingStep++;
        }

        private void BulletLoaded(AsyncOperationHandle<GameObject> obj)
        {
            _loadingStep++;
        }
    }
}
