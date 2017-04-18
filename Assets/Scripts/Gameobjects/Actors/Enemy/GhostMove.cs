using UnityEngine;

public class GhostMove : EnemyMove {
    private Transform mesh;

    protected new void Start() {
        base.Start();
        mesh = transform.FindChild("Mesh");
    }

    protected override void Move(Vector3 to) {
        end = moveDummy.position + to;
        mesh.LookAt(end);
        StartCoroutine(MoveTo(end));
    }
}