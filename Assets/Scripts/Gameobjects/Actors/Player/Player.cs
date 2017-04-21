using Assets.Scripts.Gameobjects.Game;
using UnityEngine;
using GameObject = UnityEngine.Object;

public class Player : MonoBehaviour{
    public LiveState liveState;
    private Vector3 startPoint;
    // Player keep reference on all player script Components
    [SerializeField] private PlayerMoveControll controll;
    [SerializeField] private PlayerFire shoot;
    [SerializeField] private Collector collector;

    public void ResetPlayer() {
        transform.position = startPoint + Vector3.up / 10f;
        transform.rotation = Quaternion.identity;
        liveState = LiveState.ALIVE;
        controll.Reset();
        shoot.Reset();
        collector.Reset();
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Enemy")) {
            ThouchEnemy();
        }
    }

    void ThouchEnemy() {
        liveState = LiveState.DEAD;
        EventManager.TriggerEvent(GameEvents.DEFEATE);
    }

    public void MoveToStart(Level level) {
        startPoint = level.start;
    }
}