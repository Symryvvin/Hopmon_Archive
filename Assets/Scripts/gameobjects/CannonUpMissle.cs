using System.Collections.Generic;
using UnityEngine;

public class CannonUpMissle : AbstractCannonMissle {
    public float speed;
    public List<Vector3> waypoints;
    private int currentPoint = 0;

    protected override void Init() {
        transform.position = waypoints[0];
    }

    protected override void Move() {
        float _currentDistance = Vector3.Distance(transform.position, waypoints[currentPoint]);
        if (_currentDistance <= float.Epsilon) {
            currentPoint++;
            if (currentPoint == waypoints.Count)
                Destroy(gameObject);
        }
        if (currentPoint < waypoints.Count)
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint],
                speed * Time.deltaTime);
    }
}