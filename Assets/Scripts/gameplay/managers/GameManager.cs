﻿using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    private static int cristalCount; // static count of cristals on current level
    public int number = 1; // static number of current level
    private LevelManager levelManager; // LevelManager wich use GameManager
    private UIManager uiManager; // UIManager in future
    private Player player; // main player script
    private Level level; // current level data

    private static GameManager gameManager;

    public static GameManager instance {
        get {
            if (gameManager == null) {
                gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (gameManager != null) {
                    gameManager.InitGame();
                }
                else {
                    Debug.LogError("No LevelManager on Scene");
                }
            }
            return gameManager;
        }
    }

    /// <summary>
    /// Game start point
    /// </summary>
    void Awake() {
        gameManager = instance;
        EventManager.StartListener("warpCristall", DecrementCristal);
        EventManager.StartListener("loseGame", Loose);
    }

    /// <summary>
    /// Initialize all object first time.
    /// Fill dictionary for levelManager.
    /// Instantiale player.
    /// Set gameCamera for palyer and set target for gameCamera
    /// </summary>
    private void InitGame() {
        levelManager = LevelManager.instance;
        gameManager.Restart();
    }

    /// <summary>
    /// Restart current level (same as new level)
    /// </summary>
    private void Restart() {
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
        //TODO: make action for win and go no next level
    }

    private void Loose() {
        Invoke("Restart", 1f);
        //TODO: make action for loose
    }
}