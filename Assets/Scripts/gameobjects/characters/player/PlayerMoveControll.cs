using System.Collections;
using UnityEngine;

public class PlayerMoveControll : MonoBehaviour, IResettable {
    private GameCamera gameCamera;
    private Player player;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;
    private float moveSpeed = 2.5f;
    [SerializeField] private MoveState moveState;
    private bool boost;
    private Animator animator;
    private AudioSource audioSource;

    void Awake() {
        gameCamera = Camera.main.GetComponent<GameCamera>();
        playerRigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameCamera.target = transform;
        gameCamera.Init();
        moveState = MoveState.STAND;
        playerTransform = transform;
    }

    public void Reset() {
        ChangeSpeed(0);
        StopAllMoves();
    }

    void Alive() {
        switch (moveState) {
            case MoveState.STAND:
                Idle();
                break;
            case MoveState.WALK:
                break;
        }
    }

    private bool CheckMove() {
        return !NextBlock() && NextGround();
    }

    private bool NextGround() {
        var ray = new Ray(transform.position + transform.forward + transform.up, -transform.up);
        return Physics.Raycast(ray, 1.05F, 1 << 9);
    }

    private bool NextBlock() {
        Ray ray = new Ray(transform.position + transform.up / 2, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.0F, 1 << 8)) {
            return !hit.collider.name.Contains("InvisibleWall");
        }
        return false;
    }

    void Idle() {
        float horizontal = (int) Input.GetAxisRaw("Horizontal");
        float vertical = (int) Input.GetAxisRaw("Vertical");
        switch (gameCamera.point) {
            case CameraPoint.NORTH:
                Controll(vertical, horizontal);
                break;
            case CameraPoint.WEST:
                Controll(-horizontal, vertical);
                break;
            case CameraPoint.SOUTH:
                Controll(-vertical, -horizontal);
                break;
            case CameraPoint.EAST:
                Controll(horizontal, -vertical);
                break;
        }
    }

    private void Controll(float v, float h) {
        if (gameCamera.moving == GameCamera.Move.STOP) {
            if (h != 0) {
                v = 0;
            }
            if (h != 0 || v != 0) {
                if (Move((int) h, (int) v)) {
                    audioSource.Play();
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                }
            }
        }
    }

    private bool Move(int xDir, int zDir) {
        Vector3 current = playerTransform.position;
        Vector3 end = current + new Vector3(xDir, 0f, zDir);
        playerTransform.LookAt(end);
        if (CheckMove()) {
            StartCoroutine(MoveTo(end));
            return true;
        }
        return false;
    }

    private IEnumerator MoveTo(Vector3 position) {
        moveState = MoveState.WALK;
        float distance = (playerTransform.position - position).sqrMagnitude;
        while (distance > float.Epsilon) {
            var moveTo = Vector3.MoveTowards(playerRigidbody.position, position,
                (boost ? 5f : moveSpeed) * Time.deltaTime);
            playerRigidbody.MovePosition(moveTo);
            distance = (playerTransform.position - position).sqrMagnitude;
            yield return null;
        }
        StopAllMoves();
    }

    private void StopAllMoves() {
        moveState = MoveState.STAND;
        boost = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
    }

    void Update() {
        switch (player.liveState) {
            case LiveState.ALIVE:
                Alive();
                break;
            case LiveState.DEAD:
              //  Dead();
                break;
        }
    }

    public void ChangeSpeed(int cristalCount) {
        if (cristalCount > 0) {
            moveSpeed = 30.0f / (cristalCount + 12.0f);
        }
        else {
            moveSpeed = 2.5f;
        }
        animator.speed = moveSpeed / 2.5f;
    }

    private Vector3 GetNextPoint(Booster b) {
        print(b.direction);
        switch (b.direction) {
            case MoveDirection.FORWARD:
                return Vector3.forward;
            case MoveDirection.BACK:
                return Vector3.back;
            case MoveDirection.LEFT:
                return Vector3.left;
            case MoveDirection.RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }

    private bool Boost(Vector3 end) {
        Move((int) end.x, (int) end.z);
        return true;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Booster")) {
            var booster = collider.GetComponent<Booster>();
            var end = GetNextPoint(booster);
            StartCoroutine(WaitForStand(end));
        }
    }

    private IEnumerator WaitForStand(Vector3 end) {
        while (true) {
            if (moveState == MoveState.STAND) {
                boost = Boost(end);
                break;
            }
            yield return null;
        }
    }
}