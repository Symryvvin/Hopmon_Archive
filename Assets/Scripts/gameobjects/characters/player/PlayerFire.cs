using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour {
    public float speed;
    public AudioClip fireReady;
    public int percentReload;
    public GameObject shellPrefab;
    public float reloadTime;

    private Player player;
    private GameObject shellInstance;
    private PlayerShell shell;
    private ReloadState reloadState;

    void Start() {
        player = GetComponent<Player>();
        shellInstance = Instantiate(shellPrefab);
        shellInstance.SetActive(false);
        shell = shellInstance.GetComponent<PlayerShell>();
        Reload();
    }

    private IEnumerator Reload(float time) {
        while (reloadState == ReloadState.RELOAD) {
            if (time > 0) {
                time -= Time.deltaTime;
                percentReload = Mathf.RoundToInt(100f - time * 100f / reloadTime);
            }
            if (percentReload >= 100) {
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
        shell.speed = speed;
        var start = transform.position + transform.up / 2;
        shell.path = new LinePath(2, start, start + transform.forward * 30f).EvaluateWaypoints();
        shellInstance.SetActive(true);
        Reload();
    }

    private bool IsReloaded() {
        return reloadState == ReloadState.READY;
    }

    private bool FirePressed() {
        return Input.GetButton("Fire");
    }
}