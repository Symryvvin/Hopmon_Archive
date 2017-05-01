using System.Collections;
using Assets.Scripts.Gameobjects.Actors.Enemies;
using Assets.Scripts.Gameobjects.Actors.Movements;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
    public class Controll : MonoBehaviour {
        private GameCamera gameCamera;
        private Transform playerTransform;
        private Rigidbody playerRigidbody;
        private float moveSpeed = 2.5f;
        [SerializeField] private MoveState moveState;
        private bool boost;
        private Animator animator;
        private AudioSource audioSource;
        public Node current;

        void Start() {
            gameCamera = Camera.main.GetComponent<GameCamera>();
            playerRigidbody = GetComponent<Rigidbody>();
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

        public void Alive() {
            switch (moveState) {
            case MoveState.STAND:
                Idle();
                break;
            case MoveState.WALK:
                break;
            }
        }

        private void Idle() {
            float horizontal = (int) Input.GetAxisRaw("Horizontal");
            float vertical = (int) Input.GetAxisRaw("Vertical");
            if (gameCamera.moving == GameCamera.Move.STOP && !boost) {
                switch (gameCamera.point) {
                case CameraPoint.NORTH:
                    Move(vertical, horizontal);
                    break;
                case CameraPoint.WEST:
                    Move(-horizontal, vertical);
                    break;
                case CameraPoint.SOUTH:
                    Move(-vertical, -horizontal);
                    break;
                case CameraPoint.EAST:
                    Move(horizontal, -vertical);
                    break;
                }
            }
        }

        private void Move(float v, float h) {
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

        private bool Move(int x, int z) {
            Node end = current.GetNextNode(x, z);
            if (end == null || end.isBlocked()) {
                playerTransform.LookAt(current.position + new Vector3(x, 0f, z));
                return false;
            }
            playerTransform.LookAt(end.position + Vector3.up * 0.1f);
            StartCoroutine(MoveTo(end.position + Vector3.up * 0.1f));
            current = end;
            return true;
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

        public void MoveToStart(Vector3 levelStart) {
            transform.position = levelStart + Vector3.up * 0.1f;
            transform.rotation = Quaternion.identity;
        }
    }
}