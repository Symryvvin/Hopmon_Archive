using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameobjects.Level;
using UnityEngine;
using Random = System.Random;
using GameObject = UnityEngine.GameObject;

namespace Assets.Scripts.Gameobjects.Actors {
    public class Node : ICloneable {
        public NodeType type;
        public Vector3 position;
        public IDictionary<string, Node> directions;
        public Movement graph;
        private Node clone;
        private GameObject draw;

        public Node(Tile tile) {
            position = tile.position;
            type = NodeUtils.GetTypeByName(tile.name);
        }

        public object Clone() {
            return MemberwiseClone();
        }

        public void CalculateDirections() {
            directions = new Dictionary<string, Node>();
            AddValueToDictionary(NodeDirection.FORWARD);
            AddValueToDictionary(NodeDirection.BACK);
            AddValueToDictionary(NodeDirection.LEFT);
            AddValueToDictionary(NodeDirection.RIGHT);
            clone = (Node) Clone();
        }

        private void AddValueToDictionary(string key) {
            Node node = GetNearNode(graph, NodeUtils.KeyToPosition(key));
            if (node != null) {
                directions.Add(key, node);
            }
        }

        private Node GetNearNode(Movement graph, Vector3 dir) {
            var pos = position + dir;
            foreach (var node in graph.nodes) {
                if (node.position == pos) {
                    if (node.type == NodeType.NORMAL || node.type == NodeType.BOOSTER) {
                        return node;
                    }
                }
            }
            return null;
        }

        public void SwitchType(NodeType newType) {
            type = newType;
        }

        public void Restore() {
            type = clone.type;
            directions = clone.directions;
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

        private Node ReturnIfNotNull(string key) {
            if (directions.ContainsKey(key))
                return directions[key];
            return null;
        }

        public Vector3 GetRandomDirection() {
            Random rand = new Random();
            return directions.ElementAt(rand.Next(0, directions.Count)).Value.position;
        }

        public void Draw() {
            draw = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Transform ctx = draw.transform;
            ctx.localScale = Vector3.one * 0.2f;
            ctx.position = position + (Vector3.up * 1.5f);
            if (type == NodeType.BLOCKED) {
                ctx.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }

        public void SetColor(Color color) {
            draw.GetComponent<MeshRenderer>().material.color = color;
        }

        public void Erase() {
            GameObject.Destroy(draw);
        }


    }
}