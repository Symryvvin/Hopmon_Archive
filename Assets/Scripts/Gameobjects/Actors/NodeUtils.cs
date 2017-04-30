using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors {
    public class NodeUtils {
        public static string GetReversedKey(string key) {
            return NodeDirection.reverse[key];
        }

        public static NodeType GetTypeByName(string name) {
            if (name.Equals("Floor") || name.Equals("Bridge") || name.Equals("Warp")) {
                return NodeType.NORMAL;
            }
            if (name.Equals("Arrow")) {
                return NodeType.BOOSTER;
            }
            return NodeType.BLOCKED;
        }


        public static Vector3 KeyToPosition(string key) {
            return NodeDirection.keyToPosition[key];
        }
    }
}