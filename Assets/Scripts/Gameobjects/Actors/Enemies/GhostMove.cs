using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class GhostMove : EnemyMove {
        private Transform mesh;

        protected new void Start() {
            base.Start();
            mesh = transform.FindChild("Mesh");
        }

        protected override void Move() {
            base.Move();
            if (end != null)
                mesh.LookAt(end.position);
        }
    }
}