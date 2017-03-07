using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public Vector3 start;
    public PrefabList prefabList;
    [SerializeField] private int levelNumber;
    [SerializeField] private InputField input;
    [SerializeField] private Transform level;
    private World world;
    private Dictionary<string, PrefabItem> namedItemList;

    void Start() {
        SetDictionary();
        LoadMap();
    }

    private void SetDictionary() {
        namedItemList = new Dictionary<string, PrefabItem>();
        foreach (var item in prefabList.itemList) {
            namedItemList.Add(item.name + item.world, item);
        }
    }

    public void LoadMap() {
        string json = GetJson(levelNumber);
        JsonLevel jsonLevel = JsonUtility.FromJson<JsonLevel>(json);
        jsonLevel.objects = JsonUtility.FromJson<LevelObjectWrapper>(json).objects;
        start = jsonLevel.start;
        world = (World) Enum.Parse(typeof(World), jsonLevel.world);
        // DebugPrint(jsonLevel);
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

    private GameObject GetPrefabByName(string name, bool common) {
        string key =  common ? name + "COMMON" : name + world;
        return namedItemList[key].prefab;
    }

    private void UnLoadMap() {
        foreach (Transform child in level) {
            Destroy(child.gameObject);
        }
    }

    public void SetLevelNumber() {
        levelNumber = int.Parse(input.text);
    }

    public void DebugPrevLevel() {
        UnLoadMap();
        if (levelNumber > 1)
            levelNumber--;
        LoadMap();
    }

    public void DebugNextLevel() {
        UnLoadMap();
        if (levelNumber < 45)
            levelNumber++;
        LoadMap();
    }

    private void DebugPrint(JsonLevel jsonLevel) {
        Debug.Log("Level " + levelNumber + " is loaded... Name = " + jsonLevel.name + " | World is " + jsonLevel.world +
                  "\r\nObjects count : " + jsonLevel.objects.Length);
        world = (World) Enum.Parse(typeof(World), jsonLevel.world);
        Debug.Log("Start = " + jsonLevel.start.x + "; " + jsonLevel.start.z);
    }
}