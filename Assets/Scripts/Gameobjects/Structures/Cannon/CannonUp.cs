using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Structures.Cannon {
    public class CannonUp : AbstractCannon {
        public bool debug;

        private List<MovePath> waypointsLists;
        private const int count = 15;

        protected override void SetUpCannon() {
            waypointsLists = new List<MovePath>(8);
            Vector3 p0 = transform.position + Vector3.up;
            Vector3 p1 = transform.position + Vector3.up * 2;
            List<Vector3> p2 = BezierPath.EvaluatePoints(3, transform.position + Vector3.up * 2);
            List<Vector3> p3 = BezierPath.EvaluatePoints(3, transform.position);
            for (int i = 0; i < waypointsLists.Capacity; i++) {
                waypointsLists.Add(new BezierPath(count, p0, p1, p2[i], p3[i]).EvaluateWaypoints());
            }
            InvokeRepeating("Shoot", firerate, firerate);
        }

        protected override MovePath SetPath() {
            return waypointsLists[Random.Range(0, waypointsLists.Count)];
        }

        public override void Shoot() {
            ActivateShell();
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