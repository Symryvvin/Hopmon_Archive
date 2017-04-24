using System.Collections.Generic;
using Assets.Scripts.Gameobjects.Level;
using UnityEngine;


public class PrefabDao {
    private const string prefabListPath = "Data/PrefabList";
    private readonly PrefabList prefabList;
    private IDictionary<string, PrefabItem> namedItemList;

    public PrefabDao() {
        prefabList = Resources.Load(prefabListPath) as PrefabList;
    }

    private void InstanceDictionary() {
        if (namedItemList == null) {
            namedItemList = new Dictionary<string, PrefabItem>();
            foreach (var item in prefabList.itemList) {
                namedItemList.Add(item.name + item.world, item);
            }
        }
    }

    public GameObject GetPrefabFromTile(Tile tile, World world) {
        InstanceDictionary();
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

    public GameObject GetPlayerPrefab() {
        InstanceDictionary();
        return namedItemList["HopmonCOMMON"].prefab;
    }
}