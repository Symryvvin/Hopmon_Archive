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
    }

    public void AddNextPrevButtons(Button next, Button previous) {
        this.next = next;
        this.previous = previous;
    }

    public void OnSelect() {
        menu.ActiveLevel(this);
        level.BuildPart();
        image.fillCenter = true;
        NavigationUtils.ExplicitNavigation(button, selectLevelPack, selectLevelPack, previous, next);
        NavigationUtils.ExplicitNavigation(selectLevelPack, button, button, null, null);
    }

    public void Deactivate() {
        image.fillCenter = false;
        NavigationUtils.DeactivateNavigation(button);
    }
}