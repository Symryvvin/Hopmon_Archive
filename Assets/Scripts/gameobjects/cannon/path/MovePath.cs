using System.Collections.Generic;
using UnityEngine;

public abstract class MovePath : List<Vector3> {
    protected MovePath(int capacity) : base(capacity) {
    }

    protected abstract Vector3 Evaluate(float t);

    public abstract MovePath EvaluateWaypoints();
}