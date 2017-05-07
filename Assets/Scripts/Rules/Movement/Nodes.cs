using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Levels;
using UnityEngine;

namespace Assets.Scripts.Rules.Movement {
    public class Nodes : List<Node> {
        private static IDictionary<string, NodeType> namedTypes = new Dictionary<string, NodeType> {
            {"Wall", NodeType.BLOCKED},
            {"DoubleWall", NodeType.BLOCKED},
            {"Gate", NodeType.BLOCKED},
            {"Cannon", NodeType.BLOCKED},
            {"CannonUp", NodeType.BLOCKED},
            {"Pole", NodeType.BLOCKED},
            {"Volcano", NodeType.BLOCKED_AROUND},
            {"Arrow", NodeType.BOOSTER},
            {"Floor", NodeType.NORMAL},
            {"Warp", NodeType.NORMAL},
            {"Bridge", NodeType.NORMAL},
            {"Geyser", NodeType.NORMAL},
            {"Cristal", NodeType.NORMAL},
            {"NeedleBox", NodeType.BLOCKED_FOR_ENEMY},
            {"NeedleBoxDrop", NodeType.BLOCKED_FOR_ENEMY},
            {"InvisibleWall", NodeType.BLOCKED_FOR_ENEMY}
        };

        private void CreateNodes(Tiles tiles) {
            foreach (var tile in tiles.parts) {
                if (!namedTypes.ContainsKey(tile.name)) return;
                var type = namedTypes[tile.name];
                if (type != NodeType.BLOCKED) {
                    var node = new Node(tile);
                    node.ChangeType(type);
                    if (type == NodeType.BOOSTER) {
                        node.ChangeTypeToRestore(type);
                    }
                    Add(node);
                }
            }
            foreach (var node in this) {
                ChangeType(node, tiles.structures);
                ChangeType(node, tiles.enemies);
            }
        }

        private void ChangeType(Node node, Tile[] tiles) {
            foreach (var tile in tiles) {
                if (tile.position == node.position) {
                    if (!namedTypes.ContainsKey(tile.name)) return;
                    var type = namedTypes[tile.name];
                    node.ChangeType(type);
                }
            }
        }

        public void CalculateNodes(Tiles tiles) {
            CreateNodes(tiles);
            foreach (var node in this) {
                CalculateNode(node);
            }
        }

        private void CalculateNode(Node node) {
            foreach (var n in this) {
                node.AddValueToDictionary(NodeDirection.FORWARD, n);
                node.AddValueToDictionary(NodeDirection.BACK, n);
                node.AddValueToDictionary(NodeDirection.LEFT, n);
                node.AddValueToDictionary(NodeDirection.RIGHT, n);
            }
            CalculateForVolcano(node);
        }

        private void CalculateForVolcano(Node node) {
            if (node.type == NodeType.BLOCKED_AROUND) {
                List<Vector3> list = new List<Vector3>();
                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 3; j++) {
                        if (i == 0 || j == 0 || i == 3 - 1 || j == 3 - 1) {
                            list.Add(new Vector3(node.position.x + j - 3 / 2, node.position.y,
                                node.position.z + i - 3 / 2));
                        }
                    }
                }
                foreach (var n in this) {
                    if (list.Contains(n.position)) {
                        n.ChangeType(NodeType.BLOCKED);
                    }
                }
            }
        }

        public void Draw() {
            foreach (var node in this) {
                node.Draw();
            }
        }

        public void Erase() {
            foreach (var node in this) {
                node.Erase();
            }
        }
    }
}