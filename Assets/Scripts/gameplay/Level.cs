/// <summary>
/// Class keep info of loaded level. This information will be used by GameManager
/// </summary>
public class Level {
    public int number { get; private set; }
    public int cristals { get; private set; }
    public string name { get; private set; }
    public string world { get; private set; }
    public int length { get; private set; }
    public int width { get; private set; }
    public UnityEngine.Vector3 start { get; private set; }
    private JsonLevelStruct levelStruct;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="levelStruct">json to object from JSON string</param>
    public Level(JsonLevelStruct levelStruct) {
        this.levelStruct = levelStruct;
        InitalizeLevel();
    }

    /// <summary>
    /// Init class fields
    /// </summary>
    private void InitalizeLevel() {
        number = levelStruct.number;
        cristals = GetCristallCount();
        name = levelStruct.name;
        start = levelStruct.start;
        world = levelStruct.world;
        GetLevelSize();
    }

    /// <summary>
    /// Init count of cristall by counting all cristall from level parts
    /// </summary>
    /// <returns>cristal count</returns>
    private int GetCristallCount() {
        int count = 0;
        foreach (var part in levelStruct.parts) {
            if (part.name.Equals("Cristal"))
                count++;
        }
        return count;
    }

    /// <summary>
    /// Init level size
    /// </summary>
    private void GetLevelSize() {
        int width = (int) levelStruct.parts[0].position.x;
        int length = (int) levelStruct.parts[0].position.z;
        foreach (var part in levelStruct.parts) {
            if (part.position.x > width)
                width = (int) part.position.x;
            if (part.position.z > length)
                length = (int) part.position.z;
        }
        this.width = width;
        this.length = length;
    }
}