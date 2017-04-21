using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalLevelDao : ILevelDao {
    private const string LEVEL_FOLDER = "Levels/";
    private const string EXT = ".json";

    public string GetJsonLevelByNumber(int number, Pack pack) {
        return new StreamReader(LEVEL_FOLDER + pack.packName + "/" + number + EXT).ReadToEnd();
    }

    private Level GetLevelByFileName(string name, Pack pack) {
        string json = new StreamReader(LEVEL_FOLDER + pack.packName + "/" + name).ReadToEnd();
        Level level = JsonUtility.FromJson<Level>(json);
        return level;
    }

    public Level GetLevelByNumber(int number, Pack pack) {
        string json = GetJsonLevelByNumber(number, pack);
        Level level = JsonUtility.FromJson<Level>(json);
        return level;
    }

    public IDictionary<int, Level> GetLevelsByPack(Pack pack) {
        IDictionary<int, Level> dictionary = new SortedDictionary<int, Level>();
        FileInfo[] files = GetLevelFiles(pack);
        foreach (var file in files) {
            Level level = GetLevelByFileName(file.Name, pack);
            int index = level.number;
            dictionary.Add(index, level);
        }
        return dictionary;
    }

    private FileInfo[] GetLevelFiles(Pack pack) {
        return new DirectoryInfo(LEVEL_FOLDER + pack.packName + "/").GetFiles();
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