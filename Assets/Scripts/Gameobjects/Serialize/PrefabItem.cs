using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabItem {
    public string name;
    public Kind type;
    public World world;
    public GameObject prefab;
}

public enum World {
    TEMPLE,
    JUNGLE,
    SPACE,
    COMMON
}

public enum Kind {
    PART,
    OBJECT,
    ENEMY,
    META
}
