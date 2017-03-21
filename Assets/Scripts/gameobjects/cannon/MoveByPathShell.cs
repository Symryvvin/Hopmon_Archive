using UnityEngine;

public class MoveByPathShell : AbstractShell {
    public float speed;
    public BezierPath waypoints;
    public int currentPoint;

    protected override void Init() {
        transform.position = waypoints[0];
    }

    protected void OnDisable() {
        base.OnDisable();
        currentPoint = 0;
    }

    protected override void Move() {
        float currentDistance = Vector3.Distance(transform.position, waypoints[currentPoint]);
        if (currentDistance <= float.Epsilon) {
            currentPoint++;
            if (currentPoint == waypoints.Count)
                Destroy();
        }
        if (currentPoint < waypoints.Count)
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint],
                speed * Time.deltaTime);
    }
}