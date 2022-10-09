using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{

    private GameObject audioManager;
    private AudioSource soundEffect;
    private Quaternion _lookRotation;
    private Vector3 _direction;

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

        if (target.transform.position.y > transform.position.y-0.5f) //karincalar yerden gittigi icin, yuksek bir yere giderken Y eksenini arttir
        {

            transform.position += new Vector3(0, 0.2f, 0);
        }

        _direction = (target.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 1);

    }

    void OnTriggerEnter(Collider col) //Bunun yerine direk sayiyi dusebilirsin
    {
        Node n = col.GetComponentInParent<Node>();
        if(n == target)
        {
            n.HandleIncomingTroop(faction);
            Destroy(gameObject);
            
            if (faction == Faction.PLAYER)
            { soundEffect.Play(); }
        }
    }
}
