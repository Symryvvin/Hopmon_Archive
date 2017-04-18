using System.Collections.Generic;

public interface ILevelDao {
    string GetJsonLevelByNumber(int number, LevelPack pack);

    Level GetLevelByNumber(int number, LevelPack pack);

    IDictionary<int, Level> GetLevelsByPack(LevelPack pack);

    int GetLevelsCountByPack(LevelPack pack);
}
