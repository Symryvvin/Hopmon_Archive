using UnityEngine;

public class Cannon : AbstractCannon {
    void Start() {
        startPoint = transform.position + transform.up / 2;
        InvokeRepeating("Shoot", 0f, firerate);
    }

    public override void Shoot() {
            InstanceMissle();
            AudioSource.PlayClipAtPoint(shotSound, startPoint);
    }

    private void InstanceMissle() {
        Instantiate(shell, startPoint, transform.rotation).SetParent(transform);
    }
}