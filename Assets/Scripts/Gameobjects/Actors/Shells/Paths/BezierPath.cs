using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MovePath {
    private readonly Vector3 p0;
    private readonly Vector3 p1;
    private readonly Vector3 p2;
    private readonly Vector3 p3;

    public BezierPath(int capacity, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) : base(capacity) {
        this.p0 = p0;
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    protected override Vector3 Evaluate(float t) {
        float t1 = 1 - t;
        return t1 * t1 * t1 * p0 + 3 * t * t1 * t1 * p1 +
               3 * t * t * t1 * p2 + t * t * t * p3;
    }

    public override MovePath EvaluateWaypoints() {
        float floatCount = Capacity - 1;
        for (int i = 1; i < Capacity; i++) {
            float t = (i - 1f) / floatCount;
            Add(Evaluate(t));
        }
        Add(Evaluate(Capacity / floatCount));
        return this;
    }

    public static List<Vector3> EvaluatePoints(int size, Vector3 point) {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                if (i == 0 || j == 0 || i == size - 1 || j == size - 1) {
                    list.Add(new Vector3(point.x + j - size/2, point.y, point.z + i - size/2));
                }
            }
        }
        return list;
    }
}