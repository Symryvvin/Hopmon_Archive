using UnityEngine;

public class GameManager : MonoBehaviour {
    private static int cristalCount;                            // static count of cristals on current level
    public static int number = 1;                               // static number of current level

    public LevelManager levelManager;                           // LevelManager wich use GameManager
    public UIManager uiManager;                                 // UIManager in future
    public GameCamera mainCamera;                               // main game camera

    private Transform playerTransform;                          // Transform of player gameobject
    private Player player;                                      // main player script
    private Level level;                                        // current level data

    void Start() {
        InitGame();
        Restart();
    }

    /// <summary>
    /// Initialize all object first time.
    /// Fill dictionary for levelManager.
    /// Instantiale player.
    /// Set camera for palyer and set target for camera
    /// </summary>
    private void InitGame() {
        levelManager.SetDictionary();
        var hopmon = Instantiate(levelManager.GetPrefabByName("Hopmon", true), Vector3.zero, Quaternion.identity);
        player = hopmon.GetComponent<Player>();
        mainCamera.target = hopmon.transform;
        hopmon.GetComponent<PlayerMoveControll>().camera = mainCamera;
    }

    /// <summary>
    /// Restart current level (same as new level)
    /// </summary>
    private void Restart() {
        LoadLevel(number);
        ResetPlayer();
        cristalCount = level.cristals;
    }

    /// <summary>
    /// Reset player transform and reloading fire
    /// </summary>
    private void ResetPlayer() {
        player.transform.position = level.start + Vector3.up / 10f;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<PlayerFire>().Reload();
    }

    /// <summary>
    /// Loadl new level and destroy current level
    /// </summary>
    /// <param name="number">number of level</param>
    private void LoadLevel(int number) {
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

    void Update() {
        CheckPLayerState();
    }

    private void CheckPLayerState() {
        if (player.liveState == LiveState.DEAD) {
            Loose();
        }
    }

    /// <summary>
    /// Decrement cristall count when player bring cristal to warpzone
    /// </summary>
    public void DecrementCristal() {
        cristalCount--;
        Win();
    }

    private void Win() {
        if (cristalCount == 0) {
            DebugNextLevel(); // temp call of method "go to next level"
            //TODO: make action for win and go no next level
        }
    }

    private void Loose() {
        //TODO: make action for loose

    }
}