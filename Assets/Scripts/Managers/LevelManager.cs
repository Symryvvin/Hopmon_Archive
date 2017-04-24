using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Level;

public class LevelManager : SingletonManager<LevelManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    private ILevelDao levelDao;
    private Pack pack;
    private List<Pack> packs;

    protected override void Init() {
        // while we don`t have database with levelsView use LocalLevelDao
        levelDao = new LocalLevelDao();
        // and load pack with index 0 (now it CLASSIC pack in Levels/CLASSIC)
        packs = PackLoader.GetPackList();
        pack = LoadPackByIndex(0);
    }

    public static Pack SwitchLevelPack() {
        int index = instance.packs.IndexOf(instance.pack);
        index++;
        instance.pack = index < instance.packs.Count ? LoadPackByIndex(index) : LoadPackByIndex(0);
        return instance.pack;
    }

    private static Pack LoadPackByIndex(int index) {
        return instance.packs[index].LoadPack(instance.levelDao);
    }

    public static Pack GetCurrentPack() {
        return instance.pack;
    }

    public static Level GetLevelByNumber(int number) {
        return instance.pack.GetLevelByNumber(number);
    }

    public static Level NextLevel(Level level) {
        return instance.pack.GetNextLevelFrom(level);
    }

    public static Level PrevLevel(Level level) {
        return instance.pack.GetPrevLevelFrom(level);
    }
}