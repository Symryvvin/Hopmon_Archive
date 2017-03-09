using UnityEngine;

public class NeedleBallControll : EnemyMove {
    private int yAngle;
    private Transform mesh;
    public float rotateSpeed;

    protected override void Start() {
        base.Start();
        mesh = transform.FindChild("Mesh");
        yAngle = (int) transform.rotation.eulerAngles.y;
        if (yAngle == 0) {
            direction = MoveDirection.FORWARD;
        }
        if (yAngle == 180) {
            direction = MoveDirection.BACK;
        }
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    protected override Vector3 Direction(string s) {
        Vector3 next = Vector3.zero;
        if (s.Equals("")) {
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.forward;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.back;
                    break;
                case MoveDirection.RIGHT:
                    next = Vector3.right;
                    break;
                case MoveDirection.LEFT:
                    next = Vector3.left;
                    break;
            }
        }
        else if (s.Equals("R")) {
            // Z+ Z- X-
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.forward;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.back;
                    break;
            }
        }
        else if (s.Equals("L")) {
            // Z+ Z- X+
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.forward;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.back;
                    break;
            }
        }
        else if (s.Equals("B")) {
            // Z+ X+ X-
            switch (direction) {
                case MoveDirection.RIGHT:
                    next = Vector3.right;
                    break;
                case MoveDirection.LEFT:
                    next = Vector3.left;
                    break;
            }
        }
        else if (s.Equals("F")) {
            // Z- X+ X-
            switch (direction) {
                case MoveDirection.RIGHT:
                    next = Vector3.right;
                    break;
                case MoveDirection.LEFT:
                    next = Vector3.left;
                    break;
            }
        }
        else if (s.Equals("LR")) {
            // коридор Z+ Z-
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.forward;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.back;
                    break;
            }
        }
        else if (s.Equals("FB")) {
            // коридор X+ X-
            switch (direction) {
                case MoveDirection.RIGHT:
                    next = Vector3.right;
                    break;
                case MoveDirection.LEFT:
                    next = Vector3.left;
                    break;
            }
        }
        else if (s.Equals("BL")) {
            // угол Z+ X+
            switch (direction) {
                case MoveDirection.BACK:
                    next = Vector3.right;
                    break;
                case MoveDirection.LEFT:
                    next = Vector3.forward;
                    break;
                case MoveDirection.FORWARD:
                    next = Vector3.forward;
                    break;
            }
        }
        else if (s.Equals("FL")) {
            // угол Z- X+
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.right;
                    break;
                case MoveDirection.LEFT:
                    next = Vector3.back;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.back;
                    break;
            }
        }
        else if (s.Equals("BR")) {
            // угол Z+ X-
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.forward;
                    break;
                case MoveDirection.RIGHT:
                    next = Vector3.forward;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.left;
                    break;
            }
        }
        else if (s.Equals("FR")) {
            // угол Z- X-
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = Vector3.left;
                    break;
                case MoveDirection.RIGHT:
                    next = Vector3.back;
                    break;
                case MoveDirection.BACK:
                    next = Vector3.back;
                    break;
            }
        }
        else if (s.Equals("BLR")) {
            next = Vector3.forward;
        }
        else if (s.Equals("FLR")) {
            next = Vector3.back;
        }
        else if (s.Equals("FBR")) {
            next = Vector3.left;
        }
        else if (s.Equals("FBL")) {
            next = Vector3.right;
        }
        else if (s.Equals("FBLR")) {
        }

        return next;
    }

    void Update() {
        base.Update();
        switch (direction) {
            case MoveDirection.FORWARD:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.right) *
                                mesh.rotation;
                break;
            case MoveDirection.BACK:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.left) *
                                mesh.rotation;
                break;
            case MoveDirection.LEFT:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.forward) *
                                mesh.rotation;
                break;
            case MoveDirection.RIGHT:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.back) *
                                mesh.rotation;
                break;
        }
    }
}