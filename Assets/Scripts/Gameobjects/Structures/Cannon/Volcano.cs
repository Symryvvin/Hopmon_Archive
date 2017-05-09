using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Structures.Cannon {
    public class Volcano : AbstractCannon {
        public bool debug;

        private List<MovePath> waypointsLists;
        private const int count = 25;

        protected override void SetUpCannon() {
            waypointsLists = new List<MovePath>(16);
            Vector3 p0 = transform.position + Vector3.up;
            Vector3 p1 = transform.position + Vector3.up * 4f;
            List<Vector3> p2 = BezierPath.EvaluatePoints(5, transform.position + Vector3.up * 3f);
            List<Vector3> p3 = BezierPath.EvaluatePoints(5, transform.position);
            for (int i = 0; i < waypointsLists.Capacity; i++) {
                waypointsLists.Add(new BezierPath(count, p0, p1, p2[i], p3[i]).EvaluateWaypoints());
            }
            InvokeRepeating("Shoot", firerate, firerate);
        }

        protected override MovePath SetPath() {
            return waypointsLists[Random.Range(0, waypointsLists.Count)];
        }

        private IEnumerator Eruption() {
            int shellLeft = poolAmount;
            while (shellLeft > 0) {
                shellLeft--;
                ActivateShell();
                yield return new WaitForSeconds(0.3f);
            }
        }

        public override void Shoot() {
            StartCoroutine(Eruption());
        }

        public void OnDrawGizmos() {
            if (debug) {
                Gizmos.color = Color.green;
                foreach (var p in waypointsLists) {
                    for (int i = 1; i < p.Count - 1; i++)
                        Gizmos.DrawLine(p[i], p[i + 1]);
                }
            }
        }
    }
}
