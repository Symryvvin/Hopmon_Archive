using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A singletone class of LevelManager. Load level from file. Create object of type Level for using by GameManager
/// </summary>
public class LevelManager : SingletonManager<LevelManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public PrefabList prefabList; // data asset file
    public Transform wrapper; // transform of empty GameObject (parent for level parts)
    private World world; // word type of level
  //  private World lastWorld;
  //  private bool firstLoad;
    private IDictionary<int, string> levels;
    private ILevelDao levelDao;
    private PrefabDao prefabDao;

    /// <summary>
    /// Set dictionary of prefabs, getting from data asset prefabList
    /// </summary>
    protected override void Init() {
        // db is not inmplement use local dao
        levelDao = new LocalLevelDao();
        levels = new Dictionary<int, string>();
        int levelCount = levelDao.getLevelsCountByPack(LevelPack.CLASSIC);
        for (int i = 1; i <= levelCount; i++) {
            levels.Add(i, levelDao.getLevelByNumber(i));
        }
        prefabDao = new PrefabDao(prefabList);
       // firstLoad = false;
    }

    public Level LoadLevel(int number) {
        string json = levels[number];
        Level level = JsonUtility.FromJson<Level>(json);
        level.parts = JsonUtility.FromJson<TileWrapper>(json).objects;
        world = (World) Enum.Parse(typeof(World), level.world);
        return level;
    }

    public void InstantiateTilesForLevel(Level level) {
        foreach (var tile in level.parts) {
            InstantiateTile(tile);
        }
    }

    private void InstantiateTile(Tile tile) {
        var position = tile.position;
        var rotation = tile.rotation;
        var prefab = prefabDao.GetPrefabFromTile(tile, world);
        if (prefab == null) return;
        var go = Instantiate(prefab,
            new Vector3(position.x, prefab.transform.position.y, position.z),
            Quaternion.Euler(0, rotation.y, 0));
        go.transform.SetParent(wrapper);
    }

    public GameObject GetPlayerInstance() {
        return Instantiate(prefabDao.GetPlayerPrefab());
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

    /// <summary>
    /// Unload level by destroing all gameobjects from level parent
    /// </summary>
    public void UnLoadLevelMap() {
        foreach (Transform child in wrapper) {
            Destroy(child.gameObject);
        }
    }
}