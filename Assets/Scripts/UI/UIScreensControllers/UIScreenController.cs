//**************************************************
// UIScreenController.cs
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
	public class UIScreenController : MonoBehaviour
	{
        [HideInInspector]
        public UIManager _UIManager { get; private set; } = null;

        public virtual void PrepareController(UIManager uiManager)
        {
            _UIManager = uiManager;
        }

        public virtual void ShowController()
        {
            gameObject.SetActive(true);
        }

        public virtual void HideController()
        {
            gameObject.SetActive(false);
        }
    }
}
