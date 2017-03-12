using UnityEngine;

public class Gate : MonoBehaviour, IDestructable {
    public AudioClip hit;

    public void Hit(int damage) {
        AudioSource.PlayClipAtPoint(hit, transform.position);
        Dead();
    }

    public void Dead() {
        Destroy(gameObject);
    }
}