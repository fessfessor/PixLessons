using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListener
{
    void OnEvent(EVENT_TYPE eventType, Component sender, Object param = null);
}


public enum EVENT_TYPE {
    GAME_INIT,
    GAME_END,
    HEALTH_CHANGE,
    PLAYER_DEATH
}