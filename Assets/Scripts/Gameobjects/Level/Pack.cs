using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameobjects.Game;

public class Pack {
    public string packName { get; private set; }
    private IDictionary<int, Level> levels;

    public Pack(string packName) {
        this.packName = packName;
    }

    public Pack LoadPack(ILevelDao levelDao) {
        levels = levelDao.GetLevelsByPack(this);
        return this;
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

    public Level GetNextLevelFrom(Level level) {
        bool getNextLevel = false;
        foreach (var entry in levels) {
            if (getNextLevel) {
                return entry.Value;
            }
            if (entry.Value != level) continue;
            if (entry.Value == levels.Last().Value) {
                EventManager.TriggerEvent(GameEvents.COMPLETE_LAST_LEVEL);
                return levels.Last().Value;
            }
            getNextLevel = true;
        }
        return null;
    }

    public Level GetPrevLevelFrom(Level level) {
        bool getPrevLevel = false;
        Level prev = null;
        foreach (var entry in levels) {
            if (getPrevLevel) {
                return prev;
            }
            if (entry.Value != level) {
                prev = entry.Value;
            }
            else {
                if (entry.Value == levels.First().Value)
                    return levels.First().Value;
                getPrevLevel = true;
            }
        }
        return prev;
    }
}