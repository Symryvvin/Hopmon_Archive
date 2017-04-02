using UnityEngine;

public class PlayerShell : Shell {
    public int damage;
    public AudioSource shot;
    public AudioSource hit;

    private const string ENEMY = "Enemy";
    private const string GATE = "Gate";

    public new void OnEnable() {
        base.OnEnable();
        hit.transform.SetParent(transform);
        if (path != null)
           shot.Play();
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag(ENEMY)) {
            HitDestructableObject(col.gameObject);
        }
    }

    new void OnCollisionEnter(Collision col) {
        base.OnCollisionEnter(col);
        if (col.gameObject.CompareTag(GATE)) {
            HitDestructableObject(col.gameObject);
        }
    }

    private void HitDestructableObject(GameObject hitObject) {
        var desctuct = hitObject.GetComponent<IDestructable>();
        if (desctuct != null) {
            hit.transform.SetParent(null);
            hit.Play();
            Destroy();
            desctuct.Hit(damage);
        }
    }
}