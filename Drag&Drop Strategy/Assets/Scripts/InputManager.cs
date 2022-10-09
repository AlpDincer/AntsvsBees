using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private Color originColor;
    public Node node;
    public Button bttn;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                node = hit.collider.GetComponent<Node>();
                bttn = hit.collider.GetComponent<Button>();
                //Debug.Log(bttn);
                if (node != null || bttn != null)
                {
                    GameManager.instance.HandleNodes(node,bttn);   
                    return;
                }

                else { GameManager.instance.HandleNodes(null, null); }
                
            }

        }
    }
}
