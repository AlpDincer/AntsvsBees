using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour
{

    private GameObject audioManager;
    private AudioSource buttonEffect;

    private void Start()
    {
        audioManager = GameObject.Find("--AudioManager--");
        if (audioManager.GetComponent<AudioPlayerManager>().tempMusic != audioManager.GetComponent<AudioPlayerManager>().menuMusic)
        { audioManager.GetComponent<AudioPlayerManager>().PlayMenuMusic(); }

        buttonEffect = audioManager.transform.GetChild(0).GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("playedBefore") == 0) //Baslangicta oyuncu daha once oynadi mi bak
        {
            SceneManager.LoadScene("TutorialScene");
            PlayerPrefs.SetInt("playedBefore", 1); //Tutoriala girildi olarak isaretle
        }
    }
    public void StartGame()
    {
        buttonEffect.Play();

        if(Time.timeScale == 0.0f)
        {
            Time.timeScale = 0.5f;
        }
        SceneManager.LoadScene("GameScene");
    }

    public void Tutorial()
    {
        buttonEffect.Play();

        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 0.5f;
        }
        SceneManager.LoadScene("TutorialScene");
    }

    public void HowTo()
    {
        buttonEffect.Play();

        SceneManager.LoadScene("RulesScene");
    }

    public void Options()
    {
        buttonEffect.Play();

        SceneManager.LoadScene("OptionsScene");
    }

    public void Exit()
    {
        buttonEffect.Play();

        Application.Quit();
    }
}
