using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitP2 : MonoBehaviour
{
    [System.NonSerialized]
    public bool is2Exiting = false;
    
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
        if(target.tag == "Player2")
        {
            is2Exiting = true;
            Debug.Log("Player 2 Exiting");
        }
    }

    void OnTriggerExit(Collider target)
    {
        if(target.tag == "Player2")
        {
            is2Exiting = false;
            Debug.Log("Player 2 No Longer Exiting");
        }
    }
}
