using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class ButtonSoundMixer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds;
    private float startPitch = 0;
    private float startVolume = 0;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPitch = audioSource.pitch;
        startVolume = audioSource.volume;
    }

    public void PlaySound()
    {
        if(audioSource == null) return;//Guardian
        if(sounds.Length <=0) return;//Guardian
        //Audio fatigue
        //https://www.tiktok.com/@streamingpirate/video/7440196626741742891
        audioSource.pitch = startPitch * Random.Range(0.9f,1.1f);
        audioSource.volume = startVolume * Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sounds[Random.Range(0,sounds.Length)]);
    }

}
