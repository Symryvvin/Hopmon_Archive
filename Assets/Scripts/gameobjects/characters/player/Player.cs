using UnityEngine;

public class Player : MonoBehaviour {
    public LiveState liveState;
    private Collector collector;

    public void Init() {
        collector = GetComponent<Collector>();
        collector.Init();
    }

    public void ResetPlayer(Vector3 startPoint) {
        transform.position = startPoint + Vector3.up / 10f;
        transform.rotation = Quaternion.identity;
        GetComponent<PlayerFire>().Reload();
        liveState = LiveState.ALIVE;
        collector.ClearCollectedCristals();
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Enemy")) {
            ThouchEnemy();
        }
    }

    void ThouchEnemy() {
        liveState = LiveState.DEAD;
        EventManager.TriggerEvent("loseGame");
    }
}