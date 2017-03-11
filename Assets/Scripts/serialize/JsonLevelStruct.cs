using System;
using UnityEngine;

/// <summary>
/// This is struct of object "level" (root object) in json file
/// </summary>
[Serializable]
public struct JsonLevelStruct {
    public string name;
    public int number;
    public string world;
    public Vector3 start;
    public JsonLevelObjectStruct[] parts;
}

/// <summary>
/// This is sctruct of levelObject in json array "objects"
/// </summary>
[Serializable]
public partial struct JsonLevelObjectStruct {
    public string name;
    public bool common;
    public Vector3 position;
    public Vector3 rotation;
}

public partial struct JsonLevelObjectStruct {
    // for new paramenetrs which can be iucluded in old version of game
}

/// <summary>
/// Wrapper for loading object from json file with JsonUtility.FromJson
/// </summary>
[Serializable]
public struct LevelObjectWrapper {
    public JsonLevelObjectStruct[] objects;
}

