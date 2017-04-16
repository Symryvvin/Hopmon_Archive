using System.Collections.Generic;
using System.IO;

public class LocalLevelDao : ILevelDao {
    private const string LEVEL_FOLDER = "Levels/";
    private const string EXT = ".json";

    public string getLevelByNumber(int number) {
        return new StreamReader(LEVEL_FOLDER + number + EXT).ReadToEnd();
    }

    public List<string> getLevels() {
        //
        return null;
    }

    public int getLevelsCountByPack(LevelPack pack) {
        switch (pack) {
            case LevelPack.CLASSIC:
                return 45;
        }
        return 0;
    }
}