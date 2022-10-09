using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public GameObject IntroItems;
    public GameObject firstHintItems;
    public GameObject secondHintItems;
    public GameObject thirdHintItems;
    public GameObject fourthHintItems;
    public GameObject fifthHintItems;
    public GameObject empyClickHint;
    public GameObject sixthHintItems;
    public GameObject seventhHintItems;
    public GameObject eighthHintItems;
    public GameObject ninthHintItems;
    public GameObject tenthHintItems;
    public GameObject tutorialFinal;
    public GameObject gameManager;
    public GameObject npc1Block;
    public GameObject npc2Block;
    public GameObject npc3Block;
    public GameObject playerCapitalBlock;

    public Node playerBase;
    public Node enemyBase;
    public Node npc1;
    public Node npc2;
    public Node npc3;

    public Button upgradeButton;
    public Button speedButton;
    public Button menuButton;

    void Start()
    {
        upgradeButton.interactable = false;
        speedButton.interactable = false;
        menuButton.interactable = false;

        Physics.IgnoreLayerCollision(3 /*troop layer*/, 6 /*tutorial block layer*/); //Makes troops ignore invisible tutorial blocks

        StartCoroutine(DelayedSkip());
    }

    void FirstClicked()
    {
        if(gameManager.GetComponent<InputManager>().node == playerBase)
        {
            npc2Block.SetActive(false);
            firstHintItems.SetActive(false);
            //StopCoroutine(Ping(firstHintItems));
            secondHintItems.SetActive(true);
            StartCoroutine(Ping(secondHintItems));
        }  
    }

    void SecondClicked()
    {
        if (npc2.faction == Faction.PLAYER)
        {
            npc2Block.SetActive(true);
            secondHintItems.SetActive(false);
            //StopCoroutine(Ping(secondHintItems));
            thirdHintItems.SetActive(true);
            StartCoroutine(Ping(thirdHintItems));
        }
    }

    void FifthClicked()
    {
        if (gameManager.GetComponent<InputManager>().bttn == upgradeButton && gameManager.GetComponent<GameManager>().selectedNode == playerBase && gameManager.GetComponent<GameManager>().selectedNode.maxTroops == 60)
        {
            npc2Block.SetActive(false);
            playerCapitalBlock.SetActive(true);
            fifthHintItems.SetActive(false);
            //StopCoroutine(Ping(fifthHintItems));
            empyClickHint.SetActive(true);

            StartCoroutine(Ping(empyClickHint));
        }
    }

    void EmptyClicked()
    {
        if(gameManager.GetComponent<GameManager>().selectedNode == null)
        {
            empyClickHint.SetActive(false);
            sixthHintItems.SetActive(true);

            gameManager.GetComponent<GameManager>().playerCoin = 60;

            StartCoroutine(Ping(sixthHintItems));
        }
    }

    void SixthClicked() //bunu fifthle ayni yap
    {
        if (gameManager.GetComponent<GameManager>().selectedNode != null && gameManager.GetComponent<GameManager>().selectedNode.maxTroops == 50 /*gameManager.GetComponent<InputManager>().bttn == upgradeButton && gameManager.GetComponent<GameManager>().selectedNode == npc2 && */)
        {
            sixthHintItems.SetActive(false);
            //StopCoroutine(Ping(sixthHintItems));
            seventhHintItems.SetActive(true);
            StartCoroutine(Ping(seventhHintItems));
        }
    }

    void CheckOwnership(Node node)
    {
        if (node == npc3 && node.faction == Faction.PLAYER) //UNITY BURDA PATLIYOR
        {
            ninthHintItems.SetActive(false);
            ////StopCoroutine(Ping(ninthHintItems));
            tenthHintItems.SetActive(true);

            StartCoroutine(Ping(tenthHintItems));
        }

        else if (node == enemyBase && node.faction == Faction.PLAYER)
        {
            tenthHintItems.SetActive(false);
            tutorialFinal.SetActive(true);

        }
    }

    public IEnumerator DelayedSkip()
    {
        yield return new WaitForSecondsRealtime(5);

        if (IntroItems.activeSelf== true)
        {
            //yield return new WaitForSecondsRealtime(5);

            IntroItems.SetActive(false);
            StopCoroutine(Ping(IntroItems));
            firstHintItems.SetActive(true);

            StartCoroutine(Ping(firstHintItems));
            //StopCoroutine(DelayedSkip());
        }

        else if( thirdHintItems.activeSelf == true)
        {
            //yield return new WaitForSecondsRealtime(5);

            thirdHintItems.SetActive(false);
            //StopCoroutine(Ping(thirdHintItems));
            fourthHintItems.SetActive(true);

            StartCoroutine(Ping(fourthHintItems));
            //StopCoroutine(DelayedSkip());
        }

        else if (fourthHintItems.activeSelf == true)
        {
            yield return new WaitForSecondsRealtime(5);

            fourthHintItems.SetActive(false);
            //StopCoroutine(Ping(fourthHintItems));
            fifthHintItems.SetActive(true);

            gameManager.GetComponent<GameManager>().playerCoin = 20;
            

            StartCoroutine(Ping(fifthHintItems));
            //StopCoroutine(DelayedSkip());
        }

        else if(seventhHintItems.activeSelf == true)
        {
            yield return new WaitForSecondsRealtime(5);

            seventhHintItems.SetActive(false);
            //StopCoroutine(Ping(seventhHintItems));
            eighthHintItems.SetActive(true);

            StartCoroutine(Ping(eighthHintItems));
            //StopCoroutine(DelayedSkip());
        }

        else if (eighthHintItems.activeSelf == true)
        {
            yield return new WaitForSecondsRealtime(8);

            playerCapitalBlock.SetActive(false);
            npc1Block.SetActive(false);
            npc2Block.SetActive(false);
            npc3Block.SetActive(false);
            eighthHintItems.SetActive(false);
            //StopCoroutine(Ping(eighthHintItems));
            ninthHintItems.SetActive(true);

            StartCoroutine(Ping(ninthHintItems));
            //StopCoroutine(DelayedSkip());
        }
    }

    public IEnumerator Ping(GameObject tempObj)
    {
        while(tempObj.activeInHierarchy == true)
        {
            if (tempObj == firstHintItems || tempObj == secondHintItems || tempObj == fourthHintItems || tempObj == empyClickHint || tempObj == seventhHintItems || tempObj == ninthHintItems || tempObj == tenthHintItems)
            {
                tempObj.transform.GetChild(1).gameObject.SetActive(false);
                yield return new WaitForSeconds(0.25f);
                tempObj.transform.GetChild(1).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);

                if (tempObj == firstHintItems)
                {
                    FirstClicked(); 
                }

                else if (tempObj == secondHintItems)
                { 
                    SecondClicked(); 
                }

                else if (tempObj == fourthHintItems) 
                { StartCoroutine(DelayedSkip()); }

                else if (tempObj == empyClickHint)
                {
                    EmptyClicked(); 
                }

                else if (tempObj == seventhHintItems)
                {
                    StartCoroutine(DelayedSkip()); 
                }

                else if (tempObj == ninthHintItems)
                { CheckOwnership(npc3); }

                else { CheckOwnership(enemyBase); }

            }

            else if(tempObj == thirdHintItems)
            {
                tempObj.transform.GetChild(1).gameObject.SetActive(false);
                tempObj.transform.GetChild(2).gameObject.SetActive(false);
                yield return new WaitForSeconds(0.25f);
                tempObj.transform.GetChild(1).gameObject.SetActive(true);
                tempObj.transform.GetChild(2).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);

                StartCoroutine(DelayedSkip());

            }

            else if(tempObj == fifthHintItems || tempObj == sixthHintItems)
            {
                yield return new WaitForSecondsRealtime(5);
                tempObj.transform.GetChild(0).gameObject.SetActive(false);
                tempObj.transform.GetChild(1).gameObject.SetActive(true);

                tempObj.transform.GetChild(2).gameObject.SetActive(false);
                yield return new WaitForSeconds(0.25f);
                tempObj.transform.GetChild(2).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);

                if (tempObj == fifthHintItems)
                { 
                    upgradeButton.interactable = true; 
                    FifthClicked(); 
                }

                else if (tempObj == sixthHintItems)
                { SixthClicked(); }
            }

            else if(tempObj == eighthHintItems)
            {
                speedButton.interactable = true;

                if (gameManager.GetComponent<InputManager>().bttn != speedButton)
                {
                    tempObj.transform.GetChild(2).gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    tempObj.transform.GetChild(2).gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.25f);
                }

                else if (gameManager.GetComponent<InputManager>().bttn == speedButton) //Bekleyince bir sey buraya girmeden DelayedSkip() calistiriyor. DELAYEDSKIPLERI IPTAL ET
                {
                    tempObj.transform.GetChild(0).gameObject.SetActive(false);
                    tempObj.transform.GetChild(1).gameObject.SetActive(true);
                    tempObj.transform.GetChild(2).gameObject.SetActive(false);

                    tempObj.transform.GetChild(3).gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    tempObj.transform.GetChild(3).gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.25f);

                    StartCoroutine(DelayedSkip());
                }
            }
        }
    }

    //
}
