using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Managers.EventMessages;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class Enemy : MonoBehaviour, IDestructable {
        [SerializeField] private int hitPoint;
        private EnemyMove move;
        private Rigidbody rigidbody;
        private Collider collider;
        public bool armored;

        protected void Start() {
            move = GetComponent<EnemyMove>();
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        public void Hit(int damage) {
            hitPoint = hitPoint - damage;
            if (armored) {
                ChangeTexture();
            }
            if (hitPoint == 0)
                Dead();
        }

        private void ChangeTexture() {
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_EmissionColor", new Color(0.7f, 0, 0));
        }

        public void Dead() {
            move.current.RestoreType();
            if (move.end != null)
                move.end.RestoreType();
            move.IsDead(true);
            collider.isTrigger = false;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(0, 1000, 0, ForceMode.Force);
            rigidbody.AddTorque(0, 0, 1000, ForceMode.Impulse);
            rigidbody.useGravity = true;
            Destroy(gameObject, 4f);
        }

        protected void OnTriggerEnter(Collider col) {
            EventMessenger<Enemy, Collider>.TriggerEvent(GameEvents.ENEMY_TAKE_DAMAGE, this, col);
        }
    }
}