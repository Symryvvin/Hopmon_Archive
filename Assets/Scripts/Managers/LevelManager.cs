using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonManager<LevelManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public Transform wrapper;

    private ILevelDao levelDao;
    private LevelService levelService;

    private Pack pack;
    private List<string> packNames;

    protected override void Init() {
        // while we don`t have database with levels use LocalLevelDao
        levelDao = new LocalLevelDao();
        levelService = new LevelService();
        // take list of exiting level pack
        packNames = levelDao.GetLevelPackNameList();
        // and load pack with index 0 (now it CLASSIC pack in Levels/CLASSIC)
        pack = new Pack(packNames[0]);
        pack.LoadPack(levelDao);
    }

    private void ChangePack(Pack p) {
        pack = p;
    }

    public void SwitchLevelPack() {
        Pack p;
        int index = 0;
        foreach (var n in packNames) {
            if (pack.packName.Equals(n))
                break;
            index++;
        }
        if (index < packNames.Count)
            p = new Pack(packNames[index]);
        else {
            p = new Pack(packNames[0]);
        }
        p.LoadPack(levelDao);
        ChangePack(p);
    }

    public static Pack GetCurrentPack() {
        return instance.pack;
    }

    public static void CreateLevel(Level level) {
        if (instance.wrapper.childCount > 0)
            instance.DestroyLevel();
        List<Transform> allTilesTransform = instance.levelService.InstanceLevelTiles(level);
        foreach (var t in allTilesTransform) {
            t.SetParent(instance.wrapper);
        }
        instance.levelService.ChangeMusic(level);
    }

    public static Level GetLevelByNumber(int number) {
        return instance.pack.GetLevelByNumber(number);
    }

    private void DestroyLevel() {
        foreach (Transform child in wrapper) {
            Destroy(child.gameObject);
        }
    }
}