using UnityEngine;

public class GameManager : MonoBehaviour {
    public static int cristallCount;
    public static int number = 43;

    public LevelManager levelManager;
    public UIManager uiManager;

    private GameObject player;
    private GameCamera mainCamera;
    private Level level;

    void Start() {
        levelManager.SetDictionary();
        player = Instantiate(levelManager.GetPrefabByName("Hopmon", true), Vector3.zero, Quaternion.identity);
        NewGame();
        mainCamera = Camera.main.GetComponent<GameCamera>();
        mainCamera.target = player.transform;
        player.GetComponent<PlayerMoveControll>().camera = mainCamera;
    }

    private void InitGame() {

    }

    private void Restart() {
    }

    private void NewGame() {
        level = LoadLevel(number);
        player.transform.position = level.start + Vector3.up / 10f;
        player.transform.rotation = Quaternion.identity;
        cristallCount = 0;
    }

    public void DebugPrevLevel() {
        UnLoadMap();
        if (number > 1)
            number--;
        NewGame();

    }

    public void DebugNextLevel() {
        UnLoadMap();
        if (number < 45)
            number++;
        NewGame();
    }

    private void UnLoadMap() {
        foreach (Transform child in levelManager.level) {
            Destroy(child.gameObject);
        }
    }

    private Level LoadLevel(int number) {
        levelManager.LoadMap(number);
        return new Level(levelManager.jsonLevel);
    }

    private void Win() {
    }

    private void Loose() {
    }
}
