using System.Collections.Generic;
using System.Linq;

public class Pack {
    public string packName { get; private set; }
    private IDictionary<int, Level> levels;

    public Pack(string packName) {
        this.packName = packName;
    }

    public void LoadPack(ILevelDao levelDao) {
        levels = levelDao.GetLevelsByPack(this);
    }

    public int GetLevelCountInPack() {
        return levels.Count;
    }

    public Level GetLevelByNumber(int number) {
        return levels[number];
    }

    public IDictionary<int, Level> GetLevels() {
        return levels;
    }

    public Level GetFirstLevelInPack() {
        return levels.First().Value;
    }
}