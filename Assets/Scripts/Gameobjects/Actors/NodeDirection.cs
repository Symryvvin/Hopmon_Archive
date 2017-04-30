using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Actors {
    public class NodeDirection {
        public const string FORWARD = "forward";
        public const string BACK = "back";
        public const string LEFT = "left";
        public const string RIGHT = "right";

        public static IDictionary<string, string> reverse = new Dictionary<string, string> {
            {FORWARD, BACK},
            {BACK, FORWARD},
            {LEFT, RIGHT},
            {RIGHT, LEFT}
        };

        public static IDictionary<string, Vector3> keyToPosition = new Dictionary<string, Vector3> {
            {FORWARD, Vector3.forward},
            {BACK, Vector3.back},
            {LEFT, Vector3.left},
            {RIGHT, Vector3.right}
        };
    }
}