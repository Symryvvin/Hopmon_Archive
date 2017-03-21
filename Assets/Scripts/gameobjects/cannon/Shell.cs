using UnityEngine;

public class Shell : AbstractShell {
    public float speed;

    protected override void Move() {
        if (transform != null) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }

    protected override void Init() {
    }
}