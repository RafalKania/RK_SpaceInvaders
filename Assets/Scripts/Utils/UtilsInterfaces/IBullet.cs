//**************************************************
// IBullet.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 3 July 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSoldiers
{
	public interface IBullet
	{
        void CreateBulletsPool(BulletParentType parentType);
        void ResetBulletRequest(Bullet bullet);
	}
}
