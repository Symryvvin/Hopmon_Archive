using Assets.Scripts.Gameobjects.Actors.Players;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class JellyFishMove : EnemyMove {
        [SerializeField] private Animator animator;

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
