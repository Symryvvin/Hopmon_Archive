using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour, IResettable {
    public float speed;
    public AudioClip fireReady;
    public int percentReload;
    public GameObject shellPrefab;
    public float reloadTime;

    private Player player;
    private GameObject shellInstance;
    private PlayerShell shell;
    private ReloadState reloadState;

    public void Init() {
        player = GetComponent<Player>();
        shellInstance = Instantiate(shellPrefab);
        shellInstance.transform.SetParent(player.transform);
        shellInstance.SetActive(false);
        shell = shellInstance.GetComponent<PlayerShell>();
        Reload();
    }

    public void Reset() {
        Reload();
        EventManager.TriggerEvent("disCharging");
    }

    private IEnumerator Reload(float time) {
        while (reloadState == ReloadState.RELOAD) {
            time -= reloadTime / 10f;
            yield return new WaitForSeconds(reloadTime / 10f);
            EventManager.TriggerEvent("charging");
            percentReload = Mathf.RoundToInt(100f - time * 100f / reloadTime);
            if (percentReload >= 100) {
                AudioSource.PlayClipAtPoint(fireReady, Camera.main.transform.position);
                yield return new WaitForSeconds(0.2f);
                reloadState = ReloadState.READY;
            }
            yield return null;
        }
    }

    private void Reload() {
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
        EventManager.TriggerEvent("disCharging");
        Reload();
    }

    private bool IsReloaded() {
        return reloadState == ReloadState.READY;
    }

    private bool FirePressed() {
        return Input.GetButton("Fire");
    }
}