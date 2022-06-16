using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net.Sockets;
using System.Net;

public class PlayerDetection : MonoBehaviour
{
    public bool live;
    public float manualSpeed;
    public float[] positions;
    DeepTrackingReceiver tracking;

    int currentManualMarkId = 0;
    Vector2 worldLimits = new Vector2(1, 1);

    // Start is called before the first frame update
    void Start()
    {
        positions = new float[8];

        if (live)
        {            
            tracking = new DeepTrackingReceiver();
            tracking.init();
            tracking.listen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (live)
        {
            positions = tracking.positions;
        }
        else {

            if (Input.GetKeyUp(KeyCode.R)) currentManualMarkId = 0;
            if (Input.GetKeyUp(KeyCode.G)) currentManualMarkId = 1;
            if (Input.GetKeyUp(KeyCode.B)) currentManualMarkId = 2;
            if (Input.GetKeyUp(KeyCode.Y)) currentManualMarkId = 3;

            //Manual movements
            MapMovement(currentManualMarkId);
        }
    }

    private void OnApplicationQuit()
    {
        if (live) tracking.close();
    }


    void MapMovement(int markerId) {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            positions[4] += manualSpeed;
            if (positions[4] > worldLimits.x) positions[4] = worldLimits.x;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            positions[4] -= manualSpeed;
            if (positions[4] < 0) positions[4] = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            positions[5] += manualSpeed;
            if (positions[5] > worldLimits.y) positions[5] = worldLimits.y;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            positions[5] -= manualSpeed;
            if (positions[5] < 0) positions[5] = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            positions[0] += manualSpeed;
            if (positions[0] > worldLimits.x) positions[0] = worldLimits.x;
        }

        if (Input.GetKey(KeyCode.D))
        {
            positions[0] -= manualSpeed;
            if (positions[0] < 0) positions[0] = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            positions[1] += manualSpeed;
            if (positions[1] > worldLimits.y) positions[1] = worldLimits.y;
        }

        if (Input.GetKey(KeyCode.S))
        {
            positions[1] -= manualSpeed;
            if (positions[1] < 0) positions[1] = 0;
        }

    }
}
