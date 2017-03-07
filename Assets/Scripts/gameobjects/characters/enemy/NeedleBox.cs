using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NeedleBox : MonoBehaviour {
    public AudioClip dropSound;
    public bool dropBox;
    private IEnumerator coroutine;

    private Vector3 up;
    private Vector3 down;
    public float downSpeed = 2.5f;
    public float upSpeed = 0.67f;

    private Transform box;

    void Awake() {
        box = transform;
    }

    void Start() {
        float x = box.position.x;
        float z = box.position.z;
        up = new Vector3(x, 1.1f, z);
        down = new Vector3(x, 0.1f, z);
        if (dropBox)
            coroutine = Drop();
        else
            coroutine = UpDown();
        StartCoroutine(coroutine);
    }


    IEnumerator Drop() {
        bool drop = false;
        while (true) {
            RaycastHit info;
            Debug.DrawRay(box.position, -box.up * 1.0F, Color.red);
            if (Physics.Raycast(box.position, -box.up, out info, 1.0F)) {
                if (info.collider.gameObject.CompareTag("Player")) {
                    yield return new WaitForSeconds(2f);
                    drop = true;
                }
            }
            if (drop) {
                while (isGoal(box.position, down)) {
                    box.position = Vector3.MoveTowards(box.position, down, downSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            yield return null;
        }
    }

    IEnumerator UpDown() {
        while (true) {
            yield return new WaitForSeconds(2f);
            while (isGoal(box.position, down)) {
                box.position = Vector3.MoveTowards(box.position, down, downSpeed * Time.deltaTime);
                yield return null;
            }
            AudioSource.PlayClipAtPoint (dropSound, transform.position);
            yield return new WaitForSeconds(2f);
            while (box.position.y < 1.1f) {
                box.position = Vector3.MoveTowards(box.position, up, upSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private bool isGoal(Vector3 position, Vector3 endPosition) {
        return Vector3.SqrMagnitude(position) - Vector3.SqrMagnitude(endPosition) > float.Epsilon;
    }
}