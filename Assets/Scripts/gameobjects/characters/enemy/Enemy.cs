using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestructable {
    [SerializeField] private int hitPoint;
    public AudioClip hit;

    public void Hit(int damage) {
        hitPoint = hitPoint - damage;
        AudioSource.PlayClipAtPoint(hit, transform.position);
        if (hitPoint == 0)
            Dead();
    }

    public void Dead() {
        Destroy(gameObject);
    }
}
