public class GameManager : SingletonManager<GameManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public static int number = 1; // static number of current level
    private LevelManager levelManager; // LevelManager wich use GameManager
    private UIManager uiManager; // UIManager in future
    private Player player; // main player script
    private Level level; // current level data
    private LevelStats stats;

    protected override void Init() {
        uiManager = UIManager.instance;
        levelManager = LevelManager.instance;
        EventManager.StartListener("warpCristall", DecrementCristal);
        EventManager.StartListener("loseGame", Loose);
    }


    /// <summary>
    /// StartGame current level (same as new level)
    /// </summary>
    public void StartGame() {
        LoadLevel();
        InitPlayer();
        EventManager.TriggerEvent("Reset");
        stats = new LevelStats(number, level.cristals);
        uiManager.UpdateLevelStats(stats);
    }

    /// <summary>
    /// Instance player if null and reset player prefences
    /// </summary>
    private void InitPlayer() {
        if (player == null) {
            player = levelManager.GetPlayerInstance().GetComponent<Player>();
        }
        player.SetStart(level.start);
        player.ResetPlayer();
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
        StartGame();
    }

    /// <summary>
    /// Debug method for next level button
    /// </summary>
    public void DebugNextLevel() {
        levelManager.UnLoadLevelMap();
        if (number < 45)
            number++;
        StartGame();
    }

    /// <summary>
    /// Decrement cristall count when player bring cristal to warpzone
    /// </summary>
    private void DecrementCristal() {
        stats.CristalCount--;
        uiManager.UpdateLevelStats(stats);
        if (stats.CristalCount == 0) {
            Win();
        }
    }

    private void Win() {
        Invoke("DebugNextLevel", 3f); // temp call of method "go to next level"
    }

    private void Loose() {
        Invoke("StartGame", 1f);
    }
}
