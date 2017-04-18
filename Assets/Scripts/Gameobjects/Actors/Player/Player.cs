using UnityEngine;

public class Player : MonoBehaviour {
    public LiveState liveState;
    // Player keep reference on all player script Components
    [SerializeField] private PlayerMoveControll pMoveControll;
    [SerializeField] private PlayerFire pFire;
    [SerializeField] private Collector pCollector;

    public void InitPlayer() {
        pMoveControll.Init();
        pFire.Init();
        pCollector.Init();
        EventManager.StartListener("Reset", ResetPlayer);
    }

    private void ResetPlayer() {
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

}