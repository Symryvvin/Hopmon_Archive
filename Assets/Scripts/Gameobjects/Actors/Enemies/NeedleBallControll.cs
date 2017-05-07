using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Rules.Movement;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors.Enemies {
    public class NeedleBallControll : EnemyMove {
        public float rotateSpeed;

        private int yAngle;
        private Transform mesh;

        protected new void Start() {
            base.Start();
            mesh = transform.FindChild("Mesh");
            yAngle = (int) transform.rotation.eulerAngles.y;
            if (yAngle == 0) {
                direction = MoveDirection.FORWARD;
            }
            if (yAngle == 180) {
                direction = MoveDirection.BACK;
            }
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        protected override Node CalculateMoveDirection(IDictionary<Vector3, Node> directions) {
            // Сначала удаляем из словаря узлы, которые заблокированы другими врагами или заблокированы для врагов
            var copy = new Dictionary<Vector3, Node>(directions);
            foreach (var entry in copy) {
                if (entry.Value.isBlockedForEnemy())
                    directions.Remove(entry.Key);
            }
            Node forward;
            directions.TryGetValue(NodeDirection.FORWARD, out forward);
            Node back;
            directions.TryGetValue(NodeDirection.BACK, out back);
            Node left;
            directions.TryGetValue(NodeDirection.LEFT, out left);
            Node right;
            directions.TryGetValue(NodeDirection.RIGHT, out right);
            if (directions.Count != 1) {
                switch (direction) {
                case MoveDirection.FORWARD:
                    directions.Remove(NodeDirection.BACK);
                    directions.Remove(NodeDirection.LEFT);
                    directions.Remove(NodeDirection.RIGHT);
                    if (directions.Count == 0) {
                        if (left != null) {
                            directions.Add(NodeDirection.LEFT, left);
                        }
                        if (right != null) {
                            directions.Add(NodeDirection.RIGHT, right);
                        }
                    }
                    break;
                case MoveDirection.BACK:
                    directions.Remove(NodeDirection.FORWARD);
                    directions.Remove(NodeDirection.LEFT);
                    directions.Remove(NodeDirection.RIGHT);
                    if (directions.Count == 0) {
                        if (left != null) {
                            directions.Add(NodeDirection.LEFT, left);
                        }
                        if (right != null) {
                            directions.Add(NodeDirection.RIGHT, right);
                        }
                    }
                    break;
                case MoveDirection.LEFT:
                    directions.Remove(NodeDirection.RIGHT);
                    directions.Remove(NodeDirection.BACK);
                    directions.Remove(NodeDirection.FORWARD);
                    if (directions.Count == 0) {
                        if (forward != null) {
                            directions.Add(NodeDirection.FORWARD, forward);
                        }
                        if (back != null) {
                            directions.Add(NodeDirection.BACK, back);
                        }
                    }
                    break;
                case MoveDirection.RIGHT:
                    directions.Remove(NodeDirection.LEFT);
                    directions.Remove(NodeDirection.BACK);
                    directions.Remove(NodeDirection.FORWARD);
                    if (directions.Count == 0) {
                        if (forward != null) {
                            directions.Add(NodeDirection.FORWARD, forward);
                        }
                        if (back != null) {
                            directions.Add(NodeDirection.BACK, back);
                        }
                    }
                    break;
                }
            }
            return directions.Count != 0 ? directions.ElementAt(Random.Range(0, directions.Count)).Value : null;
        }

        new void Update() {
            base.Update();
            switch (direction) {
            case MoveDirection.FORWARD:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.right) *
                                mesh.rotation;
                break;
            case MoveDirection.BACK:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.left) *
                                mesh.rotation;
                break;
            case MoveDirection.LEFT:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.forward) *
                                mesh.rotation;
                break;
            case MoveDirection.RIGHT:
                mesh.rotation = Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.back) *
                                mesh.rotation;
                break;
            }
        }
    }
}