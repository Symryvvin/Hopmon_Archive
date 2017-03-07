using System;
using UnityEngine;

[Serializable]
public class JsonLevel {
    public string name;
    public int number;
    public string world;
    public Vector3 start;
    public JsonLevelObject[] objects;
}

[Serializable]
public class JsonLevelObject {
    public string name;
    public bool common;
    public Vector3 position;
    public Vector3 rotation;
}

[Serializable]
public struct LevelObjectWrapper {
    public JsonLevelObject[] objects;
}