using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Action<CLICK_ACTION_TYPE> m_callBack;
    public void Initialize(Action<CLICK_ACTION_TYPE> callBack)
    {
        m_callBack = callBack;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_callBack?.Invoke(CLICK_ACTION_TYPE.POINTER_DOWN);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_callBack?.Invoke(CLICK_ACTION_TYPE.POINTER_UP);
    }

    
}
