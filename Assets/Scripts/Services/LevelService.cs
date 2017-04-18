using System;
using System.Collections.Generic;
using UnityEngine;
using GameObject = UnityEngine.Object;

public class LevelService {
    private ILevelDao levelDao;
    private PrefabDao prefabDao;
    private readonly bool isNetworkAvaiable;


    public LevelService(bool network) {
        isNetworkAvaiable = network;
        InstanceLevelDao();
        InstancePrefabDao();
    }

    private void InstanceLevelDao() {
        if (levelDao == null) {
            if (isNetworkAvaiable)
                levelDao =  new MySqlLevelDao();
            levelDao =  new LocalLevelDao();
        }
    }

    private void InstancePrefabDao() {
        if (prefabDao == null) {
            prefabDao = new PrefabDao();
        }
    }

    public IDictionary<int, Level> GetLevelByPack(LevelPack pack) {
        return levelDao.GetLevelsByPack(pack);
    }

    public List<Transform> InstanceLevelTiles(Level level) {
        List<Transform> transforms = new List<Transform>();
        var world = (World) Enum.Parse(typeof(World), level.world);
        transforms.AddRange(InstantiateTiles(level.tiles.parts, world));
        transforms.AddRange(InstantiateTiles(level.tiles.enemies, world));
        transforms.AddRange(InstantiateTiles(level.tiles.structures, world));
        return transforms;
    }


    private List<Transform> InstantiateTiles(Level.Tile[] tiles, World world) {
        List<Transform> transfroms = new List<Transform>();
        foreach (var tile in tiles) {
            transfroms.Add(InstantiateTile(tile, world));
        }
        return transfroms;
    }

    private Transform InstantiateTile(Level.Tile tile, World world) {
        var position = tile.position;
        var rotation = tile.rotation;
        var prefab = prefabDao.GetPrefabFromTile(tile, world);
        if (prefab == null) {
            Debug.LogError("Error. Prefab with name " + tile.name + " is null");
            return null;
        }
        return GameObject.Instantiate(prefab,
                new Vector3(position.x, prefab.transform.position.y, position.z),
                Quaternion.Euler(0, rotation.y, 0))
            .transform;
    }

    public Player InstancePlayer() {
        return GameObject.Instantiate(prefabDao.GetPlayerPrefab()).GetComponent<Player>().InitPlayer();
    }

    public int GetCristallCount(Level level) {
        int count = 0;
        foreach (var part in level.tiles.structures) {
            if (part.name.Equals("Cristal"))
                count++;
        }
        return count;
    }

    public void ChangeMusic(Level level) {
        var world = (World) Enum.Parse(typeof(World), level.world);
        switch (world) {
            case World.TEMPLE:
                AudioManager.instance.TempleMusic();
                break;
            case World.JUNGLE:
                AudioManager.instance.JungleMusic();
                break;
            case World.SPACE:
                AudioManager.instance.SpaceMusic();
                break;
        }
    }

    public int GetMaxLevelNumberInPack(LevelPack pack) {
        return levelDao.GetLevelsCountByPack(pack);
    }
}