using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sory for many stupid comments, i am tried to refactor this class
public class SelectLevelMenu : Menu {
    private Pack currentPack;
    private Level currentLevel;
    public RectTransform levelsView;
    public Button play;
    public Button select;
    public Button back;
    public Text levelPackName;
    public RectTransform levelsPanel;
    public RectTransform levelButton;
    private Image currentLevelSelect;

    // If player click "Start" in Main Menu this menu is enable
    void OnEnable() {
        Camera.main.GetComponent<OrbitCamera>().isRotate = true;
        currentPack = LevelManager.GetCurrentPack();
        BuildLevel(FirstLevelInPack());
    }

    // If player click "Back" in Select Level Menu this menu is disable
    void OnDisale() {
        Camera.main.GetComponent<OrbitCamera>().isRotate = false;
    }

    // Load first level from pack in future must load last incomplete level get from profile
    public void Play() {
        if (currentLevel == null)
            currentLevel = FirstLevelInPack();
        GameManager.level = currentLevel;
        GameManager.instance.StartGame();
    }

    // Activate panel with level list
    public void SelectLevel() {
        SelectActivation(!levelsView.gameObject.activeSelf);
    }

    // Return to Main Menu
    public void Back() {
        UIManager.instance.ShowMainMenu();
        LevelBuilder.DestroyLevel();
    }

    private void SelectActivation(bool active) {
        levelsView.gameObject.SetActive(active);
        play.interactable = !active;
        back.interactable = !active;
        if (active) {
            LoadLevelList();
        }
    }

    // Change pack, set current level pack and load button list for pick level
    public void ChangeLevelPack() {
        currentPack = LevelManager.SwitchLevelPack();
        LoadLevelList();
    }

    private void LoadLevelList() {
        SetLevelPackName();
        ClearLevelList();
        bool isLoaded = false;
        IDictionary<int, Level> levels = currentPack.GetLevels();
        foreach (var level in levels.Values) {
            var changeButton = Instantiate(levelButton, levelsPanel);
            Text text = changeButton.FindChild("Number").GetComponent<Text>();
            Image image = changeButton.GetComponent<Image>();
            Button button = changeButton.GetComponent<Button>();
            text.text = level.number.ToString();
            AddListenerToButton(button, level, image);
            if (!isLoaded) {
                ChangeLevel(level, image);
                isLoaded = true;
            }
        }
    }

    private void AddListenerToButton(Button button, Level level, Image image) {
        button.onClick.AddListener(delegate { ChangeLevel(level, image); });
    }

    private void ClearLevelList() {
        if (levelsPanel.childCount == 0) return;
        foreach (RectTransform rt in levelsPanel) {
            Destroy(rt.gameObject);
        }
    }

    private void SetLevelPackName() {
        levelPackName.text = currentPack.packName;
    }

    public void ChangeLevel(Level level, Image image) {
        if (currentLevelSelect != null)
            currentLevelSelect.fillCenter = false;
        currentLevelSelect = image;
        currentLevelSelect.fillCenter = true;
        currentLevel = level;
        BuildLevel(currentLevel);
    }

    private Level FirstLevelInPack() {
        return currentPack.GetFirstLevelInPack();
    }

    private void BuildLevel(Level level) {
        level.BuildPart();
    }
}