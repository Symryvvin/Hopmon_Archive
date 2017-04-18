using UnityEngine;

public class Gate : MonoBehaviour, IDestructable {
    public void Hit(int damage) {
        Dead();
    }

    public void Dead() {
        Destroy(gameObject);
    }
}