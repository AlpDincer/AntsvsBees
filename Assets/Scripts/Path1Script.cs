using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1Script : MonoBehaviour
{
    Path[] PathNode;
    private Vector3 startPosition;
    public GameObject tempTroop;
    public float moveSpeed;
    float timer;
    static Vector3 CurrentPosHolder;
    int CurrentNode;

    // Start is called before the first frame update
    void Start()
    {
        PathNode = GetComponentsInChildren<Path>();
        CheckNode();
    }

    void CheckNode()
    {
        if(tempTroop != null)
        {
            startPosition = tempTroop.transform.position;
        }
        
        timer = 0;
        CurrentPosHolder = PathNode[CurrentNode].transform.position; //burda bir sorun var birden cok troopla alakali olabilir
    }

    // Update is called once per frame
    void Update()
    {
        if (tempTroop != null)
        {
            timer += Time.deltaTime * moveSpeed;
            if (tempTroop.transform.position != CurrentPosHolder)
            {
                tempTroop.transform.position = Vector3.Lerp(startPosition, CurrentPosHolder, timer);
            }

            else
            {
                if (CurrentNode < PathNode.Length - 1)
                {
                    CurrentNode++;
                    CheckNode();
                }
            }
        }
    }

    public void FollowPath(GameObject gameObject)
    {
        tempTroop = gameObject;
    }
}
