using System.Collections;
using Assets.Scripts.Gameobjects.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    public static Level level;

    private Game game;

    protected override void Init() {
        //TODO получает данные из профиля и устанавливает номер уровня и пак который был последним
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
        StartCoroutine(WaitForLoadGameScene());
    }

    private IEnumerator WaitForLoadGameScene() {
        while (true) {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game")) {
                game = GameObject.Find("Game").GetComponent<Game>();
                game.level = level;
                Debug.Log(game.status);
                EventManager.TriggerEvent(GameEvents.START_GAME);
                Debug.Log(game.status);
                break;
            }
            yield return null;
        }
    }
}