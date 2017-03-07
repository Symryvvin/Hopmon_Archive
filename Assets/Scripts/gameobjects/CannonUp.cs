using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonUp : MonoBehaviour, IShootable {
    public bool debug;

    public Transform missle;
    public float firerate;

    private Transform missleInstance;
    private Vector3 missleStart;

    private Vector3 up = new Vector3(0, 2f, 0);

    public List<List<Vector3>> waypointsLists = new List<List<Vector3>>();

    public  List<Vector3> points2 = new List<Vector3>();
    public List<Vector3> points3 = new List<Vector3>();

    private int waypointsCount = 15;


    void Start() {
        EvaluateAllWaypoints();
        var cannon = transform.position;
        missleStart = transform.position + up;
        StartCoroutine(Shoot());
    }

    private void EvaluateP2P3() {
        waypointsLists.Clear();
        points2.Clear();
        points3.Clear();
        // for end point 0, 1
        points2.Add(transform.position + up + Vector3.forward);
        points3.Add(transform.position + Vector3.forward);
        // for end point 1, 1
        points2.Add(transform.position + up + Vector3.forward + Vector3.right);
        points3.Add(transform.position + Vector3.forward + Vector3.right);
        // for end point 1, 0
        points2.Add(transform.position + up + Vector3.right);
        points3.Add(transform.position + Vector3.right);
        // for end point 1, -1
        points2.Add(transform.position + up + Vector3.back + Vector3.right);
        points3.Add(transform.position + Vector3.back + Vector3.right);
        // for end point 0, -1
        points2.Add(transform.position + up + Vector3.back);
        points3.Add(transform.position + Vector3.back);
        // for end point -1, -1
        points2.Add(transform.position + up + Vector3.back + Vector3.left);
        points3.Add(transform.position + Vector3.back + Vector3.left);
        // for end point -1, 0
        points2.Add(transform.position + up + Vector3.left);
        points3.Add(transform.position + Vector3.left);
        // for end point -1, 1
        points2.Add(transform.position + up + Vector3.forward + Vector3.left);
        points3.Add(transform.position + Vector3.forward + Vector3.left);
    }

    public IEnumerator Shoot() {
        while (true) {
            InstanceMissle();
            yield return new WaitForSeconds(firerate);
        }
    }

    private void InstanceMissle() {
        missleInstance = Instantiate(missle, missleStart, transform.rotation);
        var script = missleInstance.GetComponent<CannonUpMissle>();
        int randomIndex = Random.Range(0, waypointsLists.Count);
        script.waypoints = new List<Vector3>(waypointsLists[randomIndex]);
    }

    public Vector3 Evaluate(float t, Vector3 P2, Vector3 P3) {
        Vector3 P0 = transform.position + up / 2;
        Vector3 P1 = transform.position + up;
        float t1 = 1 - t;
        return t1 * t1 * t1 * P0 + 3 * t * t1 * t1 * P1 +
               3 * t * t * t1 * P2 + t * t * t * P3;
    }

    public void OnDrawGizmos() {
        if (debug) {
        EvaluateP2P3();
        Gizmos.color = Color.green;
        for (int p = 0; p < 8; p++) {
            float floatCount = waypointsCount - 1;
            for (int i = 1; i < waypointsCount; i++) {
                float t = (i - 1f) / floatCount;
                float t1 = i / floatCount;
                Vector3 evaulatedT = Evaluate(t, points2[p], points3[p]);
                Vector3 evaulatedT1 = Evaluate(t1, points2[p], points3[p]);
                Gizmos.DrawLine(evaulatedT, evaulatedT1);
            }
        }
    }
}

    private void EvaluateAllWaypoints() {
        EvaluateP2P3();
        for (int p = 0; p < 8; p++) {
            var waypoints = new List<Vector3>();
            Vector3 evaulatedT = Vector3.zero;
            float floatCount = waypointsCount - 1;
            for (int i = 1; i < waypointsCount; i++) {
                float t = (i - 1f) / floatCount;
                evaulatedT = Evaluate(t, points2[p], points3[p]);
                waypoints.Add(evaulatedT);
            }
            waypoints.Add(Evaluate(waypointsCount / floatCount,  points2[p], points3[p]));
            waypointsLists.Add(waypoints);
        }
    }
}