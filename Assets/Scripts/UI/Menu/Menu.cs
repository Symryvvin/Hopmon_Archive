using UnityEngine;

public class Menu : MonoBehaviour {
    private RectTransform rect;

    public void Show() {
        if (!isActive()) {
            gameObject.SetActive(true);
        }
    }

    public void Close() {
        if (isActive()) {
            gameObject.SetActive(false);
        }
    }

    public bool isActive() {
        return gameObject.activeSelf;
    }

    public void InitMenu() {
        rect = GetComponent<RectTransform>();
        rect.offsetMax = new Vector2(5, 5);
        rect.offsetMin = new Vector2(5, 5);
        Close();
    }


}