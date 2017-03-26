using System.Collections;
using UnityEngine;

public class Geyser : MonoBehaviour {
    public AudioClip geyserSound;
    public float pause;

    private float duration;


    private GameObject steam;

    void Awake() {
        steam = transform.FindChild("Steam").gameObject;
        steam.SetActive(false);
        duration = geyserSound.length;
    }

    void Start() {
        StartCoroutine(Eruption());
    }

    IEnumerator Eruption() {
        while (true) {
            yield return new WaitForSeconds(pause);
            steam.SetActive(true);
            AudioSource.PlayClipAtPoint(geyserSound, transform.position);
            yield return new WaitForSeconds(duration);
            steam.SetActive(false);
        }
    }
}