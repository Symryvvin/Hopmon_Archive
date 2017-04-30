using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Level;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors {
    public class Movement {
        public List<Node> nodes;

        public Movement() {
            nodes = new List<Node>();
        }

        public void CalculateNodes(Level.Level level) {
            List<Tile> partTiles = new List<Tile>(level.tiles.parts);
            List<Tile> structTiles = new List<Tile>(level.tiles.structures);
            foreach (var tile in partTiles) {
                var node = new Node(tile);
                nodes.Add(node);
            }
            foreach (var node in nodes) {
                foreach (var tile in structTiles) {
                    if (tile.position == node.position) {
                        if (tile.name.Equals("Pole")) {
                            Debug.Log("pole");
                        }
                        node.SwitchType(NodeType.BLOCKED);
                    }
                }
                node.graph = this;
                node.CalculateDirections();
            }
        }

        public void Draw() {
            foreach (var node in nodes) {
                node.Draw();
            }
        }

        public void Erase() {
            foreach (var node in nodes) {
                node.Erase();
            }
        }
    }
}
