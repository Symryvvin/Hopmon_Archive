using Assets.Scripts.Gameobjects.Game;
using UnityEngine;

public class Game : MonoBehaviour {
    public GameStatus status;
    public Level level;
    public Player player;
    public HUD hud;
    public LevelService levelService;
    private LevelStats stats;

    void Start() {
        status = GameStatus.INITIALIZE;
        hud.StartListeners();
        EventManager.StartListener(GameEvents.START_GAME, StartGame);
        EventManager.StartListener(GameEvents.NEXT_LEVEL, NextLevel);
        EventManager.StartListener(GameEvents.PREV_LEVEL, PrevLevel);
        EventManager.StartListener(GameEvents.WARP_CRISTAL, WarpCristal);
    }

    private void StartGame() {
        BuildLevel();
        InitStatsAndHUD();
        player.MoveToStart(level);
        player.ResetPlayer();
        status = GameStatus.STARTED;
    }

    private void BuildLevel() {
        LevelManager.BuildLevel(level, false);
    }

    private void InitStatsAndHUD() {
        level.cristals = CalculateCristalsCount();
        stats = new LevelStats(level);
        hud.InitHUD(stats);
        EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
    }

    private int CalculateCristalsCount() {
        int count = 0;
        foreach (var part in level.tiles.structures) {
            if (part.name.Equals("Cristal"))
                count++;
        }
        return count;
    }

    private void WarpCristal() {
        stats.cristals--;
        EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
        if (stats.cristals == 0) {
            EventManager.TriggerEvent(GameEvents.VICTORY);
        }
    }

    private void NextLevel() {
     //   level = LevelManager.NextLevel();
    //    if (number < levelService.GetMaxLevelNumberInPack(LevelManager.GetCurrentPack()))
    //        number++;
        Start();
    }

    private void PrevLevel() {
   //     if (number > 1)
    //        number--;
        Start();
    }
}