using System;
using UnityEngine;

namespace Assets.Scripts.Gameobjects.Levels {
    [Serializable]
    public struct Size {
        public int width;
        public int length;

        public void DebugPrintSize() {
            Debug.Log("Level size: " + width + "x" + length);
        }
    }
}
