using System.Collections.Generic;
using UnityEngine;


public class PrefabDao {
    private readonly PrefabList prefabList; // data asset file
    private IDictionary<string, PrefabItem> namedItemList; // dicrionary of prefabs, key = prefabName + world

    public PrefabDao(PrefabList prefabList) {
        this.prefabList = prefabList;
    }

    public void InstanceDictionary() {
        if (namedItemList == null) {
            namedItemList = new Dictionary<string, PrefabItem>();
            foreach (var item in prefabList.itemList) {
                namedItemList.Add(item.name + item.world, item);
            }
        }
    }

    public GameObject GetPrefabFromTile(Level.Tile tile, World world) {
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
        return namedItemList["HopmonCOMMON"].prefab;
    }
}