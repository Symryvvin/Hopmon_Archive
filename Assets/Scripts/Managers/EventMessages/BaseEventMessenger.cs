using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Managers.EventMessages {
    public class BaseEventMessenger {
        protected static bool AddListener(string eventName, Delegate listener) {
            IGameEvent gameEvent = IsExistEvent(eventName);
            if (gameEvent == null) return false;
            gameEvent.Add(listener);
            return true;
        }

        protected static void AddEvent(string eventName, IGameEvent gameEvent) {
            EventManager.instance.events.Add(eventName, gameEvent);
        }

        protected static void RemoveListener(string eventName, Delegate listener) {
            if (EventManager.instance == null) return;
            IGameEvent gameEvent;
            if (EventManager.instance.events.TryGetValue(eventName, out gameEvent)) {
                gameEvent.Remove(listener);
            }
        }

        protected static IGameEvent IsExistEvent(string eventName) {
            IGameEvent gameEvent;
            return EventManager.instance.events.TryGetValue(eventName, out gameEvent) ? gameEvent : null;
        }
    }

    public class EventMessenger : BaseEventMessenger {
        public static void StartListener(string name, UnityAction action) {
            if (!AddListener(name, action)) {
                GameEvent gameEvent = new GameEvent();
                gameEvent.Add(action);
                AddEvent(name, gameEvent);
            }
        }

        public static void TriggerEvent(string name) {
            GameEvent gameEvent = IsExistEvent(name) as GameEvent;
            if (gameEvent != null) {
                gameEvent.Invoke();
            }
        }

        public static void StopListener(string name, UnityAction action) {
            RemoveListener(name, action);
        }

        internal class GameEvent : UnityEvent, IGameEvent {
            private readonly LinkedList<UnityAction> actions;

            public GameEvent() {
                actions = new LinkedList<UnityAction>();
            }

            public void Add(Delegate listener) {
                actions.AddLast(listener as UnityAction);
                AddListener(actions.Last.Value);
            }

            public void Remove(Delegate listener) {
                actions.Remove(listener as UnityAction);
                RemoveListener(listener as UnityAction);
            }

            public string ListEvents() {
                string list = "";
                foreach (var action in actions) {
                    list += action.Method + " in " + action.Target.GetType() + "\n";
                }
                return list;
            }
        }
    }

    public class EventMessenger<T> : BaseEventMessenger {
        public static void StartListener(string name, UnityAction<T> action) {
            if (!AddListener(name, action)) {
                GameEvent gameEvent = new GameEvent();
                gameEvent.Add(action);
                AddEvent(name, gameEvent);
            }
        }

        public static void TriggerEvent(string name, T param) {
            GameEvent gameEvent = IsExistEvent(name) as GameEvent;
            if (gameEvent != null) {
                gameEvent.Invoke(param);
            }
        }

        public static void StopListener(string name, UnityAction action) {
            RemoveListener(name, action);
        }

        internal class GameEvent : UnityEvent<T>, IGameEvent {
            private readonly LinkedList<UnityAction<T>> actions;

            public GameEvent() {
                actions = new LinkedList<UnityAction<T>>();
            }

            public void Add(Delegate listener) {
                actions.AddLast(listener as UnityAction<T>);
                AddListener(actions.Last.Value);
            }

            public void Remove(Delegate listener) {
                actions.Remove(listener as UnityAction<T>);
                RemoveListener(listener as UnityAction<T>);
            }

            public string ListEvents() {
                string list = "";
                foreach (var action in actions) {
                    list += action.Method + " in " + action.Target.GetType() + "\n";
                }
                return list;
            }
        }
    }
}