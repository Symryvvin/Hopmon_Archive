using System;
using System.Collections.Generic;

public class MySqlLevelDao : ILevelDao {
    public string GetJsonLevelByNumber(int number, LevelPack pack) {
        throw new NotImplementedException();
    }

    public Level GetLevelByNumber(int number, LevelPack pack) {
        throw new NotImplementedException();
    }

    public IDictionary<int, Level> GetLevelsByPack(LevelPack pack) {
        throw new NotImplementedException();
    }

    public int GetLevelsCountByPack(LevelPack pack) {
        throw new NotImplementedException();
    }
}