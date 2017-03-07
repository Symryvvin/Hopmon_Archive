using UnityEngine;
using System.Collections;

public class nBall : MonoBehaviour {
    public Vector3[] all_points;
    float speed = 2f, rotate_speed = 180f;
    float distance = 0.0f;
    int _currentPoint = 0;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        float _currentDistance = Vector3.Distance(transform.position, all_points[_currentPoint]);
        if (_currentDistance <= distance) {
            _currentPoint++;
            if (_currentPoint == all_points.Length)
                _currentPoint = 0;
        }
        //transform.LookAt (all_points [_currentPoint].position);
        transform.position = Vector3.MoveTowards(transform.position, all_points[_currentPoint], speed * Time.deltaTime);
        if (transform.position.z < all_points[_currentPoint].z) {
            transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotate_speed, Vector3.right) *
                                 transform.rotation;
        }
        if (transform.position.z > all_points[_currentPoint].z) {
            transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotate_speed, Vector3.left) * transform.rotation;
        }
        if (transform.position.x < all_points[_currentPoint].x) {
            transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotate_speed, Vector3.back) * transform.rotation;
        }
        if (transform.position.x > all_points[_currentPoint].x) {
            transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotate_speed, Vector3.forward) *
                                 transform.rotation;
        }
    }
}