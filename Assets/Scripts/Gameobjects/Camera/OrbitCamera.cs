using UnityEngine;

public class OrbitCamera : MonoBehaviour {
    public float rotateSpeed;
    public bool isRotate;
    private Vector3 initPosition;

    void Start() {
        initPosition = transform.position;
        transform.LookAt(Vector3.zero);
    }

    void LateUpdate() {
        if (isRotate)
            transform.RotateAround(Vector3.zero, Vector3.down, rotateSpeed * Time.deltaTime);
        else
            Reset();
    }

    private void Reset() {
        transform.position = initPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.LookAt(Vector3.zero);
    }
}