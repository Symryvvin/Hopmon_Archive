using System.Collections;
using UnityEngine;

public class Geyser : MonoBehaviour {
    public float pause;

    private AudioSource audioSource;
    private float duration;
    private GameObject steam;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        steam = transform.FindChild("Steam").gameObject;
        steam.SetActive(false);
        duration = audioSource.clip.length;
    }

    void Start() {
        StartCoroutine(Eruption());
    }

    IEnumerator Eruption() {
        while (true) {
            yield return new WaitForSeconds(pause);
            steam.SetActive(true);
            audioSource.Play();
            yield return new WaitForSeconds(duration);
            steam.SetActive(false);
        }
    }
}