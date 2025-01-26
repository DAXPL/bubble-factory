using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "ButtonManager", menuName = "ButtonManager/New Button Manager")]
public class ButtonManager : ScriptableObject {

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void MusicSlider(float volume) {
        if (MusicManager.instance == null)
            return;

        MusicManager.instance.GetAudioSource().volume = volume;
    }
    
    

    public void HideUpgrades(CanvasGroup canvasGroup) {
        if (GameManager.Instance.GetFactoriesAmount() < 2) {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.5f;
            return;
        }

        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;

    }
}
