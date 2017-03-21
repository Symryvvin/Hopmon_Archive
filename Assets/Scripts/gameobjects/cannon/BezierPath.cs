using System.Collections.Generic;
using UnityEngine;

public class BezierPath : List<Vector3> {
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

    private Vector3 Evaluate(float t) {
        float t1 = 1 - t;
        return t1 * t1 * t1 * p0 + 3 * t * t1 * t1 * p1 +
               3 * t * t * t1 * p2 + t * t * t * p3;
    }

    public BezierPath EvaluateAllWaypoints() {
        float floatCount = Capacity - 1;
        for (int i = 1; i < Capacity; i++) {
            float t = (i - 1f) / floatCount;
            Add(Evaluate(t));
        }
        Add(Evaluate(Capacity / floatCount));
        return this;
    }
}