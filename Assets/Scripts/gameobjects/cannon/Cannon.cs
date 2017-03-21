using UnityEngine;

public class Cannon : AbstractCannon {
    void Start() {
        startPoint = transform.position + transform.up / 2;
        Init();
        InvokeRepeating("Shoot", 0f, firerate);
    }

    public override void Shoot() {
            ActivateShell();
            AudioSource.PlayClipAtPoint(shotSound, startPoint);
    }
}