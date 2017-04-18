using UnityEngine;

public class SelectLevelMenu : Menu, ISelectLevelMenu {
    private int number = 1;
    [SerializeField] private RectTransform levels;

    public void Play() {
        GameManager.number = number;
        GameManager.instance.StartGame();
        UIManager.instance.ShowGUI();
    }

    public void SelectLevel() {
       // LevelManager.instance.LoadLevelParts(number);
        //Activale select level Panel
    }

    public void Back() {
        UIManager.instance.ShowMainMenu();
    }

    public void ChangeLevel(int n) {
      //  LevelManager.instance.LoadLevelParts(n);
    }
}
