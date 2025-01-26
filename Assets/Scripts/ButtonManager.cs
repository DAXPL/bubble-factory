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
    
    public void SoundEffectsSlider(float volume) {
        if (SoundEffectsManager.instance == null)
            return;

        SoundEffectsManager.instance.GetAudioSource().volume = volume;
    }
}
