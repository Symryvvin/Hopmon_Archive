using System.Collections.Generic;

public interface ILevelDao {
    string getLevelByNumber(int number);

    List<string> getLevels();

    int getLevelsCountByPack(LevelPack pack);
}
