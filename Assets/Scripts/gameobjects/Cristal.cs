using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Cristal : MonoBehaviour, ICollectible {
    public AudioClip pick;
    public AudioClip release;

    private enum State {
        free,
        collected,
        released
    }
    [SerializeField]
    private State state;

    void Start() {
        state = State.free;
    }

    void Update() {
        switch (state) {
            case State.free:
                transform.Rotate(Vector3.up * 45f * Time.deltaTime);
                break;
            case State.collected:
                // nothing
                break;
            case State.released:
                if (transform.parent != null) {
                    transform.SetParent(null);
                    GetComponent<Collider>().enabled = false;
                }
                transform.Translate(Vector3.up * 3f * Time.deltaTime, Space.Self);
                transform.Rotate(Vector3.up * 180f * Time.deltaTime);
                Destroy(gameObject, 3f);
                break;
        }
    }

    public void Collect() {
        //	AudioSource.PlayClipAtPoint(pick, transform.position);
        state = State.collected;
    }

    public void Release() {
        //	AudioSource.PlayClipAtPoint(release, transform.position);
        state = State.released;
    }
}