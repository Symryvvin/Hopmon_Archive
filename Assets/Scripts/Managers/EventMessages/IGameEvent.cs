using System;

namespace Assets.Scripts.Managers.EventMessages {
    public interface IGameEvent {
        void Add(Delegate listener);

        void Remove(Delegate listener);

        string ListEvents();
    }
}