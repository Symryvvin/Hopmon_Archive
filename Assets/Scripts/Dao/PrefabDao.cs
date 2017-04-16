using System.Collections.Generic;
using UnityEngine;


public class PrefabDao {
    private readonly PrefabList prefabList; // data asset file
    private IDictionary<string, PrefabItem> namedItemList; // dicrionary of prefabs, key = prefabName + world

    public PrefabDao(PrefabList prefabList) {
        this.prefabList = prefabList;
    }

    public GameObject GetPrefabFromTile(Tile tile, World world) {
        if (namedItemList == null) {
            namedItemList = new Dictionary<string, PrefabItem>();
            foreach (var item in prefabList.itemList) {
                namedItemList.Add(item.name + item.world, item);
            }
        }
        else {
            string key = tile.common ? tile.name + "COMMON" : tile.name + world;
/*            if (onlyPart) {
                return namedItemList[key].type == Kind.PART ? namedItemList[key].prefab : null;
            }*/
            return namedItemList[key].prefab;
        }
        return null;
    }

    public GameObject GetPlayerPrefab() {
        return namedItemList["HopmonCOMMON"].prefab;
    }

}