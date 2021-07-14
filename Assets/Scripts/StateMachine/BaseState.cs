//**************************************************
// BaseState.cs
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
    public class BaseState : IState<MainManager>
    {
        public virtual void InitState(MainManager mainManager)
        {
            
        }

        public virtual void UpdateState(MainManager mainManager)
        {
            
        }

        public virtual void FixedUpdateState(MainManager mainManager)
        {

        }

        public virtual void DeinitState(MainManager mainManager)
        {
            
        }
    }
}
