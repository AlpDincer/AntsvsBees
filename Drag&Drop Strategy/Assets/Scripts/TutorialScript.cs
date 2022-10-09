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
    public GameObject sixthHintItems;
    public GameObject seventhHintItems;
    public GameObject eighthHintItems;
    public GameObject ninthHintItems;
    public GameObject tenthHintItems;
    public GameObject gameManager;
    public GameObject tutorialFilter;

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
        StartCoroutine(DelayedSkip());
    }

    void FirstClicked()
    {
        if(gameManager.GetComponent<InputManager>().node == playerBase)
        {
            firstHintItems.SetActive(false);
            //StopCoroutine(Ping(firstHintItems));
            secondHintItems.SetActive(true);
            StartCoroutine(Ping(secondHintItems));
        }  
    }

    void SecondClicked()
    {
        if (gameManager.GetComponent<InputManager>().node == npc2)
        {
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
            fifthHintItems.SetActive(false);
            //StopCoroutine(Ping(fifthHintItems));
            sixthHintItems.SetActive(true);

            gameManager.GetComponent<GameManager>().playerCoin = 60;

            StartCoroutine(Ping(sixthHintItems));
        }
    }

    void SixthClicked()
    {
        if (gameManager.GetComponent<InputManager>().bttn == upgradeButton && gameManager.GetComponent<GameManager>().selectedNode == npc2 && gameManager.GetComponent<GameManager>().selectedNode.maxTroops == 50)
        {
            sixthHintItems.SetActive(false);
            //StopCoroutine(Ping(sixthHintItems));
            seventhHintItems.SetActive(true);
            StartCoroutine(Ping(seventhHintItems));
        }
    }

    void CheckOwnership()
    {
        //if(npc3.faction == Faction.PLAYER)
        //{
        //    ninthHintItems.SetActive(false);
        //    //StopCoroutine(Ping(ninthHintItems));
        //    tenthHintItems.SetActive(true);

        //    StartCoroutine(Ping(tenthHintItems));

        //}
    }

    public IEnumerator DelayedSkip()
    {
        yield return new WaitForSecondsRealtime(5);

        if (IntroItems.activeInHierarchy == true)
        {
            //yield return new WaitForSecondsRealtime(5);

            IntroItems.SetActive(false);
            firstHintItems.SetActive(true);

            StartCoroutine(Ping(firstHintItems));
        }

        else if( thirdHintItems.activeInHierarchy == true)
        {
            //yield return new WaitForSecondsRealtime(5);

            thirdHintItems.SetActive(false);
            //StopCoroutine(Ping(thirdHintItems));
            fourthHintItems.SetActive(true);

            StartCoroutine(Ping(fourthHintItems));
        }

        else if (fourthHintItems.activeInHierarchy == true)
        {
            yield return new WaitForSecondsRealtime(5);

            fourthHintItems.SetActive(false);
            //StopCoroutine(Ping(fourthHintItems));
            fifthHintItems.SetActive(true);

            gameManager.GetComponent<GameManager>().playerCoin = 20;
            

            StartCoroutine(Ping(fifthHintItems));
        }

        else if(seventhHintItems.activeInHierarchy == true)
        {
            yield return new WaitForSecondsRealtime(5);

            seventhHintItems.SetActive(false);
            //StopCoroutine(Ping(seventhHintItems));
            eighthHintItems.SetActive(true);

            StartCoroutine(Ping(eighthHintItems));
        }

        else if (eighthHintItems.activeInHierarchy == true)
        {
            yield return new WaitForSecondsRealtime(8);

            eighthHintItems.SetActive(false);
            //StopCoroutine(Ping(eighthHintItems));
            ninthHintItems.SetActive(true);

            StartCoroutine(Ping(ninthHintItems));
        }
    }

    public IEnumerator Ping(GameObject tempObj)
    {
        while(tempObj.activeInHierarchy == true)
        {
            if (tempObj == firstHintItems || tempObj == secondHintItems || tempObj == fourthHintItems || tempObj == seventhHintItems || tempObj == ninthHintItems)
            {
                tempObj.transform.GetChild(1).gameObject.SetActive(false);
                yield return new WaitForSeconds(0.25f);
                tempObj.transform.GetChild(1).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);

                if (tempObj == firstHintItems)
                {/* StopCoroutine(DelayedSkip());*/ FirstClicked(); }

                else if (tempObj == secondHintItems)
                { /*StopCoroutine(DelayedSkip());*/ SecondClicked(); }

                else if (tempObj == fourthHintItems) 
                { /*StopCoroutine(DelayedSkip());*/ StartCoroutine(DelayedSkip()); }

                else if (tempObj == seventhHintItems)
                {/* StopCoroutine(DelayedSkip());*/ StartCoroutine(DelayedSkip()); }

                else if (tempObj == ninthHintItems)
                { /*StopCoroutine(DelayedSkip());*/ CheckOwnership(); }

                

            }

            else if(tempObj == thirdHintItems)
            {
                tempObj.transform.GetChild(1).gameObject.SetActive(false);
                tempObj.transform.GetChild(2).gameObject.SetActive(false);
                yield return new WaitForSeconds(0.25f);
                tempObj.transform.GetChild(1).gameObject.SetActive(true);
                tempObj.transform.GetChild(2).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);

                //StopCoroutine(DelayedSkip());
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
                { /*StopCoroutine(DelayedSkip());*/ FifthClicked(); }

                else if (tempObj == sixthHintItems)
                { /*StopCoroutine(DelayedSkip());*/ SixthClicked(); }
            }

            else if(tempObj == eighthHintItems)
            {
                if (gameManager.GetComponent<InputManager>().bttn != speedButton)
                {
                    tempObj.transform.GetChild(2).gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    tempObj.transform.GetChild(2).gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.25f);
                }

                else if (gameManager.GetComponent<InputManager>().bttn == speedButton)
                {
                    tempObj.transform.GetChild(0).gameObject.SetActive(false);
                    tempObj.transform.GetChild(1).gameObject.SetActive(true);
                    tempObj.transform.GetChild(2).gameObject.SetActive(false);

                    tempObj.transform.GetChild(3).gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    tempObj.transform.GetChild(3).gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.25f);

                    //StopCoroutine(DelayedSkip());
                    StartCoroutine(DelayedSkip());
                }
            }
        }
    }

    //
}
