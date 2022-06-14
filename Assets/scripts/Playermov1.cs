﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermov1 : MonoBehaviour
{
    public GameObject itsBarnyard;
    public GameObject oponentBarnyard;
    public GameObject circle_chicken;
    public GameObject circle_golden_chicken;
    private GameObject circle;
    private GameObject chicken;
    public GameObject circle_fox;
    public GameObject circle_battle_fox;
    private GameObject fox;
    public GameObject timer;
    public GameObject FoxEater;

    //LoadingSystem
    public GameObject LoadP1;
    public bool foxIsActive;//is true if the fox is inside the own barnyard

    [System.NonSerialized]
    public bool chicken_caught;
    private bool fox_caught;
    private bool battling_fox;
    private float itsBarnyard_min_x;
    private float itsBarnyard_max_x;
    private float itsBarnyard_min_z;
    private float itsBarnyard_max_z;
    private float oponentBarnyard_min_z;
    private float oponentBarnyard_max_z;
    private float oponentBarnyard_min_x;
    private float oponentBarnyard_max_x;
    private float time_fox_caught;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        circle_chicken.SetActive(false);
        circle_golden_chicken.SetActive(false);
        chicken_caught = false;
        circle_fox.SetActive(false);
        circle_battle_fox.SetActive(false);
        LoadP1.SetActive(false);
        fox_caught = false;
        battling_fox = false;
        itsBarnyard_min_x = itsBarnyard.transform.position.x - itsBarnyard.GetComponent<Barnyard>().xDepth;
        itsBarnyard_max_x = itsBarnyard.transform.position.x + itsBarnyard.GetComponent<Barnyard>().xDepth;
        itsBarnyard_min_z = itsBarnyard.transform.position.z - itsBarnyard.GetComponent<Barnyard>().zDepth;
        itsBarnyard_max_z = itsBarnyard.transform.position.z + itsBarnyard.GetComponent<Barnyard>().zDepth;
        oponentBarnyard_min_x = oponentBarnyard.transform.position.x - oponentBarnyard.GetComponent<Barnyard>().xDepth;
        oponentBarnyard_max_x = oponentBarnyard.transform.position.x + oponentBarnyard.GetComponent<Barnyard>().xDepth;
        oponentBarnyard_min_z = oponentBarnyard.transform.position.z - oponentBarnyard.GetComponent<Barnyard>().zDepth;
        oponentBarnyard_max_z = oponentBarnyard.transform.position.z + oponentBarnyard.GetComponent<Barnyard>().zDepth;
        LoadTimerSystem.Instance.InitTimers();
        foxIsActive = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < itsBarnyard_max_x)
        {
            //Release chicken
            if (chicken_caught == true)
            {
                chicken.SetActive(true);
                circle.SetActive(false);
                Vector3 randomPosition = new Vector3(Random.Range(itsBarnyard_min_x+5, itsBarnyard_max_x-5), 0.5f, Random.Range(itsBarnyard_min_z+5, itsBarnyard_max_z-5));
                chicken.transform.position = randomPosition;
                chicken_caught = false;
                rb = chicken.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                if(circle == circle_chicken)
                    PointSystem.Instance.Add1PointP1();
                if (circle == circle_golden_chicken) {
                    PointSystem.Instance.Add3PointP1();

                }
                SoundManager.Instance.PlayChickenCaugthRelease();



            }

        }

        if ((fox_caught == true) && (transform.position.x > oponentBarnyard_min_x))
        {
            fox.SetActive(true);
            circle_fox.SetActive(false);
            Vector3 randomPosition = new Vector3(Random.Range(oponentBarnyard_min_x + 5, oponentBarnyard_max_x - 5), 0.5f, Random.Range(oponentBarnyard_min_z+5, oponentBarnyard_max_z-5));
            fox.transform.position = randomPosition;
            fox_caught = false;
            SoundManager.Instance.PlayFoxCaughtRelease();
            FoxEater.GetComponent<ChickenDeleater>().foxInBarnyard2 = true;
        }
        if (foxIsActive == true)
        {
            Debug.Log("fox is active");
            fox.SetActive(true);
            circle_battle_fox.SetActive(false);
            Vector3 randomPosition = new Vector3(Random.Range(itsBarnyard_min_x + 5, itsBarnyard_max_x - 5), 0.5f, Random.Range(oponentBarnyard_min_z + 5, oponentBarnyard_max_z - 5));
            fox.transform.position = randomPosition;
            SoundManager.Instance.PlayFoxCaughtRelease();
            foxIsActive = false;
        }
        //if battling fox and inside barnyard
        if (battling_fox == true){
            if (transform.position.x < itsBarnyard_max_x)
            {
                foxIsActive = false;

                LoadTimerSystem.Instance.LoadTimerP1Subtract();
                if (LoadTimerSystem.Instance.time <= 0)
                {
                    circle_battle_fox.SetActive(false);
                    LoadP1.SetActive(false);
                    battling_fox = false;

                    LoadTimerSystem.Instance.InitTimers();
                    Destroy(fox);
                    SoundManager.Instance.PlayFoxKill();
                    FoxEater.GetComponent<ChickenDeleater>().foxInGame = false;
                    FoxEater.GetComponent<ChickenDeleater>().foxInBarnyard1 = false;
                }
            }
            else
            {
                LoadTimerSystem.Instance.LoadTimerP1Add();
                if (LoadTimerSystem.Instance.time >= LoadTimerSystem.Instance.maxDuration)
                {
                    LoadP1.SetActive(false);
                    battling_fox = false;
                    foxIsActive = true;
                    circle_battle_fox.SetActive(false);
                    fox.SetActive(true);
                }

            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((transform.position.x > itsBarnyard_max_x+5) && (transform.position.x < oponentBarnyard_min_x-5))
        {
            if ((((other.gameObject.CompareTag("Chicken")) || (other.gameObject.CompareTag("GoldenChicken"))) && ((chicken_caught == false) && (fox_caught == false))) && (battling_fox == false))
            {
                Debug.Log("collisionnnn with chicken");
                chicken = other.gameObject;
                other.gameObject.SetActive(false);
                if (other.gameObject.CompareTag("Chicken"))
                {
                    circle = circle_chicken;
                }
                else
                {
                    circle = circle_golden_chicken;
                }
                circle.SetActive(true);
                chicken_caught = true;
                SoundManager.Instance.PlayChickenCaugthRelease();

            }
            if ((other.gameObject.CompareTag("Fox"))&& (chicken_caught == false) && (fox_caught == false))
            {
                Debug.Log("collisionnnn with fox");
                fox = other.gameObject;
                other.gameObject.SetActive(false);
                circle_fox.SetActive(true);
                fox_caught = true;
            }
        }
        else if (transform.position.x < itsBarnyard_max_x)
        {
            
            if (other.gameObject.CompareTag("Fox"))
            {
                Debug.Log("Battling fox");
                fox = other.gameObject;
                other.gameObject.SetActive(false);
                LoadP1.SetActive(true);
                circle_battle_fox.SetActive(true);
                battling_fox = true;
                time_fox_caught = timer.GetComponent<Timer>().timeRemaining;
                SoundManager.Instance.PlayFoxFigth();
            }
        }
    }

}
