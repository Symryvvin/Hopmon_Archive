using UnityEngine.UI;

public class NavigationUtils {

    private NavigationUtils() {
    }

    public static void DeactivateNavigation(Button button) {
        Navigation navigation = button.navigation;
        navigation.mode = Navigation.Mode.None;
        button.navigation = navigation;
    }

    public static void ExplicitNavigation(Button button, Selectable up, Selectable down, Selectable left, Selectable right) {
        Navigation navigation = button.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnUp = up;
        navigation.selectOnDown = down;
        navigation.selectOnLeft = left;
        navigation.selectOnRight = right;
        button.navigation = navigation;
    }
}
