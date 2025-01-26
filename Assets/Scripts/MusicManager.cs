using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager instance;
    private AudioSource source;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;

        source = GetComponent<AudioSource>();
    }

    public AudioSource GetAudioSource() { 
        return source;
    }
}
