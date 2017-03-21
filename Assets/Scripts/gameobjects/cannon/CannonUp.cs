using System.Collections.Generic;
using UnityEngine;

public class CannonUp : AbstractCannon {
    public bool debug;

    private readonly List<BezierPath> waypointsLists = new List<BezierPath>();
    private const int count = 15;

    void Start() {
        Vector3 z = transform.position;
        Vector3 up = Vector3.up * 2;
        Vector3 p0 = z + up / 2;
        Vector3 p1 = z + up;
        Vector3 f = Vector3.forward;
        Vector3 b = Vector3.back;
        Vector3 r = Vector3.right;
        Vector3 l = Vector3.left;
        // for end point 0, 1
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + f, z + f));
        // for end point 1, 1
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + f + r, z + f + r));
        // for end point 1, 0
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + r, z + r));
        // for end point 1, -1
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + b + r, z + b + r));
        // for end point 0, -1
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + b, z + b));
        // for end point -1, -1
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + b + l, z + b + l));
        // for end point -1, 0
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + l, z + l));
        // for end point -1, 1
        waypointsLists.Add(new BezierPath(count, p0, p1, z + up + f + l, z + f + l));
        foreach (var path in waypointsLists) {
            path.EvaluateAllWaypoints();
        }
        startPoint = p1;
        InvokeRepeating("Shoot", 0f, firerate);
    }


    public override void Shoot() {
            InstanceMissle();
    }

    private void InstanceMissle() {
        var missleInstance = Instantiate(shell, startPoint, transform.rotation);
        missleInstance.SetParent(transform);
        AudioSource.PlayClipAtPoint(shotSound, transform.position);
        var script = missleInstance.GetComponent<MoveByPathShell>();
        int randomIndex = Random.Range(0, waypointsLists.Count);
        script.waypoints = new List<Vector3>(waypointsLists[randomIndex]);
    }

    public void OnDrawGizmos() {
        if (debug) {
            Gizmos.color = Color.green;
            foreach (var path in waypointsLists) {
                for (int i = 1; i < path.Count - 1; i++)
                    Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}