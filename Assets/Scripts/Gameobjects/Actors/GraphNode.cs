using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameobjects.Level;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Gameobjects.Actors {
    public class GraphNode : ICloneable {
        private GraphNodeType type;
        public Vector3 position;
        public IDictionary<string, GraphNode> directions;
        private MotionGraph graph;
        private GraphNode clone;

        public GraphNode(Tile tile, MotionGraph graph) {
            position = tile.position;
            type = SetType(tile.name);
            this.graph = graph;
        }

        public object Clone() {
            return MemberwiseClone();
        }

        public void CalculateDirections() {
            directions = new Dictionary<string, GraphNode>();
            AddValueToDictionary(NodeDirection.FORWARD);
            AddValueToDictionary(NodeDirection.BACK);
            AddValueToDictionary(NodeDirection.LEFT);
            AddValueToDictionary(NodeDirection.RIGHT);
            clone = (GraphNode) Clone();
        }

        private void AddValueToDictionary(string key) {
            GraphNode node = GetNearNode(graph, KeyToPosition(key));
            if (node != null) {
                directions.Add(key, node);
            }
        }

        private Vector3 KeyToPosition(string key) {
            switch (key) {
            case NodeDirection.FORWARD:
                return Vector3.forward;
            case NodeDirection.BACK:
                return Vector3.back;
            case NodeDirection.LEFT:
                return Vector3.left;
            case NodeDirection.RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
            }
        }

        private GraphNode GetNearNode(MotionGraph graph, Vector3 dir) {
            var pos = position + dir;
            foreach (var node in graph.nodes) {
                if (node.position == pos) {
                    if (node.type == GraphNodeType.NORMAL || node.type == GraphNodeType.BOOSTER)
                        return node;
                }
            }
            return null;
        }

        public void SwitchType(GraphNodeType newType) {
            type = newType;
        }

        public void Restore() {
            type = clone.type;
            directions = clone.directions;
        }


        public Vector3 GetRandomDirection() {
            Random rand = new Random();
            return directions.ElementAt(rand.Next(0, directions.Count)).Value.position;
        }

        public void Draw() {
            Transform ctx = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            ctx.localScale = Vector3.one * 0.2f;
            ctx.position = position + (Vector3.up * 1.5f);
            if (type == GraphNodeType.BLOCKED) {
                ctx.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }

        public void Erase() {
        }

        private GraphNodeType SetType(string name) {
            if (name.Equals("Floor") || name.Equals("Bridge") || name.Equals("Warp")) {
                return GraphNodeType.NORMAL;
            }
            if (name.Equals("Booster")) {
                return GraphNodeType.BOOSTER;
            }
            return GraphNodeType.BLOCKED;
        }


    }
}