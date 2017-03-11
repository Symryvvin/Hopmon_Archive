using UnityEngine;

public class GameManager : MonoBehaviour {
    public static int cristallCount;
    public static int number = 1;

    public LevelManager levelManager;
    public UIManager uiManager;

    private GameObject player;
    private GameCamera mainCamera;
    private Level level;

    void Start() {
        levelManager.SetDictionary();
        player = Instantiate(levelManager.GetPrefabByName("Hopmon", true), Vector3.zero, Quaternion.identity);
        mainCamera = Camera.main.GetComponent<GameCamera>();
        mainCamera.target = player.transform;
        player.GetComponent<PlayerMoveControll>().camera = mainCamera;
        NewGame();
    }

    private void InitGame() {

    }

    private void Restart() {
    }

    private void NewGame() {
        LoadLevel(number);
        player.transform.position = level.start + Vector3.up / 10f;
        player.transform.rotation = Quaternion.identity;
        cristallCount = 0;
        print(level.width + " x " + level.length);
    }

    public void DebugPrevLevel() {
        levelManager.UnLoadLevelMap();
        if (number > 1)
            number--;
        NewGame();

    }

    public void DebugNextLevel() {
        levelManager.UnLoadLevelMap();
        if (number < 45)
            number++;
        NewGame();
    }


    private void LoadLevel(int number) {
       level = levelManager.LoadLevelMap(number);
    }

    private void Win() {
    }

    private void Loose() {
    }
}
