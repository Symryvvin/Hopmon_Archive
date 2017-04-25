using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickLevelButton : MonoBehaviour {
    public Level level;
    private Image image;
    private Button button;
    private Text number;
    private SelectLevelMenu menu;
    private Button selectLevelPack;
    private Button next;
    private Button previous;

    void Awake() {
        menu = GameObject.Find("SelectLevelMenu").GetComponent<SelectLevelMenu>();
        selectLevelPack = GameObject.Find("LevelPack").GetComponent<Button>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        number = GetComponentInChildren<Text>();
    }

    void Start() {
        number.text = level.number.ToString();

    }

    public void OnClick() {
        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(GameObject.Find("Play"));
        AudioManager.instance.PlayClick();
    }

    public void AddNextPrevButtons(Button next, Button previous) {
        this.next = next;
        this.previous = previous;
    }

    private void ChangeLevelPackButtonNavigation() {
        Navigation navigation = selectLevelPack.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnDown = button;
        navigation.selectOnUp = button;
        selectLevelPack.navigation = navigation;
    }

    public void OnSelect() {
        menu.ActiveLevel(this);
        level.BuildPart();
        image.fillCenter = true;
        Navigation navigation = button.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnUp = selectLevelPack;
        navigation.selectOnDown = selectLevelPack;
        navigation.selectOnLeft = previous;
        navigation.selectOnRight = next;
        button.navigation = navigation;
        ChangeLevelPackButtonNavigation();
        AudioManager.instance.PlaySelect();
    }

    public void Deactivate() {
        image.fillCenter = false;
        Navigation navigation = button.navigation;
        navigation.mode = Navigation.Mode.None;
        button.navigation = navigation;
    }
}