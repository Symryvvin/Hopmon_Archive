using System;
using UnityEngine;

[Serializable]
public class Level {
    public int number;
    public string name;
    public string world;
    public Vector3 start;
    public Tiles tiles;
    public Size size;
    public int cristals = 0;

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

    [Serializable]
    public struct Tile {
        public string name;
        public bool common;
        public Vector3 position;
        public Vector3 rotation;
    }

    [Serializable]
    public struct Size {
        public int width;
        public int length;

        public void DebugPrintSize() {
            Debug.Log("Level size: " + width + "x" + length);
        }
    }

    public void DebugPrintLevelInfo() {
        Debug.Log("Level #" + number + ". Name : " + name + ". World : " + world + ".\n" +
                  "Start position : " + "x=" + start.x + "y=" + start.y + "z=" + start.z);
    }
}