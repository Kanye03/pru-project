using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source-----------")] 
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("---------Audio Clip-------------")]
    public AudioClip backgroud;
    public AudioClip death;


    private void Start()
    {
        musicSource.clip = backgroud;
        musicSource.Play();
    }
}
