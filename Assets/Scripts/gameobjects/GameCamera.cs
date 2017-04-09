using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
    public Transform target;

    private enum Move {
        STOP,
        LEFT,
        RIGHT
    }

    private const float distance = 6f;
    private const float height = 8.4f;
    private Move moving = Move.STOP;
    private float rotY;
    private float angleMin;
    private float angleMax;
    private Vector3 offset;
    private Quaternion rotation;
    private Vector3 position, maxPos, minPos, targerPos;
    private float moveX;
    private float moveZ;

    public CameraPoint point;


    public void Init() {
        targerPos = target.transform.position;
        transform.position = new Vector3(targerPos.x, targerPos.y + height, targerPos.z - distance);
        offset = targerPos - transform.position;
        transform.LookAt(targerPos);
        angleMin = -90f;
        angleMax = 90f;
        rotY = 0;
    }


    void LateUpdate() {
        if (target != null) {
            targerPos = target.transform.position;
            switch ((int) transform.eulerAngles.y) {
                case 0:
                    transform.position = new Vector3(targerPos.x, targerPos.y + height, targerPos.z - distance);
                    point = CameraPoint.NORTH;
                    break;
                case 90:
                    transform.position = new Vector3(targerPos.x - distance, targerPos.y + height, targerPos.z);
                    point = CameraPoint.WEST;
                    break;
                case 180:
                    transform.position = new Vector3(targerPos.x, targerPos.y + height, targerPos.z + distance);
                    point = CameraPoint.SOUTH;
                    break;
                case 270:
                    transform.position = new Vector3(targerPos.x + distance, targerPos.y + height, targerPos.z);
                    point = CameraPoint.EAST;
                    break;
            }
        }


        if (LeftRotate()) {
            moving = Move.LEFT;
        }
        if (RightRotate()) {
            moving = Move.RIGHT;
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
        rotY += round * 1f;
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