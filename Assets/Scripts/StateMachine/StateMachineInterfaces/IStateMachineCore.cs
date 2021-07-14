//**************************************************
// IStateMachineCore.cs
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
	public interface IStateMachineCore<T>
	{
        void ChangeState(IState<T> newState);
	}
}
