using UnityEngine;

public class CubeMove : EnemyMove {
    private Transform mesh;
    public float rotateSpeed;

    protected new void Start() {
        base.Start();
        mesh = transform.FindChild("Mesh");
    }

    new void Update() {
        base.Update();
        switch (direction) {
            case MoveDirection.FORWARD:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.right) *
                                mesh.rotation;
                break;
            case MoveDirection.BACK:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.left) *
                                mesh.rotation;
                break;
            case MoveDirection.LEFT:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.forward) *
                                mesh.rotation;
                break;
            case MoveDirection.RIGHT:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.back) *
                                mesh.rotation;
                break;
        }
    }
}