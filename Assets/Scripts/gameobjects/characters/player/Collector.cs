using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    private PlayerMoveControll controll;
    private Transform playerBody;

    public AudioClip pickCristall;
    public AudioClip releaseCristall;

    private readonly List<Transform> cristalls = new List<Transform>();

    void Start() {
        controll = GetComponent<PlayerMoveControll>();
        playerBody = transform.FindChild("body").transform;
    }

    void PickCristall(Transform cristalTransform) {
        cristalls.Add(cristalTransform);
        cristalTransform.SetParent(playerBody);
        cristalTransform.position = new Vector3(playerBody.position.x,
            playerBody.position.y + cristalls.Count - (cristalls.Count - 1.5f) * 0.6f, playerBody.position.z);
        cristalTransform.GetComponent<Cristal>().Collect();
        controll.ChangeSpeed(cristalls.Count);
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Collectible")) {
            AudioSource.PlayClipAtPoint(pickCristall, transform.position);
            PickCristall(collider.transform);
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.CompareTag("WarpZone")) {
            //StartCourutine
            foreach (var cristal in cristalls) {
                cristal.GetComponent<Cristal>().Release();
            }
            controll.ChangeSpeed(cristalls.Count);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.CompareTag("WarpZone")) {
            //StopCourutine
        }
    }

}
