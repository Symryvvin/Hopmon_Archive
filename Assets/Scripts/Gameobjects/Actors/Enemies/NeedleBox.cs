using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class NeedleBox : MonoBehaviour {
        public bool dropBox;
        public float downSpeed = 2.5f;
        public float upSpeed = 0.67f;

        private AudioSource audioSource;
        private IEnumerator coroutine;
        private Vector3 up;
        private Vector3 down;
        private Transform box;

        protected void Awake() {
            box = transform;
            audioSource = GetComponent<AudioSource>();
        }

        protected void Start() {
            float x = box.position.x;
            float z = box.position.z;
            up = new Vector3(x, 1.6f, z);
            down = new Vector3(x, 0.6f, z);
            if (dropBox)
                coroutine = Drop();
            else
                coroutine = UpDown();
            StartCoroutine(coroutine);
        }


        private IEnumerator Drop() {
            bool drop = false;
            while (box.position != down) {
                RaycastHit info;
                Debug.DrawRay(box.position, -box.up * 1.0F, Color.red);
                if (Physics.Raycast(box.position, -box.up, out info, 1.0F)) {
                    if (info.collider.gameObject.CompareTag("Player")) {
                        yield return new WaitForSeconds(2f);
                        drop = true;
                    }
                }
                if (drop) {
                    while (isGoal(down)) {
                        box.position = Vector3.MoveTowards(box.position, down, downSpeed * Time.deltaTime);
                        yield return null;
                    }
                    audioSource.Play();
                }
                yield return null;
            }
        }

        private IEnumerator UpDown() {
            while (true) {
                yield return new WaitForSeconds(2f);
                while (isGoal(down)) {
                    box.position = Vector3.MoveTowards(box.position, down, downSpeed * Time.deltaTime);
                    yield return null;
                }
                audioSource.Play();
                yield return new WaitForSeconds(2f);
                while (isGoal(up)) {
                    box.position = Vector3.MoveTowards(box.position, up, upSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }

        private bool isGoal(Vector3 position) {
            return (box.position - position).sqrMagnitude > float.Epsilon;
        }
    }
}