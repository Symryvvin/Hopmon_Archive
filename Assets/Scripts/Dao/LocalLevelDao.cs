using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalLevelDao : ILevelDao {
    private const string LEVEL_FOLDER = "Levels/";
    private const string EXT = ".json";

    public string GetJsonLevelByNumber(int number, LevelPack pack) {
        return new StreamReader(LEVEL_FOLDER + pack + "/" + number + EXT).ReadToEnd();
    }

    public Level GetLevelByNumber(int number, LevelPack pack) {
        string json = GetJsonLevelByNumber(number, pack);
        Level level = JsonUtility.FromJson<Level>(json);
        return level;
    }


    public IDictionary<int, Level> GetLevelsByPack(LevelPack pack) {
        IDictionary<int, Level> dictionary = new Dictionary<int, Level>();
        int count = GetLevelsCountByPack(pack);
        for (int i = 1; i <= count; i++) {
            dictionary.Add(i, GetLevelByNumber(i, pack));
        }
        return dictionary;
    }

    public int GetLevelsCountByPack(LevelPack pack) {
        return new DirectoryInfo(LEVEL_FOLDER + pack + "/").GetFiles().Length;
    }
}