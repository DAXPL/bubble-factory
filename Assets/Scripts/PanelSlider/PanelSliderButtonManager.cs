using UnityEngine;

[CreateAssetMenu(fileName = "PanelSliderButtonManager", menuName = "ButtonManager/New Panel Slider Button Manager")]
public class PanelSliderButtonManager : ScriptableObject {

    public void ToggleCanvasGroup(CanvasGroup canvasGroup) {
        canvasGroup.interactable = !canvasGroup.interactable;
    }

    public void ToggleMenu(PanelSlider menu) {
        menu.StartMoving();
    }

}
