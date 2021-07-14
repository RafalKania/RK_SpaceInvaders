//**************************************************
// UIScreen.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 24 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public class UIScreen : MonoBehaviour
	{
        public virtual void SetupScreen(MainManager mainManager)
        {

        }

		public virtual void ShowScreen()
        {
            gameObject.SetActive(true);
        }

        public virtual void HideScreen()
        {
            gameObject.SetActive(false);
        }
    }
}
