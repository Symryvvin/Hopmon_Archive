using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour {
    private Player player;
    private bool isReloaded;
    public int percentReload;
    [SerializeField] private float reloadTime;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip fireReady;
    [SerializeField] private Transform missle;

    void Start() {
        player = GetComponent<Player>();
        StartCoroutine(Reload(reloadTime));
    }

    IEnumerator Reload(float time) {
        while (!isReloaded) {
            if (time > 0) {
                time -= Time.deltaTime;
                percentReload = Mathf.RoundToInt(100f - time * 100f / reloadTime);
                isReloaded = percentReload == 100;
            }
            if (isReloaded)
                AudioSource.PlayClipAtPoint(fireReady, transform.position);
            yield return null;
        }
    }

    void Update() {
        if (player.liveState == LiveState.ALIVE) {
            Fire();
        }
    }

    void Fire() {
        if (FirePressed() && isReloaded) {
            Instantiate(missle, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            isReloaded = false;
            StartCoroutine(Reload(reloadTime));
        }
    }

    private bool FirePressed() {
        return Input.GetAxis("Fire") == 1;
    }
}