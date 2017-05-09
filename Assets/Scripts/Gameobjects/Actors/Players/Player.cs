using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Managers.EventMessages;
using Assets.Scripts.Rules.Movement;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
    public class Player : MonoBehaviour {
        public LiveState liveState;
        [SerializeField] private Controll controll;
        [SerializeField] private PlayerFire shoot;
        [SerializeField] private Collector collector;

        public void ResetPlayer() {
            liveState = LiveState.ALIVE;
            controll.Reset();
            shoot.Reset();
            collector.Reset();
        }

        public void SetNodeForPlayer(Node node) {
            controll.current = node;
        }

        public void Hit() {
            liveState = LiveState.DEAD;
            EventMessenger.TriggerEvent(GameEvents.DEFEATE);
        }

        public void MoveToStart(Levels.Level level) {
            controll.MoveToStart(level.start);
        }

        private bool IsAlive() {
            return liveState == LiveState.ALIVE;
        }

        protected void Update() {
            if (IsAlive())
                controll.Alive();
        }

        protected void OnTriggerEnter(Collider col) {
            EventMessenger<Player, Collider>.TriggerEvent(GameEvents.PLAYER_TAKE_DAMAGE, this, col);
            EventMessenger<Collector, Collider>.TriggerEvent(GameEvents.COLLECT_CRISTAL, collector, col);
            EventMessenger<Collector, Collider>.TriggerEvent(GameEvents.WARP_CRISTAL, collector, col);
        }

        protected void OnTriggerExit(Collider col) {
            EventMessenger<Collector, Collider>.TriggerEvent(GameEvents.STOP_WARP, collector, col);
        }
    }
}