using System.Collections;
using UnityEngine;

public class UISounds : MonoBehaviour {
    public AudioClip onSelect;
    public AudioClip onClick;
    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
        StartCoroutine(UnMute());
    }

    public bool isPlaying() {
        return source.isPlaying;
    }

    public void PlaySelect() {
        Play(onSelect);
    }

    public void PlayClick() {
        Play(onClick);
    }

    private void Play(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }

    private IEnumerator UnMute() {
        yield return new WaitForSeconds(0.2F);
        if (source.mute)
            source.mute = false;
        yield return null;
    }
}