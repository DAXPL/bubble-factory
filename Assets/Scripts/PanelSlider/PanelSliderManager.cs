using UnityEngine;

public class PanelSliderManager : MonoBehaviour {
    public static PanelSliderManager instance;
    [SerializeField] private PanelSlider openedMenu;

    private void Awake() {
        instance = this;
    }

    public void SetOpenedMenu(PanelSlider menu) {
        openedMenu = menu;
    }

    public PanelSlider GetOpenedMenu() {
        return openedMenu;
    }
}
