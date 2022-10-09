using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private GameObject enemyBase;
    private GameObject playerBase;
    public GameObject pauseMenu;
    public GameObject onImage;
    public GameObject offImage;
    private GameObject audioManager;
    private AudioSource buttonEffect;

    private TMP_Text coinUI;
    private Color originColor;
    private bool isSpeedUp;

    

    public int playerCoin;
    public int enemyCoin;
    private float nextIncreaseTime = 0.0f;
    public float period;

    //public Node[] nodeList;

    Node enemyNode;
    Node playerNode;

    void Start()
    {
        Time.timeScale = 0.5f;
        isSpeedUp = false;

        enemyBase = GameObject.Find("EnemyCapital");
        enemyNode = enemyBase.GetComponent<Node>();

        playerBase = GameObject.Find("PlayerCapital");
        playerNode = playerBase.GetComponent<Node>();

        coinUI = GameObject.Find("PlayerCoin").GetComponent<TMP_Text>();

        playerCoin = 0;
        enemyCoin = 0;
        period = 2.0f;

        audioManager = GameObject.Find("--AudioManager--");

        if (audioManager != null)
        { 
            audioManager.GetComponent<AudioPlayerManager>().PlayGameMusic();
            buttonEffect = audioManager.transform.GetChild(0).GetComponent<AudioSource>();
        }

        /*nodeList = FindObjectsOfType<Node>() as Node[];*/// Node scriptini cagirip train'i yap.
    }

    void Update()
    {
        if(enemyNode.faction == Faction.PLAYER)
        {
            SceneManager.LoadScene("WinScreen");
        }

        else if(playerNode.faction == Faction.ENEMY)
        {
            SceneManager.LoadScene("LoseScreen");
        }

        coinUI.SetText("Coin: " + playerCoin.ToString());

        if (SceneManager.GetActiveScene().name != "TutorialScene")
        {
            if (Time.time > nextIncreaseTime)
            {
                nextIncreaseTime = Time.time + period;
                playerCoin += 1;
                enemyCoin += 1;
            }
        }

        //StartCoroutine(Train());
    }

    void Awake()
    {
        instance = this;
    }

    public Node selectedNode;
    public Node nextNode;

    public void HandleNodes(Node node, Button bttn)
    {
        //Null
        if(selectedNode == node || (node == null && bttn == null))
        {
            if (selectedNode != null) { selectedNode.GetComponent<Renderer>().material.color = originColor; }

            selectedNode = null;
            nextNode = null;
            return;
        }

        //Selected
        if(selectedNode == null && node.faction == Faction.PLAYER)
        {
            //Debug.Log(selectedNode + "is Selected");
            selectedNode = node;
            originColor = selectedNode.GetComponent<Renderer>().material.color;
            selectedNode.GetComponent<Renderer>().material.color = Color.yellow;
            return;
        }

        //Next
       if(selectedNode != node && selectedNode!= null && bttn == null && selectedNode.faction == Faction.PLAYER)
        {
            nextNode = node;
            //send units
            //Debug.Log(nextNode + "is Targeted");

            selectedNode.SendTroops(nextNode);

            selectedNode.GetComponent<Renderer>().material.color = originColor;

            selectedNode = null;
            nextNode = null;

        }

    }

    public void UpgradeNode() //bunuda node'da upgrade fonksiyonu cagirma yap
    {
        buttonEffect.Play();

        //GameObject nodeToUpgrade;
        if (selectedNode != null && playerCoin >= 20)
        {GameObject.Find(selectedNode.name).GetComponent<Node>().UpgradeHandler(selectedNode); playerCoin -= 20; }

        else if (selectedNode != null && playerCoin < 20) { Debug.Log("You do not have enough coins"); }

        else { Debug.Log("You have not selected any bases"); }
    }

    public void MenuButton()
    {
        buttonEffect.Play();

        if(Time.timeScale != 0.0f)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        else if(Time.timeScale == 0 && isSpeedUp == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 0.5f; 
        }

        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void DoubleSpeed()
    {
        buttonEffect.Play();

        if (Time.timeScale != 1.0f)
        { 
            Time.timeScale = 1.0f;
            offImage.SetActive(false);
            onImage.SetActive(true);
        }
        else 
        { 
            Time.timeScale = 0.5f;
            onImage.SetActive(false);
            offImage.SetActive(true);           
        }

    }

    public void LeaveGame()

    {
        buttonEffect.Play();

        SceneManager.LoadScene("StartingScene");
    }
    //IEnumerator Train()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    foreach (Node node in nodeList)
    //    {
    //        node.TrainTroops();
    //    }
    //}

}
