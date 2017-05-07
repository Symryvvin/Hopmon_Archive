using Assets.Scripts.Gameobjects.Actors.Shells;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
    public class PlayerShell : Shell {
        public int damage;
        public AudioSource shot;
        public AudioSource hit;

        public override void OnEnable() {
            base.OnEnable();
            AttachParent(hit);
            if (path != null) {
                DetachParent(shot);
                shot.Play();
            }
        }

        public void Explosion() {
            DetachParent(hit);
            AttachParent(shot);
            hit.Play();
            Destroy();
        }

        private void DetachParent(AudioSource source) {
            source.transform.SetParent(null);
        }

        private void AttachParent(AudioSource source) {
            source.transform.SetParent(transform);
        }
    }
}