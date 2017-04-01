public class GameManager : SingletonManager<GameManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    private static int cristalCount; // static count of cristals on current level
    public static int number = 1; // static number of current level
    private LevelManager levelManager; // LevelManager wich use GameManager
    private UIManager uiManager; // UIManager in future
    private Player player; // main player script
    private Level level; // current level data

    protected override void Init() {
        levelManager = LevelManager.instance;
        EventManager.StartListener("warpCristall", DecrementCristal);
        EventManager.StartListener("loseGame", Loose);
    }


    /// <summary>
    /// Restart current level (same as new level)
    /// </summary>
    public void Restart() {
        LoadLevel();
        InitPlayer();
        cristalCount = level.cristals;
    }

    /// <summary>
    /// Instance player if null and reset player prefences
    /// </summary>
    private void InitPlayer() {
        if (player == null) {
            player = levelManager.GetPlayerInstance().GetComponent<Player>();
            player.Init();
        }
        player.ResetPlayer(level.start);
    }

    /// <summary>
    /// Loadl new level and destroy current level
    /// </summary>
    private void LoadLevel() {
        levelManager.UnLoadLevelMap();
        level = levelManager.LoadLevelMap(number);
    }

    /// <summary>
    /// Debug method for previous level button
    /// </summary>
    public void DebugPrevLevel() {
        levelManager.UnLoadLevelMap();
        if (number > 1)
            number--;
        Restart();
    }

    /// <summary>
    /// Debug method for next level button
    /// </summary>
    public void DebugNextLevel() {
        levelManager.UnLoadLevelMap();
        if (number < 45)
            number++;
        Restart();
    }

    /// <summary>
    /// Decrement cristall count when player bring cristal to warpzone
    /// </summary>
    public void DecrementCristal() {
        cristalCount--;
        if (cristalCount == 0) {
            Win();
        }
    }

    private void Win() {
        Invoke("DebugNextLevel", 3f); // temp call of method "go to next level"
    }

    private void Loose() {
        Invoke("Restart", 1f);
    }
}