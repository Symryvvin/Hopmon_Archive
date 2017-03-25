using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
    private Dictionary<string, UnityEvent> events;

    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (eventManager == null) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (eventManager != null) {
                    eventManager.Init();
                }
                else {
                    Debug.LogError("No EventManager on Scene");
                }
            }
            return eventManager;
        }
    }

    private void Init() {
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
        if (eventManager == null) return;
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
