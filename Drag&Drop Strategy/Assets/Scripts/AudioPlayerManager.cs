using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayerManager : MonoBehaviour
{
    private static AudioPlayerManager instance = null;
   
    public AudioSource menuMusic;
    public AudioSource gameMusic;
    public AudioSource tempMusic;
    public AudioSource buttonEffect;
    //public AudioSource troopEffect;
    
    public float tempMusicVolume;
    public float tempSoundVolume;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        tempSoundVolume = 1.0f;
        tempMusicVolume = 1.0f;
        PlayMenuMusic();
    }

    public void ChangeMusicVolume()
    {
        menuMusic.volume = tempMusicVolume;
        gameMusic.volume = tempMusicVolume;
    }

    public void ChangeSoundVolume()
    {
        buttonEffect = this.transform.GetChild(0).GetComponent<AudioSource>();
        //troopEffect = this.transform.GetChild(1).GetComponent<AudioSource>();
        buttonEffect.volume = tempSoundVolume;
        //troopEffect.volume = tempSoundVolume;
    }

    public void PlayMenuMusic()
    {
        gameMusic.Stop();

        menuMusic.volume = tempMusicVolume;
        menuMusic.Play();

        tempMusic = menuMusic;
    }

    public void PlayGameMusic ()
    {
        menuMusic.Stop();

        gameMusic.volume = tempMusicVolume;
        gameMusic.Play();

        tempMusic = gameMusic;
    }
}
