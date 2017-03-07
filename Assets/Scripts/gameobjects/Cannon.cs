using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour, IShootable {
    public Transform missle;
    public float firerate;
    public AudioClip shoot;

    private Vector3 missleStart;

    void Start() {
        missleStart = transform.position + transform.up / 2;
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot() {
        while (true) {
            instanceMissle();

            AudioSource.PlayClipAtPoint(shoot, missleStart);
            yield return new WaitForSeconds(firerate);
        }
    }

    private void instanceMissle() {
        Instantiate(missle, missleStart, transform.rotation);
    }
}