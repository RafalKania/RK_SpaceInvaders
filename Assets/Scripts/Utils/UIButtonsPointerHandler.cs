//**************************************************
// UIButtonsPointerHandler.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 4 July 2021
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CodeSoldiers
{
    public class UIButtonsPointerHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public IUIButtonPointer<UIButtonsPointerHandler> _UIButtonPointerListener = null;

        #region IPointerDownHandler implementation
        public void OnPointerDown(PointerEventData eventData)
        {
            _UIButtonPointerListener.SendPointerDownRequest(this);
        }
        #endregion

        #region IPointerUpHandler implementation
        public void OnPointerUp(PointerEventData eventData)
        {
            _UIButtonPointerListener.SendPointerUpRequest(this);
        }
        #endregion
    }
}
