using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCannon : MonoBehaviour, IShootable {
    public GameObject shell;
    public float firerate;
    public AudioClip shotSound;
    public int poolAmount;

    protected List<GameObject> pool;
    protected Vector3 startPoint;

    protected void Init() {
        pool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++) {
            GameObject obj = Instantiate(shell);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    protected void ActivateShell() {
        foreach (var o in pool) {
            if (!o.activeInHierarchy) {
                o.transform.position = startPoint;
                o.transform.rotation = transform.rotation;
                o.SetActive(true);
                break;
            }
        }
    }

    public abstract void Shoot();
}