using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OptionsScript : MonoBehaviour
{
    private GameObject audioManager;
    private AudioSource buttonEffect;
    public Slider musicSlider;
    public Slider soundSlider;
    void Start()
    {
        audioManager = GameObject.Find("--AudioManager--");
        musicSlider.value = audioManager.GetComponent<AudioPlayerManager>().tempMusicVolume;
        soundSlider.value = audioManager.GetComponent<AudioPlayerManager>().tempSoundVolume;
        buttonEffect = audioManager.transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void Back()
    {
        buttonEffect.Play();

        SceneManager.LoadScene("StartingScene");
    }

    void OnEnable()
    {
        //Register Slider Events
        if (musicSlider != null)
        { musicSlider.onValueChanged.AddListener(delegate { changeMusicVolume(musicSlider.value); }); }

        if (soundSlider != null)
        { soundSlider.onValueChanged.AddListener(delegate { changeSoundVolume(soundSlider.value); }); }
    }

    //Called when Slider is moved
    void changeMusicVolume(float sliderValue)
    {
        if (musicSlider != null)
        { 
            audioManager.GetComponent<AudioPlayerManager>().tempMusicVolume = sliderValue;
            audioManager.GetComponent<AudioPlayerManager>().ChangeMusicVolume();
        }
    }

    void changeSoundVolume(float sliderValue)
    {
        if (soundSlider != null)
        {
            audioManager.GetComponent<AudioPlayerManager>().tempSoundVolume = sliderValue;
            audioManager.GetComponent<AudioPlayerManager>().ChangeSoundVolume();
        }
    }

    void OnDisable()
    {
        if (musicSlider != null)
        //Un-Register Slider Events
        { musicSlider.onValueChanged.RemoveAllListeners(); }

        if (soundSlider != null)
        //Un-Register Slider Events
        { soundSlider.onValueChanged.RemoveAllListeners(); }
    }
}
