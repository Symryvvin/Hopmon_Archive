using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public LiveState liveState;

    private GameCamera gameCamera;

    void Awake() {
        gameCamera = FindObjectOfType(typeof(GameCamera)) as GameCamera;
        if (gameCamera != null) {
            gameCamera.target = transform;
            GetComponent<PlayerMoveControll>().gameCamera = gameCamera;
        }

        liveState = LiveState.ALIVE;
    }

    public void ResetPlayer(Vector3 startPoint) {
        transform.position = startPoint + Vector3.up / 10f;
        transform.rotation = Quaternion.identity;
        GetComponent<PlayerFire>().Reload();
    }

    void ThouchEnemy() {
        liveState = LiveState.DEAD;
    }
}