﻿namespace Assets.Scripts.Gameobjects.Levels {
    public class LevelStats {
        public int number;
        public int cristals;
        public int enemies;

        public LevelStats(Level level) {
            number = level.number;
            cristals = level.cristals;
            enemies = 0;
        }
    }
}