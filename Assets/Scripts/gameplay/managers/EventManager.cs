using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager : SingletonManager<EventManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    private Dictionary<string, UnityEvent> events;

    protected override void Init() {
        if (events == null) {
            events = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListener(string eventName, UnityAction listener) {
        UnityEvent thisEvent;
        if (instance.events.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.events.Add(eventName, thisEvent);
        }
    }

    public static void StopListener(string eventName, UnityAction listener) {
        if (manager == null) return;
        UnityEvent thisEvent;
        if (instance.events.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName) {
        UnityEvent thisEvent;
        if (instance.events.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }
}