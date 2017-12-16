using System.Collections;
using Assets.Scripts.Gameobjects.Games;
using Assets.Scripts.Gameobjects.Levels;
using Assets.Scripts.Managers.EventMessages;
using Assets.Scripts.Rules;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public static Level level;

    private Game game;
    private CollisionHandler handler;

    protected override void Init() {
        //Грузим тестовый уровень для отладки
        level = LevelManager.GetLevelByNumber(0);
        StartCoroutine(WaitForLoadGameScene());
        handler = CollisionHandler.instance;
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
        StartCoroutine(WaitForLoadGameScene());
        handler = CollisionHandler.instance;
        handler.StartRules();
    }

    private IEnumerator WaitForLoadGameScene() {
        while (true) {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game")) {
                game = GameObject.Find("Game").GetComponent<Game>();
                game.level = level;
                Debug.Log(game.status);
                if (game.status == GameStatus.READY) {
                    EventMessenger.TriggerEvent(GameEvents.START_GAME);
                    Debug.Log(game.status);
                    break;
                }
            }
            yield return null;
        }
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Main");
            handler.StopRules();
        }
    }
}