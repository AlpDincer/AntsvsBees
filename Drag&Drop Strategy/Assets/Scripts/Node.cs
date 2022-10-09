using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public enum Faction
{
    NEUTRAL,
    ENEMY,
    PLAYER
}

public class Node : MonoBehaviour
{
    public int maxTroops;
    int startingTroops = 10;
    public int currentTroops;
    private int enemyCoin;
    private float upgradeCount;
    private Color tempColor;

    public Material[] materials; //0=Neutral|1=Player|2=Enemy

    public TMP_Text count;

    public enum NodeType
    {
        Capital_City,
        Small_City,
        Fort_City
    }

    public Faction faction;
    public NodeType type;

    public GameObject troopPrefab;
    public GameObject upgradePrefab;
    public GameObject fortifyText;

    public List<GameObject> connectedNodes;


    // Start is called before the first frame update
    void Start()
    {
        upgradeCount = 0.0f;

        //fortifyText.GetComponent<CanvasRenderer>().SetAlpha(0);
        tempColor = upgradePrefab.GetComponent<SpriteRenderer>().color;

        if (type == NodeType.Capital_City){maxTroops = 50; startingTroops = 15; }

        else if (type == NodeType.Fort_City) { maxTroops = 40; startingTroops = 30; }

        else { maxTroops = 20; }

        currentTroops = startingTroops;
        UpdateCount();

        StartCoroutine(Train());
        //StartCoroutine(PlayEnemy());

        if (faction == Faction.ENEMY)
        {
            PlayEnemyNodes(this);
        }
    }

    public void TrainTroops() //GameManager'dan cagir
    {
        if (faction != Faction.NEUTRAL && type != NodeType.Fort_City)
        {
            if (currentTroops >= maxTroops)
            {
                currentTroops = maxTroops;
            }
            else { currentTroops++; }
        }

        else if(faction != Faction.NEUTRAL && type == NodeType.Fort_City)
        {
            if (currentTroops >= maxTroops)
            {
                currentTroops = maxTroops;
            }
            else { currentTroops+= 2; }
        }
        UpdateCount();

    }

    IEnumerator DestroyTroop(Faction troopFaction)
    {
        currentTroops--;
        if(currentTroops <= 0)
        {
            FactionHandler(troopFaction);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Train() //traini manager'a alinca bunun adini PlayEnemy yap
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            TrainTroops();
            if (faction == Faction.ENEMY)
            {
                PlayEnemyNodes(this);
            }
        }
    }

    void UpdateCount()
    {
        count.text = currentTroops+ "|"+ maxTroops;
    }

    IEnumerator SendAllTroops(Node targetNode)
    {
        int troopsToSend = currentTroops;
        for (int i = 0; i < troopsToSend; i++)
        {
            if (targetNode.faction == Faction.PLAYER && targetNode.currentTroops < targetNode.maxTroops - 1)
            {
                if (currentTroops > 1)
                {
                    GameObject newTroop = Instantiate(troopPrefab, transform.position, Quaternion.identity);

                    newTroop.GetComponent<Troop>().SetTroop(faction, targetNode, GetComponent<MeshRenderer>().material); //Move troops
                    yield return new WaitForSeconds(0.1f);

                    //check troops

                    currentTroops--;
                    UpdateCount();
                }
            }

            else if (targetNode.faction != Faction.PLAYER)
            {
                if (currentTroops > 1)
                {
                    GameObject newTroop = Instantiate(troopPrefab, transform.position, Quaternion.identity);

                    newTroop.GetComponent<Troop>().SetTroop(faction, targetNode, GetComponent<MeshRenderer>().material); //Move troops
                    yield return new WaitForSeconds(0.1f);

                    currentTroops--;
                    UpdateCount();
                }
            }

            else { Debug.Log("Node is full"); }


        }
    }

    IEnumerator SendEnemyTroops(Node targetNode) //Bot Kodu
    {
        int troopsToSend = currentTroops;
        for (int i = 0; i < troopsToSend; i++)
        {
            if (targetNode.faction == Faction.ENEMY && targetNode.currentTroops < targetNode.maxTroops - 1)
            {
                if (currentTroops > 1)
                {
                    GameObject newTroop = Instantiate(troopPrefab, transform.position, Quaternion.identity);

                    newTroop.GetComponent<Troop>().SetTroop(faction, targetNode, GetComponent<MeshRenderer>().material); //Move troops
                    yield return new WaitForSeconds(0.1f);

                    //check troops

                    currentTroops--;
                    UpdateCount();
                }
            }

            else if (targetNode.faction != Faction.ENEMY)
            {
                int connectedTroops = 0;
                for(int j = 0; j < connectedNodes.Count; j++) { connectedTroops += connectedNodes[j].GetComponent<Node>().currentTroops; } //suralara bak
                if (currentTroops > 1 && connectedTroops > targetNode.currentTroops)
                {
                    GameObject newTroop = Instantiate(troopPrefab, transform.position, Quaternion.identity);

                    newTroop.GetComponent<Troop>().SetTroop(faction, targetNode, GetComponent<MeshRenderer>().material); //Move troops
                    yield return new WaitForSeconds(0.1f);

                    currentTroops--;
                    UpdateCount();
                }
            }

        }
    }

    public void PlayEnemyNodes(Node enemyNode)
    {      

        if(GameObject.Find("--GameManager--") != null)
        {
            enemyCoin = GameObject.Find("--GameManager--").GetComponent<GameManager>().enemyCoin;
        }

        else { enemyCoin = 0; }

        if(enemyCoin >=20) {UpgradeHandler(this) /*enemyNode.maxTroops += 10*/ /*bunun yerine fonksiyon cagir ve goruntusunu degistir*/; enemyCoin -= 20; GameObject.Find("--GameManager--").GetComponent<GameManager>().enemyCoin = enemyCoin; }

        for (int i = 0; i < connectedNodes.Count; i++)
        {
                //Debug.Log("In For");
            if (connectedNodes[i].GetComponent<Node>().faction == Faction.NEUTRAL)
            {
                //Debug.Log("In If");
                StartCoroutine(SendEnemyTroops(connectedNodes[i].GetComponent<Node>()));
                i = connectedNodes.Count;
            }

            else if (enemyNode.currentTroops > connectedNodes[i].GetComponent<Node>().currentTroops && connectedNodes[i].GetComponent<Node>().faction == Faction.PLAYER)
            {
                StartCoroutine(SendEnemyTroops(connectedNodes[i].GetComponent<Node>()));
                i = connectedNodes.Count;
            }

            else if (enemyNode.currentTroops >= enemyNode.maxTroops && connectedNodes[i].GetComponent<Node>().faction == Faction.ENEMY)
            { 
                StartCoroutine(SendEnemyTroops(connectedNodes[i].GetComponent<Node>()));
                i = connectedNodes.Count;
            }

        }
    }

    public void SendTroops(Node targetNode)
    {
        for (int i = 0; i < connectedNodes.Count; i++)
        {
            if (targetNode.gameObject == connectedNodes[i] /*&& GameObject.Find("Troop(Clone)") == null => sadece tek yerden asker gonderimine izin verir*/)
            {
                StartCoroutine(SendAllTroops(targetNode));
                   
            }
        }
        
    }

    public void HandleIncomingTroop(Faction troopFaction)
    {
        if(troopFaction == faction)
        {
            currentTroops++;
            UpdateCount();
            return;
        }

        else
        {
            StartCoroutine(DestroyTroop(troopFaction));

        }
    }

    void FactionHandler(Faction f)
    {
        faction =f;
        switch(faction)
        {
            case Faction.PLAYER:
                GetComponent<MeshRenderer>().material = materials[1];
                break;

            case Faction.ENEMY:
                GetComponent<MeshRenderer>().material = materials[2];
                break;
        }
    }

    public void UpgradeHandler(Node node)
    {
        upgradeCount++;
        float newScale;

        if (node.type != NodeType.Fort_City)
        { newScale = (1.0f + (upgradeCount / 10)); }
        else { newScale = (1.5f + (upgradeCount / 10)); }
        Vector3 tempVec = new Vector3(0.0f, 0.0f, (upgradeCount - 1) / 100);
        Vector3 newPos = node.transform.position + tempVec;

        Vector3 fortifyPos = new Vector3(0.0f, 0.0f, -2.0f);

        node.maxTroops += 10;
        if(node.type == NodeType.Small_City && upgradeCount >=3)
        {
            node.type = NodeType.Fort_City;
            fortifyText.transform.position = node.transform.position + fortifyPos;
            //fortifyText.GetComponent<TMP_Text>()..CrossFadeAlpha(255.0f, 1.0f, false);
            fortifyText.SetActive(true);

            StartCoroutine(TextDisapear(fortifyText));
        }
        
        GameObject tmpObj = Instantiate(upgradePrefab, newPos, node.transform.rotation);
        tmpObj.name = node.name + "Upgrade" + upgradeCount.ToString();
        tmpObj.transform.localScale = new Vector3(newScale, newScale, newScale);
        tmpObj.GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b - 0.08f);

        tempColor = tmpObj.GetComponent<SpriteRenderer>().color;

    }

    public IEnumerator TextDisapear(GameObject text)
    {
        yield return new WaitForSeconds(1.0f);
        //fortifyText.GetComponent<TMP_Text>().CrossFadeAlpha(0.0f, 1.0f, false);
        text.SetActive(false);
    }
}
