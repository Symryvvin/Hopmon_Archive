using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
    public  Transform target;

    enum Move {
        Stop,
        Left,
        Right
    };

    float _dist = 4f;
    float _hight = 6.4f;
    Move moving = Move.Stop;
    float rotY;
    float angleMin;
    float angleMax;
    Vector3 _offset;
    Quaternion rotation;
    Vector3 position, maxPos, minPos, targerPos;
    float _moveX;
    float _moveZ;

    public CameraPoint point;


    void Start() {
        targerPos = target.transform.position;
        transform.position = new Vector3(targerPos.x, targerPos.y + _hight, targerPos.z - _dist);
        _offset = targerPos - transform.position;
        transform.LookAt(targerPos);
        angleMin = -90f;
        angleMax = 90f;
        rotY = 0;
    }


    void LateUpdate() {
        targerPos = target.transform.position;
        switch ((int) transform.eulerAngles.y) {
            case 0:
                transform.position = new Vector3(targerPos.x, targerPos.y + _hight, targerPos.z - _dist);
                point = CameraPoint.NORTH;
                break;
            case 90:
                transform.position = new Vector3(targerPos.x - _dist, targerPos.y + _hight, targerPos.z);
                point = CameraPoint.WEST;
                break;
            case 180:
                transform.position = new Vector3(targerPos.x, targerPos.y + _hight, targerPos.z + _dist);
                point = CameraPoint.SOUTH;
                break;
            case 270:
                transform.position = new Vector3(targerPos.x + _dist, targerPos.y + _hight, targerPos.z);
                point = CameraPoint.EAST;
                break;
        }


        if (Input.GetKeyDown(KeyCode.T) && moving == Move.Stop) {
            moving = Move.Left;
        }
        if (Input.GetKeyDown(KeyCode.R) && moving == Move.Stop) {
            moving = Move.Right;
        }
        switch (moving) {
            case Move.Left:
                CameraRotate(-1);
                break;
            case Move.Right:
                CameraRotate(1);
                break;
            case Move.Stop:
                break;
        }
        maxPos = CalculatePosition(angleMax);
        minPos = CalculatePosition(angleMin);
    }

    private Vector3 CalculatePosition(float angle) {
        return targerPos - Quaternion.Euler(0, angle, 0) * _offset;
    }

    private void CameraRotate(int round) {
        rotY += round * 1f;
        rotY = Mathf.Clamp(rotY, angleMin, angleMax);
        rotation = Quaternion.Euler(0, rotY, 0);
        position = targerPos - rotation * _offset;
        transform.position = position;
        transform.LookAt(targerPos);
        if (transform.position == maxPos || transform.position == minPos) {
            moving = Move.Stop;
            angleMax += round * 90f;
            angleMin += round * 90f;
        }
    }
}