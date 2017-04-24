using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Level;

public class LevelManager : SingletonManager<LevelManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public LevelService levelService;
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

    public static void DestroyLevel() {
        instance.levelService.DestroyLevel();
    }

    public static void BuildLevel(Level level, bool partTypeOnly) {
        DestroyLevel();
        instance.levelService.InstanceLevelTiles(level, partTypeOnly);
        if (!partTypeOnly)
            instance.levelService.ChangeMusic(level);
        else
            instance.levelService.CenteredLevel(level);
    }

    public static Level NextLevel() {
        throw new System.NotImplementedException();
    }
}