using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Games;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
    public class Collector : MonoBehaviour {
        private Controll controll; // PlayerMove instance
        private Transform playerBody; // transform of Hopmon "body" mesh

        private readonly List<Transform> cristals = new List<Transform>(); // list of cristall which was collected

        /// <summary>
        /// Initialise all components on Start
        /// </summary>
        void Start() {
            controll = GetComponent<Controll>();
            playerBody = transform.FindChild("body").transform;
        }

        public void Reset() {
            cristals.Clear();
            foreach (Transform c in playerBody) {
                Destroy(c.gameObject);
            }
        }

        /// <summary>
        /// Picking cristal. Add to list "cristals" and set parent of player body mesh. Also change player speed
        /// </summary>
        /// <param name="cristalTransform">transform of picked cristal</param>
        void PickCristall(Transform cristalTransform) {
            cristals.Add(cristalTransform);
            cristalTransform.SetParent(playerBody);
            cristalTransform.position = new Vector3(playerBody.position.x,
                playerBody.position.y + cristals.Count - (cristals.Count - 0.8f) * 0.6f, playerBody.position.z);
            cristalTransform.GetComponent<Cristal>().Collect();
            controll.ChangeSpeed(cristals.Count);
           // Messenger<int>.Broadcast("dsdsds", cristals.Count);
        }

        /// <summary>
        /// Enter triggers
        /// </summary>
        /// <param name="col">collider of enter object</param>
        void OnTriggerEnter(Collider col) {
            if (col.CompareTag("Collectible")) {
                PickCristall(col.transform);
            }
            if (col.CompareTag("WarpZone")) {
                StartCoroutine(ReleaseCristals());
            }
        }

        /// <summary>
        /// Coroutine which start when trigger tag is WarpZone. Wait 0.83 seconds, set last cristal state "RELEASE",
        /// decrement count of cristall in Game manager. Also change player speed
        /// </summary>
        /// <returns>IEnumerator</returns>
        private IEnumerator ReleaseCristals() {
            while (true) {
                if (cristals.Count != 0) {
                    yield return new WaitForSeconds(0.83f);
                    var cristal = cristals[cristals.Count - 1];
                    if (cristal != null) {
                        cristal.GetComponent<Cristal>().Release();
                        cristals.Remove(cristal);
                        EventManager.TriggerEvent(GameEvents.WARP_CRISTAL);
                        controll.ChangeSpeed(cristals.Count);
                    }
                }
                yield return null;
            }
        }

        /// <summary>
        /// Stop all coroutines in this csript if player exit from triggers
        /// </summary>
        /// <param name="col">collider of exit object</param>
        void OnTriggerExit(Collider col) {
            if (col.CompareTag("WarpZone")) {
                StopAllCoroutines();
            }
        }
    }
}