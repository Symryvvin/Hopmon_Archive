using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour {
    private Player player;
    public int percentReload;
    [SerializeField] private float reloadTime;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip fireReady;
    [SerializeField] private Transform missle;
    private ReloadState reloadState;

    void Start() {
        player = GetComponent<Player>();
        Reload();
    }

    private IEnumerator Reload(float time) {
        while (reloadState == ReloadState.RELOAD) {
            if (time > 0) {
                time -= Time.deltaTime;
                percentReload = Mathf.RoundToInt(100f - time * 100f / reloadTime);
            }
            if (percentReload == 100) {
                AudioSource.PlayClipAtPoint(fireReady, transform.position);
                yield return new WaitForSeconds(0.2f);
                reloadState = ReloadState.READY;
            }
            yield return null;
        }
    }

    public void Reload() {
        StopAllCoroutines();
        reloadState = ReloadState.RELOAD;
        StartCoroutine(Reload(reloadTime));
    }

    void Update() {
        if (player.liveState == LiveState.ALIVE) {
            Fire();
        }
    }

    private void Fire() {
        if (!FirePressed() || !IsReloaded()) return;
        Instantiate(missle, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
        Reload();
    }

    private bool IsReloaded() {
        return reloadState == ReloadState.READY;
    }

    private bool FirePressed() {
        return Input.GetAxis("Fire") == 1;
    }
}