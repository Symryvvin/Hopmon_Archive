using UnityEngine;

public class Shell : AbstractShell {
    public float speed;

    protected override void Move() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    protected override void Init() {
    }
}