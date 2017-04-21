using UnityEngine;

public class MainMenu : Menu {

    public void StartGame() {
        GameManager.instance.StartGame();
    }

    public void Settings() {
        UIManager.instance.ShowSettings();
    }

    public void SelectLevel() {
        UIManager.instance.ShowSelectLevel();
    }

    public void Exit() {
        Application.Quit();
    }
}
