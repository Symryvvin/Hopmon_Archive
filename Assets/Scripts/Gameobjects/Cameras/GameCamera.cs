using UnityEngine;

namespace Assets.Scripts.Gameobjects.Cameras {
    public class GameCamera : MonoBehaviour {
        public Transform target;

        public enum Move {
            STOP,
            LEFT,
            RIGHT
        }

        private float distance = 6f;
        private float height = 7.4f;
        public Move moving = Move.STOP;
        private float rotY;
        private float angleMin;
        private float angleMax;
        private Vector3 offset;
        private Quaternion rotation;
        private Vector3 position, maxPos, minPos, targerPos;
        private float moveX;
        private float moveZ;
        private Camera camera;
        private Quaternion startRotation;

        public CameraLook look;
        public bool blockDisable;
        public bool nodeDisable;

        protected void Start() {
            camera = GetComponent<Camera>();
            camera.cullingMask &= ~(1 << LayerMask.NameToLayer("NodeLayer"));
            blockDisable = false;
            nodeDisable = true;
            startRotation = transform.rotation;
        }

        public void Init() {
            targerPos = target.transform.position;
            Reset();
            offset = targerPos - transform.position;
            transform.LookAt(targerPos);
        }

        public void Reset() {
            transform.position = new Vector3(targerPos.x, targerPos.y + height, targerPos.z - distance);
            angleMin = -90f;
            angleMax = 90f;
            transform.rotation = startRotation;
        }

        protected void Update() {
            targerPos = target.transform.position;
            switch (Mathf.RoundToInt(transform.eulerAngles.y)) {
            case 0:
                look = CameraLook.NORTH;
                break;
            case 90:
                look = CameraLook.WEST;
                break;
            case 180:
                look = CameraLook.SOUTH;
                break;
            case 270:
                look = CameraLook.EAST;
                break;
            }
        }

        protected void LateUpdate() {
            if (target != null) {
                targerPos = target.transform.position;
                switch (look) {
                case CameraLook.NORTH:
                    transform.position = new Vector3(targerPos.x, targerPos.y + height, targerPos.z - distance);
                    break;
                case CameraLook.WEST:
                    transform.position = new Vector3(targerPos.x - distance, targerPos.y + height, targerPos.z);
                    break;
                case CameraLook.SOUTH:
                    transform.position = new Vector3(targerPos.x, targerPos.y + height, targerPos.z + distance);
                    break;
                case CameraLook.EAST:
                    transform.position = new Vector3(targerPos.x + distance, targerPos.y + height, targerPos.z);
                    break;
                }
            }


            if (LeftRotate()) {
                moving = Move.LEFT;
                look = CameraLook.UNSTABLE;
            }
            if (RightRotate()) {
                moving = Move.RIGHT;
                look = CameraLook.UNSTABLE;
            }

            switch (moving) {
            case Move.LEFT:
                CameraRotate(1);
                break;
            case Move.RIGHT:
                CameraRotate(-1);
                break;
            case Move.STOP:
                break;
            }
            maxPos = CalculatePosition(angleMax);
            minPos = CalculatePosition(angleMin);

            blockDisable = ChangeCullingMask(KeyCode.Alpha1, "BlockLayer", blockDisable);
            nodeDisable = ChangeCullingMask(KeyCode.Alpha2, "NodeLayer", nodeDisable);

            if (Input.GetKeyDown(KeyCode.F)) {
                height = 2f;
                transform.position = targerPos;
                transform.rotation = target.rotation;
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                height = 8.4f;
            }
        }

        private bool ChangeCullingMask(KeyCode code, string layerName, bool disable) {
            if (Input.GetKeyDown(code)) {
                if (disable) {
                    camera.cullingMask |= 1 << LayerMask.NameToLayer(layerName);
                    return false;
                }
                camera.cullingMask &= ~(1 << LayerMask.NameToLayer(layerName));
                return true;
            }
            return disable;
        }

        private bool LeftRotate() {
            return moving == Move.STOP && Input.GetButtonDown("CameraRotationLeft");
        }

        private bool RightRotate() {
            return moving == Move.STOP && Input.GetButtonDown("CameraRotationRight");
        }

        private Vector3 CalculatePosition(float angle) {
            return targerPos - Quaternion.Euler(0, angle, 0) * offset;
        }

        private void CameraRotate(int round) {
            rotY += round * 1.5f;
            rotY = Mathf.Clamp(rotY, angleMin, angleMax);
            rotation = Quaternion.Euler(0, rotY, 0);
            position = targerPos - rotation * offset;
            transform.position = position;
            transform.LookAt(targerPos);
            if (transform.position == maxPos || transform.position == minPos) {
                moving = Move.STOP;
                angleMax += round * 90f;
                angleMin += round * 90f;
            }
        }
    }
}