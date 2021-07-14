//**************************************************
// IUIButtonPointer.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 4 July 2021
//**************************************************

using UnityEngine.EventSystems;

namespace CodeSoldiers
{
	public interface IUIButtonPointer<T> 
	{
        void SendPointerDownRequest(T handler);
        void SendPointerUpRequest(T handler);
	}
}
