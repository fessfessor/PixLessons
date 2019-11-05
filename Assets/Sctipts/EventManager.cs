using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region singleton
    public static EventManager Instance {
        get { return instance; }
    }
    #endregion

    #region variables
    private static EventManager instance = null;

    // Делегат, обрабатывающий события
    public delegate void OnEvent(EVENT_TYPE eventType, Component sender, object param = null);
    // Главная коллекция с событиями
    private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

    #endregion

    #region awake
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    #endregion


    public void AddListener(EVENT_TYPE eventType, OnEvent Listener) {
        //Список получателей
        List<OnEvent> ListenList = null;

        //Проверка типа события
        if(Listeners.TryGetValue(eventType, out ListenList)) {
            ListenList.Add(Listener);
            return;
        }

        //Если списка нет
        ListenList = new List<OnEvent>();
        ListenList.Add(Listener);
        Listeners.Add(eventType, ListenList);
    }

    //Посылаем события получателям
    public void PostNotification(EVENT_TYPE eventType, Component sender, Object param = null) {
        List<OnEvent> ListenList = null;
        if (!Listeners.TryGetValue(eventType, out ListenList))
            return;

        // Получатели есть 
        for (int i = 0; i < ListenList.Count; i++) {
            if (!ListenList[i].Equals(null)) {
                ListenList[i](eventType, sender,param);
            }
        }
        
    }

    //Удаляем событие из словаря +  получателей
    public void RemoveEvent(EVENT_TYPE eventType) {
        Listeners.Remove(eventType);
    }

    //удаляем избыточные записи из словаря
    public void RemoveRedundancies() {
        Dictionary<EVENT_TYPE, List<OnEvent>> tmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

        foreach (var item in Listeners) {
            //Удаляем пустые ссылки
            foreach (var listItem in item.Value) {
                if (listItem.Equals(null))
                    item.Value.Remove(listItem);
            }

            if (item.Value.Count > 0)
                tmpListeners.Add(item.Key, item.Value);

            Listeners = tmpListeners;

        }
    }

    //Очистка словаря при смене сцены
    private void OnLevelWasLoaded() {
        RemoveRedundancies();
    }


}


// тип событий
public enum EVENT_TYPE {
    GAME_INIT,
    GAME_END,
    HEALTH_CHANGE,
    PLAYER_DEATH,
    KILL_ENEMY,
    BLD_BALL_HIT,
    BLD_BALL_MISS,
    BLD_MELEE_HIT,
    BLD_MELEE_MISS,
    BLD_DAMAGE

}
