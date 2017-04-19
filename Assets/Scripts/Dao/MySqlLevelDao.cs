using System;
using System.Collections.Generic;

public class MySqlLevelDao : ILevelDao {
    public string GetJsonLevelByNumber(int number, Pack pack) {
        throw new NotImplementedException();
    }

    public Level GetLevelByNumber(int number, Pack pack) {
        throw new NotImplementedException();
    }

    public IDictionary<int, Level> GetLevelsByPack(Pack pack) {
        throw new NotImplementedException();
    }

    public List<string> GetLevelPackNameList() {
        throw new NotImplementedException();
    }
}