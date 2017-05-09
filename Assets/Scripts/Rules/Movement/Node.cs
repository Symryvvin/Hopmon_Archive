using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Levels;
using UnityEngine;

namespace Assets.Scripts.Rules.Movement {
    public class Node {
        public NodeType type;
        public Vector3 position;
        public IDictionary<Vector3, Node> directions;
        private GameObject draw;
        private NodeType toRestore;

        public Node(Tile tile) {
            toRestore = NodeType.NO_TYPE;
            position = tile.position;
            directions = new Dictionary<Vector3, Node>();
        }

        public void AddValueToDictionary(Vector3 key, Node node) {
            if (node.position == position + key) {
                directions.Add(key, node);
            }
        }

        public bool isBlockedForEnemy() {
            return isBlocked() || type == NodeType.BLOCKED_FOR_ENEMY;
        }

        public bool isBlocked() {
            return type == NodeType.BLOCKED;
        }

        public void ChangeType(NodeType newType) {
            type = newType;
            if (toRestore == NodeType.NO_TYPE)
                toRestore = type;
            ColoredByType();
        }

        public void ChangeTypeToRestore(NodeType newType) {
            toRestore = newType;
        }

        public void RestoreType() {
            type = toRestore;
            ColoredByType();
        }

        private void ColoredByType() {
            switch (type) {
            case NodeType.BLOCKED:
                SetColor(Color.red);
                break;
            case NodeType.BLOCKED_FOR_ENEMY:
                SetColor(Color.yellow);
                break;
            case NodeType.NORMAL:
                SetColor(Color.white);
                break;
            case NodeType.BOOSTER:
                SetColor(Color.blue);
                break;
            }
        }

        public Node GetNextNode(int x, int z) {
            if (x == 0 && z == 1)
                return ReturnIfNotNull(NodeDirection.FORWARD);
            if (x == 0 && z == -1)
                return ReturnIfNotNull(NodeDirection.BACK);
            if (x == -1 && z == 0)
                return ReturnIfNotNull(NodeDirection.LEFT);
            if (x == 1 && z == 0)
                return ReturnIfNotNull(NodeDirection.RIGHT);
            return null;
        }

        private Node ReturnIfNotNull(Vector3 key) {
            if (directions.ContainsKey(key))
                return directions[key];
            return null;
        }

        public void Draw() {
            draw = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            draw.layer = 11;
            Transform ctx = draw.transform;
            ctx.localScale = Vector3.one * 0.2f;
            ctx.position = position + Vector3.up * 0.05f;
            ColoredByType();
        }

        public void SetColor(Color color) {
            if (draw != null) {
                draw.GetComponent<MeshRenderer>().material.color = color;
            }
        }

        public void Erase() {
            GameObject.Destroy(draw);
        }
    }
}