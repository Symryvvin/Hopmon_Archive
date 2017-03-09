using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public PrefabList prefabList;
    public Transform level;
    private World world;
    public Dictionary<string, PrefabItem> namedItemList;
    public JsonLevel jsonLevel;

    public void SetDictionary() {
        namedItemList = new Dictionary<string, PrefabItem>();
        foreach (var item in prefabList.itemList) {
            namedItemList.Add(item.name + item.world, item);
        }
    }

    public void LoadMap(int number) {
        string json = GetJson(number);
        jsonLevel = JsonUtility.FromJson<JsonLevel>(json);
        jsonLevel.objects = JsonUtility.FromJson<LevelObjectWrapper>(json).objects;
        world = (World) Enum.Parse(typeof(World), jsonLevel.world);
        foreach (var jsonLevelObject in jsonLevel.objects) {
            GenerateLevelObject(jsonLevelObject);
        }
    }

    // получаем json строку из файла
    private static string GetJson(int number) {
        var reader = new StreamReader("Levels/" + number + ".json");
        string jsonString = reader.ReadToEnd();
        return jsonString;
    }


    private void GenerateLevelObject(JsonLevelObject o) {
        string loName = o.name;
        // Kind kind = (Kind) Enum.Parse(typeof(Kind), o.type); тип будет влиять на слой
        bool common = o.common;
        var position = o.position;
        var rotation = o.rotation;
        var prefab = GetPrefabByName(loName, common);
        if (prefab != null) {
            var go = Instantiate(prefab,
                new Vector3(position.x, prefab.transform.position.y, position.z),
                Quaternion.Euler(0, rotation.y, 0));
            go.transform.SetParent(level);
        }
    }

    public GameObject GetPrefabByName(string name, bool common) {
        string key = common ? name + "COMMON" : name + world;
        return namedItemList[key].prefab;
    }


}