using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListener
{
    void OnEvent(EVENT_TYPE eventType, Component sender, Object param = null);
}


