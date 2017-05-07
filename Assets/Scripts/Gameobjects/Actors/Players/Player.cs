using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Managers.EventMessages;
using Assets.Scripts.Rules.Movement;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
    public class Player : MonoBehaviour{
        public LiveState liveState;
        // Player keep reference on all player script Components
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

        void Update() {
            switch (liveState) {
            case LiveState.ALIVE:
                controll.Alive();
                break;
            case LiveState.DEAD:
                //  Dead();
                break;
            }
        }

        void OnTriggerEnter(Collider col) {
            if (col.CompareTag("Enemy")) {
                ThouchEnemy();
            }
        }

        void ThouchEnemy() {
            liveState = LiveState.DEAD;
            EventMessenger.TriggerEvent(GameEvents.DEFEATE);
        }

        public void MoveToStart(Levels.Level level) {
            controll.MoveToStart(level.start);
        }
    }
}