using System;
using Assets.Scripts.Rules.Movement;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Levels {
    [Serializable]
    public class Level {
        public int number;
        public string name;
        public string world;
        public Vector3 start;
        public Tiles tiles;
        public Size size;
        public int cristals;
        public Nodes nodes;

        public void Build() {
            nodes = new Nodes();
            nodes.CalculateNodes(tiles);
            nodes.Draw();
            LevelBuilder.BuildLevel(this, false);
            CountCristals();
        }

        public void BuildPart() {
            LevelBuilder.BuildLevel(this, true);
            CountCristals();
        }

        private void CountCristals() {
            int count = 0;
            foreach (var part in tiles.structures) {
                if (part.name.Equals("Cristal"))
                    count++;
            }
            cristals = count;
        }


        public void DebugPrintLevelInfo() {
            Debug.Log("Level #" + number + ". Name : " + name + ". World : " + world + ".\n" +
                      "Start position : " + "x=" + start.x + "y=" + start.y + "z=" + start.z);
        }
    }
}