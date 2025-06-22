using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source-----------")] 
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("---------Audio Clip-------------")]
    public AudioClip backgroud;
    public AudioClip death;
    [Header("---------UI Controls-------------")]
    [SerializeField] Slider musicVolumeSlider;

    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private float defaultMusicVolume = 0.5f;

    private void Start()
    {
        // Load saved music volume
        float savedVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, defaultMusicVolume);
        
        // Set up music
        musicSource.clip = backgroud;
        musicSource.volume = savedVolume;
        musicSource.Play();
        
        // Set up slider if available
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = savedVolume;
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
    }

    public void OnMusicVolumeChanged(float newVolume)
    {
        // Update music volume
        musicSource.volume = newVolume;
        
        // Save setting
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, newVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }
}
