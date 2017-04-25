using System;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Level {
    [Serializable]
    public struct Tiles {
        public Tile[] parts;
        public Tile[] enemies;
        public Tile[] structures;

        public void DebugTilesCount() {
            Debug.Log("Parts : " + parts.Length + ". Enemies : " + enemies.Length + ". Structures : " +
                      structures.Length);
        }
    }
}
