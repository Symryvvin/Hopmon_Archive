using Assets.Scripts.Gameobjects.Actors.Players;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class JellyFishMove : EnemyMove {
        public Animator animator;

        protected new void Start() {
            base.Start();
            animator = transform.FindChild("Mesh").GetComponent<Animator>();
        }

        protected override void Update() {
            switch (moveState) {
            case MoveState.STAND:
                Move();
                animator.SetBool("Jump", false);
                animator.SetBool("Idle", true);
                break;
            case MoveState.WALK:
                animator.SetBool("Jump", true);
                animator.SetBool("Idle", false);
                CurrentDirection();
                break;
            }
        }


    }
}
