using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public Transform moveDummy;
    public MoveState moveState;
    public MoveDirection direction;
    public string schema;

    protected Vector3 end;
    protected readonly List<Vector3> moves = new List<Vector3>();

    private Rigidbody enemyRigidbody;
    private LayerMask blockLayer;
    private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;

    protected void Start() {
        moveState = MoveState.STAND;
        direction = MoveDirection.STOP;
        blockLayer = 1 << LayerMask.NameToLayer("BlockLayer") | 1 << LayerMask.NameToLayer("EnemyLayer");
        groundLayer = 1 << LayerMask.NameToLayer("GroundLayer");
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    protected void Update() {
        switch (moveState) {
            case MoveState.STAND:
                CalculateMoveDirection();
                break;
            case MoveState.WALK:
                CurrentDirection();
                break;
        }
    }

    protected void CalculateMoveDirection() {
        Ray forwardRay = new Ray(moveDummy.position + moveDummy.up / 2, moveDummy.forward);
        Ray backRay = new Ray(moveDummy.position + moveDummy.up / 2, -moveDummy.forward);
        Ray rightRay = new Ray(moveDummy.position + moveDummy.up / 2, moveDummy.right);
        Ray leftRay = new Ray(moveDummy.position + moveDummy.up / 2, -moveDummy.right);

        Ray forwardFloorRay = new Ray(moveDummy.position + moveDummy.up + moveDummy.forward, -moveDummy.up);
        Ray backFloorRay = new Ray(moveDummy.position + moveDummy.up - moveDummy.forward, -moveDummy.up);
        Ray rightFloorRay = new Ray(moveDummy.position + moveDummy.up + moveDummy.right, -moveDummy.up);
        Ray leftFloorRay = new Ray(moveDummy.position + moveDummy.up - moveDummy.right, -moveDummy.up);

        schema = GetSchemaPart(forwardRay, forwardFloorRay, "F") +
                 GetSchemaPart(backRay, backFloorRay, "B") +
                 GetSchemaPart(leftRay, leftFloorRay, "L") +
                 GetSchemaPart(rightRay, rightFloorRay, "R");

        Move(Direction(schema));
    }

    protected string GetSchemaPart(Ray forward, Ray onGround, string litera) {
        if (!NextBlock(forward) && NextGround(onGround)) {
            return "";
        }
        return litera;
    }

    protected bool NextGround(Ray ray) {
        return Physics.Raycast(ray, 1.05F, groundLayer);
    }

    protected bool NextBlock(Ray ray) {
        return Physics.Raycast(ray, 1.0F, blockLayer);
    }

    protected Vector3 RandomMove(string m) {
        if (m.Contains("F"))
            moves.Add(Vector3.forward);
        if (m.Contains("B"))
            moves.Add(Vector3.back);
        if (m.Contains("L"))
            moves.Add(Vector3.left);
        if (m.Contains("R"))
            moves.Add(Vector3.right);
        return moves[Random.Range(0, moves.Count)];
    }

    protected virtual Vector3 Direction(string s) {
        Vector3 next = Vector3.zero;
        if (s.Equals("")) {
            // ""открытое пространство
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = RandomMove("FLR");
                    break;
                case MoveDirection.RIGHT:
                    next = RandomMove("FBR");
                    break;
                case MoveDirection.LEFT:
                    next = RandomMove("FBL");
                    break;
                case MoveDirection.BACK:
                    next = RandomMove("BLR");
                    break;
                case MoveDirection.STOP:
                    next = RandomMove("FBLR");
                    break;
            }
        }
        else if (s.Equals("R")) {
            // Z+ Z- X-
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = RandomMove("FL");
                    break;
                case MoveDirection.RIGHT:
                    next = RandomMove("FB");
                    break;
                case MoveDirection.BACK:
                    next = RandomMove("BL");
                    break;
                case MoveDirection.STOP:
                    next = RandomMove("FBL");
                    break;
            }
        }
        else if (s.Equals("L")) {
            // Z+ Z- X+
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = RandomMove("FR");
                    break;
                case MoveDirection.LEFT:
                    next = RandomMove("FB");
                    break;
                case MoveDirection.BACK:
                    next = RandomMove("BR");
                    break;
                case MoveDirection.STOP:
                    next = RandomMove("FBR");
                    break;
            }
        }
        else if (s.Equals("B")) {
            // Z+ X+ X-
            switch (direction) {
                case MoveDirection.RIGHT:
                    next = RandomMove("FR");
                    break;
                case MoveDirection.LEFT:
                    next = RandomMove("FL");
                    break;
                case MoveDirection.BACK:
                    next = RandomMove("LR");
                    break;
                case MoveDirection.STOP:
                    next = RandomMove("FLR");
                    break;
            }
        }
        else if (s.Equals("F")) {
            // Z- X+ X-
            switch (direction) {
                case MoveDirection.FORWARD:
                    next = RandomMove("LR");
                    break;
                case MoveDirection.RIGHT:
                    next = RandomMove("BR");
                    break;
                case MoveDirection.LEFT:
                    next = RandomMove("BL");
                    break;
                case MoveDirection.STOP:
                    next = RandomMove("BLR");
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
                case MoveDirection.STOP:
                    next = RandomMove("FB");
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
                case MoveDirection.STOP:
                    next = RandomMove("LR");
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
                case MoveDirection.STOP:
                    next = RandomMove("FR");
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
                case MoveDirection.STOP:
                    next = RandomMove("BR");
                    break;
            }
        }
        else if (s.Equals("BR")) {
            // угол Z+ X-
            switch (direction) {
                case MoveDirection.BACK:
                    next = Vector3.left;
                    break;
                case MoveDirection.RIGHT:
                    next = Vector3.forward;
                    break;
                case MoveDirection.STOP:
                    next = RandomMove("FL");
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
                case MoveDirection.STOP:
                    next = RandomMove("BL");
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
        moves.Clear();
        return next;
    }

    protected void CurrentDirection() {
        if (moveDummy.position.x < end.x)
            direction = MoveDirection.RIGHT;
        if (moveDummy.position.x > end.x)
            direction = MoveDirection.LEFT;
        if (moveDummy.position.z > end.z)
            direction = MoveDirection.BACK;
        if (moveDummy.position.z < end.z)
            direction = MoveDirection.FORWARD;
    }

    protected virtual void Move(Vector3 to) {
        end = moveDummy.position + to;
        StartCoroutine(MoveTo(end));
    }

    protected IEnumerator MoveTo(Vector3 position) {
        float distance = (moveDummy.position - position).sqrMagnitude;
        while (distance > float.Epsilon) {
            transform.position = Vector3.MoveTowards(enemyRigidbody.position, position, moveSpeed * Time.deltaTime);
            moveState = MoveState.WALK;
            distance = (moveDummy.position - position).sqrMagnitude;
            yield return null;
        }
        moveState = MoveState.STAND;
    }
}