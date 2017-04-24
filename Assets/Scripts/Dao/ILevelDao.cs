using System.Collections.Generic;

public interface ILevelDao {
    string GetJsonLevelByNumber(int number, Pack pack);

    Level GetLevelByNumber(int number, Pack pack);

    IDictionary<int, Level> GetLevelsByPack(Pack pack);
}
