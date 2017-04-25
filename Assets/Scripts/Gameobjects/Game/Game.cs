using Assets.Scripts.Gameobjects.Game;
using UnityEngine;

public class Game : MonoBehaviour {
    public GameStatus status;
    public Level level;
    public Player player;
    public HUD hud;
    private LevelStats stats;

    void Start() {
        status = GameStatus.INITIALIZE;
        hud.StartListeners();
        EventManager.StartListener(GameEvents.START_GAME, StartGame);
        EventManager.StartListener(GameEvents.WARP_CRISTAL, WarpCristal);
        EventManager.StartListener(GameEvents.DEFEATE, Defeat);
        EventManager.StartListener(GameEvents.VICTORY, Victory);
    }

    private void StartGame() {
        level.Build();
        InitStatsAndHUD();
        player.MoveToStart(level);
        player.ResetPlayer();
        status = GameStatus.STARTED;
    }

    private void InitStatsAndHUD() {
        stats = new LevelStats(level);
        hud.InitHUD(stats);
        EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
    }

    private void WarpCristal() {
        stats.cristals--;
        EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
        if (stats.cristals == 0) {
            EventManager.TriggerEvent(GameEvents.VICTORY);
        }
    }

    public void NextLevel() {
        level = LevelManager.NextLevel(level);
        StartGame();
    }

    public void PrevLevel() {
        level = LevelManager.PrevLevel(level);
        StartGame();
    }

    private void Victory() {
        Invoke("NextLevel", 3f);
    }

    private void Defeat() {
        Invoke("StartGame", 1f);
    }
}