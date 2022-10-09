using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    private GameObject audioManager;
    private AudioSource soundEffect;

    public Faction faction;

    Node target;

    public float speed = 5;

    void Start()
    {
        audioManager = GameObject.Find("--AudioManager--");

        if (audioManager != null)
        {
            soundEffect = audioManager.transform.GetChild(1).GetComponent<AudioSource>();
            soundEffect.volume = 0.1f;
        }
    }

    public void SetTroop(Faction baseFaction, Node targetNode, Material mat)
    {
        faction = baseFaction;
        target = targetNode;
        GetComponent<MeshRenderer>().material = mat;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col) //Bunun yerine direk sayiyi dusebilirsin
    {
        Node n = col.GetComponent<Node>();
        if(n == target)
        {
            n.HandleIncomingTroop(faction);
            Destroy(gameObject);
            
            if (faction == Faction.PLAYER)
            { soundEffect.Play(); }
        }
    }
}
