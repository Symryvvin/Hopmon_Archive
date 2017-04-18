using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonManager<LevelManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public Transform wrapper;
    public IDictionary<int, Level> levels { get; private set; }
    private LevelPack pack;
    private LevelService levelService;

    protected override void Init() {
        levelService = new LevelService(false);
        //TODO создавать новый сервис в зависимости от пака увроней
        ChangePack(LevelPack.CLASSIC);
        levels = levelService.GetLevelByPack(pack);
    }

    public static void ChangePack(LevelPack pack) {
        instance.pack = pack;
    }

    public static LevelPack GetCurrentPack() {
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
        return instance.levels[number];
    }

    private void DestroyLevel() {
        foreach (Transform child in wrapper) {
            Destroy(child.gameObject);
        }
    }
}