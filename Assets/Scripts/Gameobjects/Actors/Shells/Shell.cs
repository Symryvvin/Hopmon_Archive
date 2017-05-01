using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour, IPoolable {
    public MovePath path;
    [HideInInspector] public float speed;
    private int index;
    private int block;

    void Start() {
        block = LayerMask.NameToLayer("BlockLayer");
    }

    public void OnEnable() {
        index = 0;
        if (path != null) {
            transform.position = path[index];
            StartCoroutine(Move());
            Invoke("Destroy", 6f);
        }
    }

    public void Destroy() {
        gameObject.SetActive(false);
    }

    public void OnDisable() {
        StopAllCoroutines();
        CancelInvoke();
    }

    IEnumerator Move() {
        while (gameObject.activeSelf) {
            float currentDistance = (transform.position - path[index]).sqrMagnitude;
            //do not work with float.Epsilon!
            if (currentDistance - float.Epsilon < 0.001f) {
                index++;
                if (index == path.Count)
                    Destroy();
            }
            if (index < path.Count)
                transform.position = Vector3.MoveTowards(transform.position, path[index],
                    speed * Time.deltaTime);
            yield return null;
        }
    }

    protected void OnCollisionEnter(Collision col) {
        int colLayer = col.gameObject.layer;
        if (colLayer == block && col.gameObject.transform != transform.parent) {
            Destroy();
        }
    }
}