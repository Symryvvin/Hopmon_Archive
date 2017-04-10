using UnityEngine;

public class Player : MonoBehaviour {
    public LiveState liveState;
    private Vector3 startPoint;
    // Player keep reference on all player script Components
    [SerializeField] private PlayerMoveControll pMoveControll;
    [SerializeField] private PlayerFire pFire;
    [SerializeField] private Collector pCollector;

    public void ResetPlayer() {
        transform.position = startPoint + Vector3.up / 10f;
        transform.rotation = Quaternion.identity;
        liveState = LiveState.ALIVE;
        pMoveControll.Reset();
        pFire.Reset();
        pCollector.Reset();
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

    public void SetStart(Vector3 levelStart) {
        startPoint = levelStart;
    }
}