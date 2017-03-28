using UnityEngine;

public class PlayerShell : Shell {
    public int damage;
    public AudioClip shot;
    public AudioClip hit;

    private const string ENEMY = "Enemy";
    private const string GATE = "Gate";

    public new void OnEnable() {
        base.OnEnable();
        if (path != null)
            AudioSource.PlayClipAtPoint(shot, transform.position);
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag(ENEMY)) {
            HitDestructableObject(col.gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag(GATE)) {
            HitDestructableObject(col.gameObject);
        }
    }

    private void HitDestructableObject(GameObject hitObject) {
        var desctuct = hitObject.GetComponent<IDestructable>();
        if (desctuct != null) {
            Destroy();
            desctuct.Hit(damage);
            AudioSource.PlayClipAtPoint(hit, transform.position);
        }
    }
}