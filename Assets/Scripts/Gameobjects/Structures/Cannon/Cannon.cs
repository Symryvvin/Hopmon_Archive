public class Cannon : AbstractCannon {
    private const int count = 2;

    protected override void SetUpCannon() {
        var start = transform.position + transform.up / 2;
        var end = start + transform.forward * 50f;
        path = new LinePath(count, start, end);
        path.EvaluateWaypoints();
        InvokeRepeating("Shoot", firerate, firerate);
    }

    protected override MovePath SetPath() {
        return path;
    }

    public override void Shoot() {
        ActivateShell();
    }
}