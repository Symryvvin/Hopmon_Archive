using UnityEngine;

public abstract class AbstractCannon : MonoBehaviour, IShootable {
    public Transform shell;
    public float firerate;
    public AudioClip shotSound;
    public int poolAmount;

    protected Vector3 startPoint;

    public abstract void Shoot();
}
