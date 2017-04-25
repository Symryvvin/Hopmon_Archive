using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Level;
using UnityEngine;


public class PrefabLoader : MonoBehaviour {
    public PrefabList prefabList;
    private static IDictionary<string, PrefabItem> namedItemList;

    void Start() {
        namedItemList = new Dictionary<string, PrefabItem>();
        foreach (var item in prefabList.itemList) {
            namedItemList.Add(item.name + item.world, item);
        }
    }

    public GameObject GetPrefabFromTile(Tile tile, World world) {
        string key = "";
        try {
            key = tile.common ? tile.name + "COMMON" : tile.name + world;
            return namedItemList[key].prefab;
        }
        catch (KeyNotFoundException e) {
            Debug.LogError("KEY : " + key + ". error " + e);
        }
        return null;
    }
}