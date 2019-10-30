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

    #region переменные
    private static EventManager instance = null;
    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

    #endregion

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    

    public void AddListener(EVENT_TYPE eventType, IListener Listener) {
        //Список получателей
        List<IListener> ListenList = null;

        //Проверка типа события
        if(Listeners.TryGetValue(eventType, out ListenList)) {
            ListenList.Add(Listener);
            return;
        }

        //Если списка нет
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(eventType, ListenList);
    }

    //Посылаем события получателям
    public void PostNotification(EVENT_TYPE eventType, Component sender, Object param = null) {
        List<IListener> ListenList = null;
        if (!Listeners.TryGetValue(eventType, out ListenList))
            return;

        // Получатели есть 
        for (int i = 0; i < ListenList.Count; i++) {
            if (!ListenList[i].Equals(null)) {
                ListenList[i].OnEvent(eventType, sender, param);
            }
        }
        
    }

    //Удаляем событие из словаря +  получателей
    public void RemoveEvent(EVENT_TYPE eventType) {
        Listeners.Remove(eventType);
    }

    //удаляем избыточные записи из словаря
    public void RemoveRedundancies() {
        Dictionary<EVENT_TYPE, List<IListener>> tmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

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
