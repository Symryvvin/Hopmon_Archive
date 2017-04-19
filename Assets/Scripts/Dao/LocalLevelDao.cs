using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalLevelDao : ILevelDao {
    private const string LEVEL_FOLDER = "Levels/";
    private const string EXT = ".json";

    public string GetJsonLevelByNumber(int number, Pack pack) {
        return new StreamReader(LEVEL_FOLDER + pack.packName + "/" + number + EXT).ReadToEnd();
    }

    public Level GetLevelByNumber(int number, Pack pack) {
        string json = GetJsonLevelByNumber(number, pack);
        Level level = JsonUtility.FromJson<Level>(json);
        return level;
    }

    public IDictionary<int, Level> GetLevelsByPack(Pack pack) {
        IDictionary<int, Level> dictionary = new Dictionary<int, Level>();
        int count = GetLevelsCountByPack(pack);
        for (int i = 1; i <= count; i++) {
            dictionary.Add(i, GetLevelByNumber(i, pack));
        }
        return dictionary;
    }

    private int GetLevelsCountByPack(Pack pack) {
        return new DirectoryInfo(LEVEL_FOLDER + pack.packName + "/").GetFiles().Length;
    }


    public List<string> GetLevelPackNameList() {
        List<string> list = new List<string>();
        DirectoryInfo[] directories = new DirectoryInfo(LEVEL_FOLDER).GetDirectories();
        foreach (var dir in directories) {
            list.Add(dir.Name);
        }
        return list;
    }
}