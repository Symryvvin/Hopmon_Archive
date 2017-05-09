using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Managers.EventMessages;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Players {
    public class Collector : MonoBehaviour {
        private Transform playerBody;

        private readonly List<Transform> cristals = new List<Transform>();

        protected void Start() {
            playerBody = transform.FindChild("body").transform;
        }

        public void Reset() {
            StopAllCoroutines();
            cristals.Clear();
            foreach (Transform c in playerBody) {
                Destroy(c.gameObject);
            }
        }

        public void Pick(Transform cristalTransform) {
            cristals.Add(cristalTransform);
            cristalTransform.SetParent(playerBody);
            cristalTransform.position = new Vector3(playerBody.position.x,
                playerBody.position.y + cristals.Count - (cristals.Count - 0.8f) * 0.6f, playerBody.position.z);
            cristalTransform.GetComponent<Cristal>().Collect();
            EventMessenger<int>.TriggerEvent(GameEvents.CHANGE_SPEED, cristals.Count);
        }

        public void Release() {
            StartCoroutine(ReleaseCristals());
        }

        private IEnumerator ReleaseCristals() {
            while (true) {
                if (cristals.Count != 0) {
                    yield return new WaitForSeconds(0.83f);
                    var cristal = cristals[cristals.Count - 1];
                    if (cristal != null) {
                        cristal.GetComponent<Cristal>().Release();
                        cristals.Remove(cristal);
                        EventMessenger.TriggerEvent(GameEvents.UPDATE_CRISTAL_COUNT);
                        EventMessenger<int>.TriggerEvent(GameEvents.CHANGE_SPEED, cristals.Count);
                    }
                }
                yield return null;
            }
        }
    }
}