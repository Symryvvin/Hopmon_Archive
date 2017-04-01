using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCannon : MonoBehaviour, IShootable {
    public GameObject shellPrefab;
    public float firerate;
    public float shellSpeed;
    public int poolAmount;

    protected List<GameObject> shellPool;
    protected MovePath path;

    private AudioSource audioSource;

    protected void Start() {
        audioSource = GetComponent<AudioSource>();
        SetUpCannon();
        CreatePool();
    }

    protected abstract void SetUpCannon();

    private void CreatePool() {
        shellPool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++) {
            var shell = Instantiate(shellPrefab);
            shell.transform.SetParent(transform);
            shell.SetActive(false);
            shellPool.Add(shell);
        }
    }

    protected void ActivateShell() {
        foreach (var shell in shellPool) {
            if (!shell.activeInHierarchy) {
                audioSource.Play();
                shell.GetComponent<Shell>().path = SetPath();
                shell.GetComponent<Shell>().speed = shellSpeed;
                shell.SetActive(true);
                break;
            }
        }
    }

    protected abstract MovePath SetPath();

    public abstract void Shoot();
}