using Assets.Scripts.Gameobjects.Game;

public class Game {
    public GameStatus status;

    private Level level;
    private Player player;
    private int number;
    private LevelStats stats;
    private readonly HUD hud;
    private readonly LevelService levelService = new LevelService();

    public Game(int number) {
        status = GameStatus.INITIALIZE;
        this.number = number;
        hud = UIManager.GetHUD();
        hud.StartListeners();
        CreatePlayer();
        EventManager.StartListener(GameEvents.START_GAME, Start);
        EventManager.StartListener(GameEvents.NEXT_LEVEL, NextLevel);
        EventManager.StartListener(GameEvents.PREV_LEVEL, PrevLevel);
        EventManager.StartListener(GameEvents.WARP_CRISTAL, WarpCristal);
    }

    private void CreatePlayer() {
        if (player == null) {
            player = levelService.InstancePlayer();
        }
    }

    private void Start() {
        CreateLevel();
        InitStatsAndHUD();
        player.MoveToStart(level);
        player.ResetPlayer();
        status = GameStatus.STARTED;
    }

    private void CreateLevel() {
        level = LevelManager.GetLevelByNumber(number);
        LevelManager.CreateLevel(level);
    }

    private void InitStatsAndHUD() {
        var cristal = levelService.GetCristallCount(level);
        var enemies = 0;
        stats = new LevelStats(number, cristal, enemies);
        hud.InitHUD(stats);
        EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
    }

    private void WarpCristal() {
        stats.cristal--;
        EventManager.TriggerEvent(GameEvents.UPDATE_HUD);
        if (stats.cristal == 0) {
            EventManager.TriggerEvent(GameEvents.VICTORY);
        }
    }

    private void NextLevel() {
        if (number < levelService.GetMaxLevelNumberInPack(LevelManager.GetCurrentPack()))
            number++;
        Start();
    }

    private void PrevLevel() {
        if (number > 1)
            number--;
        Start();
    }
}