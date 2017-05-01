using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameobjects.Level;
using UnityEngine;
using UnityEngine.UI;

// Sory for many stupid comments, i am tried to refactor this class
public class SelectLevelMenu : Menu {
    private Pack currentPack;
    public Level currentLevel;
    public Text levelPack;
    public RectTransform levelsPanel;
    public RectTransform levelButtonPrefab;
    private SelectLevelButton selectedLevel;

    // If player click "Start" in Main Menu this menu is enable
    void OnEnable() {
        Camera.main.GetComponent<OrbitCamera>().isRotate = true;
        currentPack = LevelManager.GetCurrentPack();
        LoadLevelList();
    }

    // If player click "Back" in Select Level Menu this menu is disable
    void OnDisale() {
        Camera.main.GetComponent<OrbitCamera>().isRotate = false;
    }

    // Load first level from pack in future must load last incomplete level get from profile
    public void Play() {
        if (currentLevel == null)
            currentLevel = currentPack.GetFirstLevelInPack();
        GameManager.level = currentLevel;
        GameManager.instance.StartGame();
    }

    public void SelectLevel() {
        // This method implement in inspector
    }

    // Return to Main Menu
    public void Back() {
        UIManager.instance.ShowMainMenu();
        LevelBuilder.DestroyLevel();
    }

    // Change pack, set current level pack and load button list for pick level
    public void SwitchLevelPack() {
        currentPack = LevelManager.SwitchLevelPack();
        LoadLevelList();
    }

    private void LoadLevelList() {
        SetLevelPackName();
        ClearLevelList();
        IDictionary<int, Level> levels = currentPack.GetLevels();
        List<Button> selectButtons = new List<Button>();
        foreach (var level in levels.Values) {
            var button = Instantiate(levelButtonPrefab, levelsPanel).GetComponent<Button>();
            button.GetComponent<SelectLevelButton>().level = level;
            selectButtons.Add(button);
        }
        LinkButtons(selectButtons);
        ActivateFirstButton(selectButtons.First());
    }

    private void LinkButtons(List<Button> buttons) {
        int count = buttons.Count;
        for (int i = 0; i < buttons.Count; i++) {
            var pick = buttons[i].GetComponent<SelectLevelButton>();
            if (i == 0) {
                pick.AddNextPrevButtons(buttons[i + 1],
                    buttons[count - 1]);
            }
            else if (i == count - 1) {
                pick.AddNextPrevButtons(buttons[0],
                    buttons[i - 1]);
            }
            else {
                pick.AddNextPrevButtons(buttons[i + 1],
                    buttons[i - 1]);
            }
        }
    }

    private void ActivateFirstButton(Button first) {
        var pick = first.GetComponent<SelectLevelButton>();
        pick.OnSelect();
    }

    private void ClearLevelList() {
        if (levelsPanel.childCount == 0) return;
        foreach (RectTransform rt in levelsPanel) {
            Destroy(rt.gameObject);
        }
    }

    private void SetLevelPackName() {
        levelPack.text = currentPack.packName;
    }

    public void ActiveLevel(SelectLevelButton button) {
        if (selectedLevel != null) {
            selectedLevel.Deactivate();
        }
        currentLevel = button.level;
        selectedLevel = button;
    }
}