using System;
using UnityEngine;

public class MainMenu : Menu, IMainMenu {

    public void StartGame() {
        // get last completed level from database and load it with GameManager
       // GameManager.GetLastCompletedLevelNumber();
        GameManager.number = 1;
        GameManager.instance.StartGame();
        UIManager.instance.ShowGUI();
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
