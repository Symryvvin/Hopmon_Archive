using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonManager<UIManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    [SerializeField] private Menu mainMenu;
    [SerializeField] private Menu settingsMenu;
    [SerializeField] private Menu selectLeveMenu;
    [SerializeField] private GameInterface gui;

    private Menu activeMenu;

    private List<Menu> menus;

    protected override void Init() {
        menus = new List<Menu> {
            mainMenu,
            settingsMenu,
            selectLeveMenu,
            gui
        };

        foreach (var menu in menus) {
            menu.InitMenu();
            menu.Close();
        }

        gui.InitGUI();

        activeMenu = mainMenu;
        activeMenu.Show();
    }

    public void ShowSettings() {
        CloseActiveMenu();
        ShowMenu(settingsMenu);
    }

    public void ShowSelectLevel() {
        CloseActiveMenu();
        ShowMenu(selectLeveMenu);
    }

    public void ShowMainMenu() {
        CloseActiveMenu();
        ShowMenu(mainMenu);
    }

    public void ShowGUI() {
        CloseActiveMenu();
        ShowMenu(gui);
    }

    private void CloseActiveMenu() {
        foreach (var menu in menus) {
            if (menu.isActive())
                menu.Close();
        }
    }

    private void ShowMenu(Menu menu) {
        menu.Show();
    }

    public void UpdateLevelStats(LevelStats stats) {
        gui.SetLevelStats(stats);
        gui.UpdateLevelStats();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!mainMenu.isActive())
                ShowMainMenu();
            else {
                mainMenu.Close();
            }
        }
    }
}