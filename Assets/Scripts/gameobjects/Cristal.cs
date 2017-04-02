using UnityEngine;

public class Cristal : MonoBehaviour, ICollectible {
    public AudioClip pick; // sound of pick cristal
    public AudioClip release; // sound of release cristal

    private enum State {
        FREE,
        COLLECTED,
        RELEASE
    } // cristal states

    [SerializeField] private State state; // initial state

    /// <summary>
    /// For each frame do something depending in the cristal state
    /// </summary>
    void Update() {
        switch (state) {
            case State.FREE:
                transform.Rotate(Vector3.up * 45f * Time.deltaTime);
                break;
            case State.COLLECTED:
                // nothing
                break;
            case State.RELEASE:
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

    /// <summary>
    /// Call if cristal was collected
    /// </summary>
    public void Collect() {
        AudioSource.PlayClipAtPoint(pick, Camera.main.transform.position);
        state = State.COLLECTED;
    }

    /// <summary>
    /// Call if cristall was released
    /// </summary>
    public void Release() {
        AudioSource.PlayClipAtPoint(release, Camera.main.transform.position);
        state = State.RELEASE;
    }
}