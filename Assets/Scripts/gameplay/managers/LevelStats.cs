public class LevelStats {
    public int LevelNumber { get; set; }
    public int CristalCount { get; set; }

    public LevelStats(int levelNumber, int cristalCount) {
        this.LevelNumber = levelNumber;
        this.CristalCount = cristalCount;
    }
}