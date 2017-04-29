using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Level;

namespace Assets.Scripts.Gameobjects.Actors {
    public class MotionGraph {
        public List<GraphNode> nodes;

        public MotionGraph() {
            nodes = new List<GraphNode>();
        }

        public void CalculateNodes(Level.Level level) {
            List<Tile> partTiles = new List<Tile>(level.tiles.parts);
            List<Tile> structTiles = new List<Tile>(level.tiles.structures);
            foreach (var tile in partTiles) {
                var node = new GraphNode(tile, this);
                node.CalculateDirections();
                nodes.Add(node);
            }
            foreach (var node in nodes) {
                foreach (var tile in structTiles) {
                    if (tile.position == node.position) {
                        node.SwitchType(GraphNodeType.BLOCKED);
                        node.CalculateDirections();
                    }
                }
            }
        }

        public void Draw() {
            foreach (var node in nodes) {
                node.Draw();
            }
        }
    }
}
