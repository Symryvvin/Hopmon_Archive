using UnityEngine;

public abstract class AbstractCannonMissle : MonoBehaviour {
    private LayerMask block;

    void Start() {
        block = LayerMask.NameToLayer("BlockLayer");
        Init();
    }

    void Update() {
        if (gameObject != null) {
            Move();
            Destroy(gameObject, 6f);
        }
    }

    protected abstract void Move();

    protected abstract void Init();

    void OnCollisionEnter(Collision col) {
        LayerMask colLayer = col.gameObject.layer;
        if (colLayer == block && col.gameObject.transform != transform.parent) {
            Destroy(gameObject);
        }
    }
}