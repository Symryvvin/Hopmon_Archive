using UnityEngine;

public abstract class AbstractShell : MonoBehaviour {
    private LayerMask block;

    void Start() {
        block = LayerMask.NameToLayer("BlockLayer");
        Init();
    }

    void OnEnable() {
        Invoke("Destroy", 6f);
    }

    protected void Destroy() {
        gameObject.SetActive(false);
    }

    protected void OnDisable() {
        CancelInvoke();
    }

    void Update() {
         Move();
    }

    protected abstract void Move();

    protected abstract void Init();

    void OnCollisionEnter(Collision col) {
        LayerMask colLayer = col.gameObject.layer;
        if (colLayer == block && col.gameObject.transform != transform.parent) {
            Destroy();
        }
    }
}