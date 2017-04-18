using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonManager<UIManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    [SerializeField] private Menu mainMenu;
    [SerializeField] private Menu settingsMenu;
    [SerializeField] private Menu selectLeveMenu;
    [SerializeField] private HUD hud;

    private Menu activeMenu;

    private List<Menu> menus;

    protected override void Init() {
        menus = new List<Menu> {
            mainMenu,
            settingsMenu,
            selectLeveMenu,
            hud
        };

        foreach (var menu in menus) {
            menu.InitMenu();
            menu.Close();
        }

        activeMenu = mainMenu;
        activeMenu.Show();
    }

    public void ShowSettings() {
        ShowMenu(settingsMenu);
    }

    public void ShowSelectLevel() {
        ShowMenu(selectLeveMenu);
    }

    public void ShowMainMenu() {
        ShowMenu(mainMenu);
    }

    public void ShowGUI() {
        ShowMenu(hud);
    }

    public static HUD GetHUD() {
        return instance.hud;
    }

    private void CloseActiveMenu() {
        foreach (var menu in menus) {
            if (menu.isActive())
                menu.Close();
        }
    }

    private void ShowMenu(Menu menu) {
        CloseActiveMenu();
        menu.Show();
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