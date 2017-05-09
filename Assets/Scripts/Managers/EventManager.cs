using System.Collections.Generic;
using Assets.Scripts.Managers.EventMessages;

namespace Assets.Scripts.Managers {
    public class EventManager : SingletonManager<EventManager>, IManager {
        public ManagerStatus status {
            get { return managerStatus; }
        }

        public Dictionary<string, IGameEvent> events { get; private set; }

        protected override void Init() {
            if (events == null) {
                events = new Dictionary<string, IGameEvent>();
            }
        }
    }
}

