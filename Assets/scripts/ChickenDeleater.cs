using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeleater : MonoBehaviour
{
    public bool foxInBarnyard1;
    public bool foxInBarnyard2;
    public bool foxInGame;
    public GameObject timer;
    /*    private GameObject[] chickens;
        private GameObject[] goldenChicken;
        private List<GameObject> chickensInBarnyard1 = new List<GameObject>();
        private List<GameObject> chickensInBarnyard2 = new List<GameObject>();
        private List<GameObject> chickensNotInBarnyard = new List<GameObject>();
        private List<GameObject> Foxes = new List<GameObject>();
        public GameObject Barnyard1;
        public GameObject Barnyard2;
        private float Barnyard1_min_x;
        private float Barnyard1_max_x;
        private float Barnyard2_min_x;
        private float Barnyard2_max_x;
        public bool foxInBarnyard1;
        public bool foxInBarnyard2;
        public bool foxInGame;
        public GameObject timer;
        private float timeLastEaten;
        float minx = 25;
        float maxx = 75;

        bool is_free(GameObject go)
        {
            return (go.transform.position.x > minx) && (go.transform.position.x < maxx);
        }
        // Start is called before the first frame update
        void Start()
        {
            timeLastEaten = 240;
            foxInBarnyard1 = false;
            foxInBarnyard2 = false;
            foxInGame = false;

            Barnyard1_min_x = Barnyard1.transform.position.x - Barnyard1.GetComponent<Barnyard>().xDepth;
            Barnyard1_max_x = Barnyard1.transform.position.x + Barnyard1.GetComponent<Barnyard>().xDepth;
            Barnyard2_min_x = Barnyard2.transform.position.x - Barnyard2.GetComponent<Barnyard>().xDepth;
            Barnyard2_max_x = Barnyard2.transform.position.x + Barnyard2.GetComponent<Barnyard>().xDepth;
        }

        // Update is called once per frame
        void Update()
        {
            GameObject best_chicken = null;
            chickens = GameObject.FindGameObjectsWithTag("Chicken");
            goldenChicken = GameObject.FindGameObjectsWithTag("GoldenChicken");
            if (foxInBarnyard1)
            {
                chickensInBarnyard1 = new List<GameObject>();
                Debug.Log("Fox in Barnyard1");
                for (int i = 0; i < chickens.Length; i++)
                {
                    if (chickens[i].transform.position.x < Barnyard1_max_x)
                    {
                        chickensInBarnyard1.Add(chickens[i]);
                    }
                }
                if ((goldenChicken.Length != 0)&&(goldenChicken[0].transform.position.x < Barnyard1_max_x))
                {
                    chickensInBarnyard1.Add(goldenChicken[0]);
                }
                if (chickensInBarnyard1.Count > 0)
                {
                    int randomChicken = 0;
                    if (chickensInBarnyard1.Count > 1)
                    {
                        randomChicken = Random.Range(0, chickensInBarnyard1.Count);
                    }
                    best_chicken = chickensInBarnyard1[randomChicken];
                }
                else
                {
                    Debug.Log("No chickens in Barnyard1");
                }
            }
            else if (foxInBarnyard2)
            {
                chickensInBarnyard2 = new List<GameObject>();
                Debug.Log("Fox in Barnyard2");
                for (int i = 0; i < chickens.Length; i++)
                {
                    if (chickens[i].transform.position.x > Barnyard2_min_x)
                    {
                        chickensInBarnyard2.Add(chickens[i]);
                    }
                }
                if ((goldenChicken.Length != 0) && (goldenChicken[0].transform.position.x > Barnyard2_min_x))
                {
                    Debug.Log("goldeeen");
                    chickensInBarnyard2.Add(goldenChicken[0]);
                }
                if (chickensInBarnyard2.Count > 0)
                {
                    int randomChicken = 0;
                    if (chickensInBarnyard2.Count > 1)
                    {
                        randomChicken = Random.Range(0, chickensInBarnyard2.Count);
                    }
                    best_chicken = chickensInBarnyard2[randomChicken];
                }
                else
                {
                    Debug.Log("No chickens in Barnyard2");
                }

            }
            else if (GameObject.FindGameObjectsWithTag("Fox").Length != 0)
            {
                Debug.Log("Fox in area");
                chickensNotInBarnyard = new List<GameObject>();

                for (int i = 0; i < chickens.Length; i++)
                {
                    if ((chickens[i].transform.position.x > Barnyard1_max_x) && (chickens[i].transform.position.x < Barnyard2_min_x))
                    {
                        chickensNotInBarnyard.Add(chickens[i]);
                    }
                }
                if ((goldenChicken.Length != 0) && ((goldenChicken[0].transform.position.x > Barnyard1_max_x) && (goldenChicken[0].transform.position.x < Barnyard2_min_x)))
                {
                    chickensNotInBarnyard.Add(goldenChicken[0]);
                }
                if (chickensNotInBarnyard.Count > 0)
                {
                        GameObject Fox = GameObject.FindGameObjectWithTag("Fox");
                        float x = Fox.transform.position.x;
                        float z = Fox.transform.position.z;
                        float best_distance = 1000;
                        best_chicken = null;
                        for (int i = 0; i < chickensNotInBarnyard.Count; i++)
                        {
                            GameObject myChicken = chickensNotInBarnyard[i];
                            if (is_free(myChicken))
                            {
                                float chickenx = myChicken.transform.position.x;
                                float chickenz = myChicken.transform.position.z;
                                float d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                                if (d < best_distance)
                                {
                                    best_chicken = myChicken;
                                    best_distance = d;
                                }
                            }
                        }
                        if(goldenChicken.Length != 0){
                            float chickenx = goldenChicken[0].transform.position.x;
                            float chickenz = goldenChicken[0].transform.position.z;
                            float d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                            if (d < best_distance)
                            {
                                best_chicken = goldenChicken[0];
                                best_distance = d;
                            }
                        }
                }
                else
                {
                    Debug.Log("No chickens in Playarea");
                }                 

            }
            if ((foxInGame)&&(timeLastEaten - 10 > timer.GetComponent<Timer>().timeRemaining))
            {
                if((timer.GetComponent<Timer>().timeRemaining > 0)&& (best_chicken != null)){
                    if (best_chicken.CompareTag("Chicken"))
                    {
                        if(foxInBarnyard1){
                            PointSystem.Instance.ErasePointP1();
                        }else if(foxInBarnyard2){
                            PointSystem.Instance.ErasePointP2();
                        }
                    }
                    else
                    {
                        if(foxInBarnyard1){
                            PointSystem.Instance.Erase3PointsP1();
                        }else if(foxInBarnyard2){
                            PointSystem.Instance.Erase3PointsP2();
                        }
                    }

                    Destroy(best_chicken);
                    SoundManager.Instance.PlayChickenKilled();
                    timeLastEaten = timer.GetComponent<Timer>().timeRemaining;

                }

            }
        }*/
}
