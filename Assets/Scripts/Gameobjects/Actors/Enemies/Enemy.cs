using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class Enemy : MonoBehaviour, IDestructable {
        [SerializeField] private int hitPoint;

        public void Hit(int damage) {
            hitPoint = hitPoint - damage;
            if (hitPoint == 0)
                Dead();
        }

        public void Dead() {
            Destroy(gameObject);
        }
    }
}