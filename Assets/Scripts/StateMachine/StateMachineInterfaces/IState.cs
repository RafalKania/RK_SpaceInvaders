//**************************************************
// IState.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 23 June 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public interface IState<T>
	{
        void InitState(T manager);
        void UpdateState(T manager);
        void FixedUpdateState(T manager);
        void DeinitState(T manager);
	}
}
