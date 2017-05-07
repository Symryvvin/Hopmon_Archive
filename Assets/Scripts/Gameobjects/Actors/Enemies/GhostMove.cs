using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class GhostMove : EnemyMove {

        protected override void Move() {
            base.Move();
            if (end != null)
                transform.LookAt(end.position + new Vector3(0, transform.position.y, 0));
        }
    }
}