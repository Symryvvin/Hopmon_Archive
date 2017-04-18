using UnityEngine;

public class LinePath : MovePath {
    private readonly Vector3 p0;
    private readonly Vector3 p1;

    public LinePath(int capacity, Vector3 p0, Vector3 p1) : base(capacity) {
        this.p0 = p0;
        this.p1 = p1;
    }

    protected override Vector3 Evaluate(float t) {
        return Vector3.zero;
    }

    public override MovePath EvaluateWaypoints() {
        Add(p0);
        Add(p1);
        return this;
    }
}
