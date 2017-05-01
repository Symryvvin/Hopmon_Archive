using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Levels;

namespace Assets.Scripts.Gameobjects.Actors.Movements {
    public class Movement {
        public List<Node> nodes;

        public Movement() {
            nodes = new List<Node>();
        }

        public void CalculateNodes(Tiles tiles) {
            foreach (var tile in tiles.parts) {
                // Добавляем все узлы, которые не отсносятся в стенам
                if (!tile.name.Equals("Wall") && !tile.name.Equals("DoubleWall")) {
                    var node = new Node(tile);
                    node.ChangeType(NodeType.NORMAL);
                    // Если узел это ускоритель, меняем ему тип
                    if (tile.name.Equals("Arrow")) {
                        node.ChangeType(NodeType.BOOSTER);
                    }
                    nodes.Add(node);
                }
            }
            // Меняем тип узла на BLOCKED, если его позиция совпадает с позицией объекта
            // из массива структур, кроме кристалов
            foreach (var node in nodes) {
                foreach (var tile in tiles.structures) {
                    if (tile.position == node.position) {
                        if (!tile.name.Equals("Cristal")) {
                            node.ChangeType(NodeType.BLOCKED);
                        }
                        if (tile.name.Equals("InvisibleWall")) {
                            node.ChangeType(NodeType.BLOCKED_FOR_ENEMY);
                        }
                        if (tile.name.Equals("Volcano")) {
                            node.ChangeType(NodeType.BLOCKED_AROUND);
                        }
                    }
                }
            }
            // Меняем тип узла на BLOCKED_FOR_ENEMY, если его позиция совпадает с позицией объекта
            // из массива врагов с именем NeedleBox
            foreach (var node in nodes) {
                foreach (var tile in tiles.enemies) {
                    if (tile.position == node.position && tile.name.Contains("NeedleBox")) {
                        node.ChangeType(NodeType.BLOCKED_FOR_ENEMY);
                    }
                }
            }
            // Перебираем все полученные узлы и расчитываем их связи
            foreach (var node in nodes) {
                node.CalculateDirections(this);
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