using System;
using UnityEngine;

public class PlayerMissle : MonoBehaviour {
    public float speed;
    public int damage;
    private static readonly String ENEMY = "Enemy";
    private static readonly String GATE = "Gate";

    void Move() {
        if (transform != null) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }

    void Update() {
        Move();
        Destroy(gameObject, 6f);
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag(ENEMY)) {
            var enemy = col.GetComponent<Enemy>();
            if (enemy) {
                Destroy(gameObject);
                enemy.Hit(damage);
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(GATE)) {
            var gate = collision.gameObject.GetComponent<Gate>();
            if (gate) {
                Destroy(gameObject);
                gate.Hit(damage);
            }
        }
        Destroy(gameObject);
    }
}