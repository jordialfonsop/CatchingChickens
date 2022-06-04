using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitP1 : MonoBehaviour
{
    [System.NonSerialized]
    public bool is1Exiting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player1")
        {
            is1Exiting = true;
            Debug.Log("Player 1 Exiting");
        }
    }

    void OnTriggerExit(Collider target)
    {
        if(target.tag == "Player1")
        {
            is1Exiting = false;
            Debug.Log("Player 1 No Longer Exiting");
        }
    }
}
