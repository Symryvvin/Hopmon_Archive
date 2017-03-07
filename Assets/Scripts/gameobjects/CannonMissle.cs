using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class CannonMissle : AbstractCannonMissle {
    public float speed;

    protected override void Move() {
        if (transform != null) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }

    protected override void Init() {
    }
}