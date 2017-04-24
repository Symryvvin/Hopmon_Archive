using System;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Level {
    [Serializable]
    public struct Tile {
        public string name;
        public bool common;
        public Vector3 position;
        public Vector3 rotation;
    }
}
