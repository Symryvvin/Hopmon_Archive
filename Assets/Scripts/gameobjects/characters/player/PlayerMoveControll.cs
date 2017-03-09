using System.Collections;
using UnityEngine;

public class PlayerMoveControll : MonoBehaviour {
    private Player player;
    private Transform playerTransform;
    private Rigidbody rigidbody;
    private float moveSpeed = 2.5f;
    private Animator animator;
    public AudioClip jump;
    AudioSource player_jump;


    [SerializeField] bool boost;


    // Камера
    public GameCamera camera;


    private MoveState moveState;

    void Start() {
        playerTransform = transform;
        // playerTransform.position = start;
        player = GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //	player_jump = GetComponent<AudioSource> ();
    }


    void OnDisable() {
        transform.eulerAngles = Vector3.zero;
    }

    void OnEnable() {
        moveState = MoveState.STAND;
        //  playerTransform.position = start;
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

    void Dead() {
        transform.Rotate(Vector3.left * 1600F * Time.deltaTime);
        Transform _transform = transform;
        if (_transform.position.y < 3f) {
            _transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
            Destroy(gameObject, 2f);
        }
    }

    private bool CheckMove() {
        return !NextBlock() && NextGround();
    }

    private bool NextGround() {
        bool ground = false;
        Ray ray = new Ray(transform.position + transform.forward + transform.up, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.05F)) {
            var layer = hit.collider.gameObject.layer;
            if (layer == LayerMask.NameToLayer("GroundLayer")) {
                ground = true;
            }
        }
        return ground;
    }

    private bool NextBlock() {
        bool block = false;
        Ray ray = new Ray(transform.position + transform.up / 2, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.0F)) {
            var layer = hit.collider.gameObject.layer;
            if (layer == LayerMask.NameToLayer("BlockLayer")) {
                block = true;
            }
        }
        return block;
    }

    void Idle() {
        float horizontal = (int) Input.GetAxisRaw("Horizontal");
        float vertical = (int) Input.GetAxisRaw("Vertical");
        switch (camera.point) {
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
        if (h != 0) {
            v = 0;
        }
        if (h != 0 || v != 0) {
            if (Move((int) h, (int) v)) {
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
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
            var moveTo = Vector3.MoveTowards(rigidbody.position, position, (boost ? 5f : moveSpeed) * Time.deltaTime);
            rigidbody.MovePosition(moveTo);
            distance = (playerTransform.position - position).sqrMagnitude;
            yield return null;
        }
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
                Dead();
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
        Move((int)end.x, (int)end.z);
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