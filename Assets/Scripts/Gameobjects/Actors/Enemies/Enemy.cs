using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class Enemy : MonoBehaviour, IDestructable {
        [SerializeField] private int hitPoint;
        private EnemyMove move;

        void Start() {
            move = GetComponent<EnemyMove>();
        }

        public void Hit(int damage) {
            hitPoint = hitPoint - damage;
            if (hitPoint == 0)
                Dead();
        }

        public void Dead() {
            move.current.RestoreType();
            if (move.end != null)
                move.end.RestoreType();
            Destroy(gameObject);
        }
    }
}