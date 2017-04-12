using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMove : EnemyMove {
    public Animator animator;

    protected new void Start() {
        base.Start();
        animator = transform.FindChild("Mesh").GetComponent<Animator>();
    }

    protected override void Update() {
       // animator.speed =
        switch (moveState) {
            case MoveState.STAND:
                CalculateMoveDirection();
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
