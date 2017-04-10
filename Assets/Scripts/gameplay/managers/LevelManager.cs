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
    public Transform level; // transform of empty GameObject (parent for level parts)
    private Dictionary<string, PrefabItem> namedItemList; // dicrionary of prefabs, key = prefabName + world
    private JsonLevelStruct jsonLevelStruct; // sctrut object of json file
    private World world; // word type of level
    private World lastWorld;
    private bool firstLoad;

    /// <summary>
    /// Set dictionary of prefabs, getting from data asset prefabList
    /// </summary>
    protected override void Init() {
        namedItemList = new Dictionary<string, PrefabItem>();
        foreach (var item in prefabList.itemList) {
            namedItemList.Add(item.name + item.world, item);
        }
        firstLoad = false;
    }

    /// <summary>
    /// Get instanse of player
    /// </summary>
    /// <returns>Hopmon gameObject</returns>
    public GameObject GetPlayerInstance() {
        return Instantiate(GetPrefabByName("Hopmon", true, false));
    }

    /// <summary>
    /// Load level map, generated all GameObject from json file on scene
    /// </summary>
    /// <param name="number">level number</param>
    /// <returns>Level with init fields for using by GameManager</returns>
    public Level LoadLevelMap(int number) {
        LoadLevelStruct(number);
        ChangeMusic();
        foreach (var jsonLevelObject in jsonLevelStruct.parts) {
            GenerateAnyLevelObject(jsonLevelObject);
        }
        return new Level(jsonLevelStruct);
    }

    public Level LoadLevelParts(int number) {
        LoadLevelStruct(number);
        foreach (var jsonLevelObject in jsonLevelStruct.parts) {
            GenerateLevelPart(jsonLevelObject);
        }
        return new Level(jsonLevelStruct);
    }

    private void LoadLevelStruct(int number) {
        string json = GetJson(number);
        jsonLevelStruct = JsonUtility.FromJson<JsonLevelStruct>(json);
        // very important to naming array "object" like in json file
        jsonLevelStruct.parts = JsonUtility.FromJson<LevelObjectWrapper>(json).objects;
        world = (World) Enum.Parse(typeof(World), jsonLevelStruct.world);
    }

    private void ChangeMusic() {
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

    /// <summary>
    /// Get JSON String from file
    /// </summary>
    /// <param name="number">number of level</param>
    /// <returns>JSON String</returns>
    private static string GetJson(int number) {
        var reader = new StreamReader("Levels/" + number + ".json");
        return reader.ReadToEnd();
    }


    private void GenerateLevelPart(JsonLevelObjectStruct levelObject) {
        GenerateLevelObject(levelObject, true);
    }

    private void GenerateAnyLevelObject(JsonLevelObjectStruct levelObject) {
        GenerateLevelObject(levelObject, false);
    }

    /// <summary>
    /// Generate gameObject on scene and set parent level - empty GameObject on scene
    /// </summary>
    /// <param name="levelObject">Loaded sctuct from JSON file</param>
    /// <param name="onlyPart">if type is a PART</param>
    private void GenerateLevelObject(JsonLevelObjectStruct levelObject, bool onlyPart) {
        string loName = levelObject.name;
        bool common = levelObject.common;
        var position = levelObject.position;
        var rotation = levelObject.rotation;
        var prefab = GetPrefabByName(loName, common, onlyPart);
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
    /// <param name="prefabName">prefabName of prefab</param>
    /// <param name="common">world type of prefab</param>
    /// <param name="onlyPart"></param>
    /// <returns>prefab GameObject</returns>
    private GameObject GetPrefabByName(string prefabName, bool common, bool onlyPart) {
        string key = common ? prefabName + "COMMON" : prefabName + world;
        if (onlyPart) {
            return namedItemList[key].type == Kind.PART ? namedItemList[key].prefab : null;
        }
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