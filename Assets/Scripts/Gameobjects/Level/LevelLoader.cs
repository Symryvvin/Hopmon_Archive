using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader {

    public static IDictionary<int, Level> GetLevelsByPack(Pack pack) {
        IDictionary<int, Level> dictionary = new SortedDictionary<int, Level>();
        FileInfo[] files = GetLevelFiles(pack);
        foreach (var file in files) {
            Level level = GetLevelByFileName(file.FullName, pack);
            int index = level.number;
            dictionary.Add(index, level);
        }
        return dictionary;
    }

    private static FileInfo[] GetLevelFiles(Pack pack) {
        return new DirectoryInfo(pack.path).GetFiles();
    }

    private static Level GetLevelByFileName(string name, Pack pack) {
        string json = new StreamReader(name).ReadToEnd();
        Level level = JsonUtility.FromJson<Level>(json);
        return level;
    }
}