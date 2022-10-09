using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesScript : MonoBehaviour
{

    private GameObject audioManager;
    private AudioSource buttonEffect;
    void Start()
    {
        audioManager = GameObject.Find("--AudioManager--");
        buttonEffect = audioManager.transform.GetChild(0).GetComponent<AudioSource>();
    }
    public void Back()
    {
        buttonEffect.Play();

        SceneManager.LoadScene("StartingScene");
    }
}
