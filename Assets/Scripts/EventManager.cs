using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public event Action OnLevelCompleteEvent;

    public void TriggerLevelCompleteEvent()
    {
        if (OnLevelCompleteEvent != null)
            OnLevelCompleteEvent();
    }
}
