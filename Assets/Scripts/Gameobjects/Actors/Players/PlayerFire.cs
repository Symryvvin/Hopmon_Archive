﻿using System.Collections;
using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Managers.EventMessages;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
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

        public void Init() {

        }

        public void Reset() {
            Reload();
            EventMessenger.TriggerEvent(GameEvents.DISCHARGE);
        }

        private IEnumerator Reload(float time) {
            while (reloadState == ReloadState.RELOAD) {
                time -= reloadTime / 10f;
                yield return new WaitForSeconds(reloadTime / 10f);
                EventMessenger.TriggerEvent(GameEvents.CHARGE);
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
            var start = transform.position + transform.up / 2 + transform.forward * 0.3f;
            shell.path = new LinePath(2, start, start + transform.forward * 30f).EvaluateWaypoints();
            shellInstance.SetActive(true);
            EventMessenger.TriggerEvent(GameEvents.DISCHARGE);
            Reload();
        }

        private bool IsReloaded() {
            return reloadState == ReloadState.READY;
        }

        private bool FirePressed() {
            return Input.GetButton("Fire");
        }
    }
}