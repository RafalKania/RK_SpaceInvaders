//**************************************************
// CameraManager.cs
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
	public class CameraManager : MonoBehaviour
	{
        [SerializeField]
        private Camera _mainCamera = null;

        public void InitializeCamera ()
        {
            if (_mainCamera == null)
            {
                Debug.LogError("_mainCamera not found!");
                return;
            }

            if (!_mainCamera.orthographic)
            {
                Debug.LogError("_mainCamera should be set to orthographic!");
                return;
            }
        }

        public Vector3 GetCameraBounds()
        {
            return _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
        }
	}
}
