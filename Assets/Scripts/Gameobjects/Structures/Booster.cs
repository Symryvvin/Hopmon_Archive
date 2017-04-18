using UnityEngine;

public class Booster : MonoBehaviour {
    public MoveDirection direction;

    void Start() {
        var y = (int) transform.eulerAngles.y;
        if (y == 0) {
            direction = MoveDirection.BACK;
        }
        if (y == 180) {
            direction = MoveDirection.FORWARD;
        }
        if (y == 90) {
            direction = MoveDirection.LEFT;
        }
        if (y == 270) {
            direction = MoveDirection.RIGHT;
        }
    }
}