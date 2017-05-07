using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class CubeMove : EnemyMove {
        public float rotateSpeed;

        protected override void Update() {
            base.Update();
            switch (direction) {
            case MoveDirection.FORWARD:
                transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.right) *
                                     transform.rotation;
                break;
            case MoveDirection.BACK:
                transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.left) *
                                     transform.rotation;
                break;
            case MoveDirection.LEFT:
                transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.forward) *
                                     transform.rotation;
                break;
            case MoveDirection.RIGHT:
                transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.back) *
                                     transform.rotation;
                break;
            }
        }
    }
}