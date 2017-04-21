using System;
using System.Collections.Generic;
using UnityEngine;
using GameObject = UnityEngine.Object;

public class LevelService : MonoBehaviour {
    private PrefabDao prefabDao;

    void Start() {
        DontDestroyOnLoad(gameObject);
        prefabDao = new PrefabDao();
    }

    public void InstanceLevelTiles(Level level, bool partOnly) {
        var world = (World) Enum.Parse(typeof(World), level.world);
        if (partOnly)
            InstantiateTiles(level.tiles.parts, world);
        else {
            InstantiateTiles(level.tiles.parts, world);
            InstantiateTiles(level.tiles.structures, world);
            InstantiateTiles(level.tiles.enemies, world);
        }
    }

    public void InstanceLevelPartTiles(Level level, World world) {
        InstantiateTiles(level.tiles.parts, world);
    }


    private void InstantiateTiles(Level.Tile[] tiles, World world) {
        foreach (var tile in tiles) {
            InstantiateTile(tile, world);
        }
    }

    private void InstantiateTile(Level.Tile tile, World world) {
        var position = tile.position;
        var rotation = tile.rotation;
        var prefab = prefabDao.GetPrefabFromTile(tile, world);
        if (prefab != null) {
            Instantiate(prefab,
                    new Vector3(position.x, prefab.transform.position.y, position.z),
                    Quaternion.Euler(0, rotation.y, 0))
                .transform.SetParent(transform);
        }
        else {
            Debug.LogError("Error. Prefab with name " + tile.name + " is null");
        }
    }

    public void DestroyLevel() {
        if (transform.childCount == 0) return;
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
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

    public void CenteredLevel(Level level) {
        transform.position = new Vector3(transform.position.x - (float) level.size.length / 2, 0,
            transform.position.z - (float) level.size.width / 2);
    }
}