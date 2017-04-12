public class SeletLevelMenu : Menu, ISelectLevelMenu {
    private int number;


    public void Play() {
        GameManager.number = number;
        GameManager.instance.StartGame();
        UIManager.instance.ShowGUI();
    }

    public void SelectLevel() {
        //Activale select level Panel
    }

    public void Back() {
        UIManager.instance.ShowMainMenu();
    }
}
