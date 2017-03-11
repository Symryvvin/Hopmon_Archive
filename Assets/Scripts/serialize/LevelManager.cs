using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A singletone class of LevelManager. Load leve from file. Create object of type Level for using by GameManager
/// </summary>
public class LevelManager : MonoBehaviour {
    public PrefabList prefabList;                              // data asset file
    public Transform level;                                    // transform of empty GameObject (parent for level parts)
    private World world;                                       // word type of level
    public Dictionary<string, PrefabItem> namedItemList;       // dicrionary of prefabs, key = name + world
    public JsonLevelStruct jsonLevelStruct;                    // sctrut object of json file

    /// <summary>
    /// Set dictionary of prefabs, getting from data asset prefabList
    /// </summary>
    public void SetDictionary() {
        namedItemList = new Dictionary<string, PrefabItem>();
        foreach (var item in prefabList.itemList) {
            namedItemList.Add(item.name + item.world, item);
        }
    }

    /// <summary>
    /// Load level map, generated all GameObject from json file on scene
    /// </summary>
    /// <param name="number">level number</param>
    /// <returns>Level with init fields for using by GameManager</returns>
    public Level LoadLevelMap(int number) {
        string json = GetJson(number);
        jsonLevelStruct = JsonUtility.FromJson<JsonLevelStruct>(json);
        // very important to naming array "object" like in json file
        jsonLevelStruct.parts = JsonUtility.FromJson<LevelObjectWrapper>(json).objects;
        world = (World) Enum.Parse(typeof(World), jsonLevelStruct.world);
        foreach (var jsonLevelObject in jsonLevelStruct.parts) {
            GenerateLevelObject(jsonLevelObject);
        }
        return new Level(jsonLevelStruct);
    }

    /// <summary>
    /// Get JSON String from file
    /// </summary>
    /// <param name="number">number of level</param>
    /// <returns>JSON String</returns>
    private static string GetJson(int number) {
        var reader = new StreamReader("Levels/" + number + ".json");
        return reader.ReadToEnd();
    }


    /// <summary>
    /// Generate gameObject on scene and set parent level - empty GameObject on scene
    /// </summary>
    /// <param name="levelObject">Loaded sctuct from JSON file</param>
    private void GenerateLevelObject(JsonLevelObjectStruct levelObject) {
        string loName = levelObject.name;
        bool common = levelObject.common;
        var position = levelObject.position;
        var rotation = levelObject.rotation;
        var prefab = GetPrefabByName(loName, common);
        if (prefab != null) {
            var go = Instantiate(prefab,
                new Vector3(position.x, prefab.transform.position.y, position.z),
                Quaternion.Euler(0, rotation.y, 0));
            go.transform.SetParent(level);
        }
    }

    /// <summary>
    /// Get prefab by key from dictionaye namedItemList
    /// </summary>
    /// <param name="name">name of prefab</param>
    /// <param name="common">world type of prefab</param>
    /// <returns>prefab GameObject</returns>
    public GameObject GetPrefabByName(string name, bool common) {
        string key = common ? name + "COMMON" : name + world;
        return namedItemList[key].prefab;
    }

    /// <summary>
    /// Unload level by destroing all gameobjects from level parent
    /// </summary>
    public void UnLoadLevelMap() {
        foreach (Transform child in level) {
            Destroy(child.gameObject);
        }
    }


}