using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonManager<LevelManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public Transform wrapper;
    private IDictionary<int, Level> levels;
    private LevelService levelService;

    protected override void Init() {
        levelService = new LevelService(false);
        //TODO создавать новый сервис в зависимости от пака увроней
        levels = levelService.GetLevelByPack(LevelPack.CLASSIC);
        // firstLoad = false;
    }

    //TODO переместить в класс Game
    public Level LoadLevel(int number) {
        return levels[number];
    }

    public void CreateLevel(Level level) {
        if (wrapper.childCount > 0)
            DestroyLevel();
        List<Transform> allTilesTransform = levelService.InstanceLevelTiles(level);
        foreach (var t in allTilesTransform) {
            t.SetParent(wrapper);
        }
        levelService.ChangeMusic(level);
    }

    //TODO переместить в класс Game
    public Transform GetPlayerInstance(Level level) {
        return levelService.InstancePlayerOnStartPoint(level);
    }

    //TODO переместить в класс Game
    public void DestroyPlayer(GameObject player) {
        levelService.DestroyPlayer(player);
    }


    //TODO переместить в класс Game
    public int GetCristalCount(Level level) {
        return levelService.GetCristallCount(level);
    }


/*  private void ChangeMusic() {
        if (lastWorld != world || !firstLoad) {
            firstLoad = true;
            lastWorld = world;
            switch (world) {
                case World.TEMPLE:
                    EventManager.TriggerEvent("templeMusic");
                    break;
                case World.JUNGLE:
                    EventManager.TriggerEvent("jungleMusic");
                    break;
                case World.SPACE:
                    EventManager.TriggerEvent("spaceMusic");
                    break;
            }
        }
    }
*/

    private void DestroyLevel() {
        foreach (Transform child in wrapper) {
            Destroy(child.gameObject);
        }
    }
}