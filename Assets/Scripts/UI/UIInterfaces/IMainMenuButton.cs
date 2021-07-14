//**************************************************
// IMainMenuButton.cs
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
	public interface IMainMenuButton
	{
        void HideScreen(UIScreen uiScreen);
        void SetState();
	}
}
