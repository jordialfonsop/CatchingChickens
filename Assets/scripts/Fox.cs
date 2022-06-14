using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private GameObject[] chickens;
    private GameObject golden_chicken = null;

    private static GameObject Barnyard1;
    private static GameObject Barnyard2;

    private float max_cooldown = 5;
    private float cooldown = 6;

    public float speed_fox = 15;


    private float[] b1_x = new float[2];
    private float[] b1_z = { 20, 80 };
    private float[] b2_x = new float[2];
    private float[] b2_z = { 20, 80 };
    private float[] free_x = { 20, 80 };
    private float[] free_z = { -20, 120 };
    // Params

    private void Start()
    {
        Barnyard1 = GameObject.FindGameObjectWithTag("BaryardPlayer1");
        Barnyard2 = GameObject.FindGameObjectWithTag("BaryardPlayer2");
        //b1_x[0] = Barnyard1.transform.position.x - Barnyard1.GetComponent<Barnyard>().xDepth;
        b1_x[0] = 0;
        //b1_x[1] = Barnyard1.transform.position.x + Barnyard1.GetComponent<Barnyard>().xDepth;
        b1_x[1] = 20;
        //b2_x[0] = Barnyard2.transform.position.x - Barnyard2.GetComponent<Barnyard>().xDepth;
        b2_x[0] = 80;
        //b2_x[1] = Barnyard2.transform.position.x + Barnyard2.GetComponent<Barnyard>().xDepth;
        b2_x[1] = 100;
        speed_fox = 15;
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
    }

    bool is_free(GameObject go)
    {
        float minx = free_x[0];
        float minz = free_z[0];
        float maxx = free_x[1];
        float maxz = free_z[1];

        float x = go.transform.position.x;
        float z = go.transform.position.z;

        return (x > minx) && (x < maxx) && (z > minz) && (z < maxz);
    }

    bool im_free()
    {
        float minx = free_x[0];
        float minz = free_z[0];
        float maxx = free_x[1];
        float maxz = free_z[1];

        float x = transform.position.x;
        float z = transform.position.z;

        return (x > minx) && (x < maxx) && (z > minz) && (z < maxz);
    }

    bool is_b1(GameObject go)
    {
        float minx = b1_x[0];
        float minz = b1_z[0];
        float maxx = b1_x[1];
        float maxz = b1_z[1];

        float x = go.transform.position.x;
        float z = go.transform.position.z;

        return (x > minx) && (x < maxx) && (z > minz) && (z < maxz);
    }

    bool im_b1()
    {
        float minx = b1_x[0];
        float minz = b1_z[0];
        float maxx = b1_x[1];
        float maxz = b1_z[1];

        float x = transform.position.x;
        float z = transform.position.z;

        return (x > minx) && (x < maxx) && (z > minz) && (z < maxz);
    }

    bool is_b2(GameObject go)
    {
        float minx = b2_x[0];
        float minz = b2_z[0];
        float maxx = b2_x[1];
        float maxz = b2_z[1];

        float x = go.transform.position.x;
        float z = go.transform.position.z;

        return (x > minx) && (x < maxx) && (z > minz) && (z < maxz);
    }

    bool im_b2()
    {
        float minx = b2_x[0];
        float minz = b2_z[0];
        float maxx = b2_x[1];
        float maxz = b2_z[1];

        float x = transform.position.x;
        float z = transform.position.z;

        return (x > minx) && (x < maxx) && (z > minz) && (z < maxz);
    }



    private void Update()
    {
        if (cooldown > max_cooldown)
        {
            chickens = GameObject.FindGameObjectsWithTag("Chicken");
            Debug.Log(chickens.Length);
            golden_chicken = GameObject.FindGameObjectWithTag("GoldenChicken");
            if (im_free())
            {
                float x = transform.position.x;
                float z = transform.position.z;

                float vx = 0;
                float vz = 0;

                GameObject best_chicken = null;
                float best_distance = 1000;

                for (int i = 0; i < chickens.Length; i++)
                {
                    GameObject myChicken = chickens[i];
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
                if (golden_chicken)
                {
                    if (is_free(golden_chicken))
                    {
                        float chickenx = golden_chicken.transform.position.x;
                        float chickenz = golden_chicken.transform.position.z;
                        float d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                        if (d < best_distance)
                        {
                            best_chicken = golden_chicken;
                            best_distance = d;
                        }
                    }
                }

                if (best_chicken == null)
                {
                    Debug.Log("No more free chickens");
                }
                else
                {
                    float dx = best_chicken.transform.position.x - transform.position.x;
                    float dz = best_chicken.transform.position.z - transform.position.z;

                    float d = Mathf.Sqrt(dx * dx + dz * dz);

                    Vector3 v = new Vector3(dx, 0, dz) * speed_fox / d;
                    if (d > 0.5)
                    {
                        Quaternion rotation = Quaternion.LookRotation(v, Vector3.up);
                        transform.rotation = rotation;

                    }
                    else
                    {
                        Destroy(best_chicken);
                        SoundManager.Instance.PlayChickenKilled();
                        cooldown = 0;
                    }
                    transform.position += v * Time.deltaTime;
                }

            }

            if (im_b1())
            {
                float x = transform.position.x;
                float z = transform.position.z;

                float vx = 0;
                float vz = 0;

                GameObject best_chicken = null;
                float best_distance = 1000;

                for (int i = 0; i < chickens.Length; i++)
                {
                    GameObject myChicken = chickens[i];
                    if (is_b1(myChicken))
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
                if (golden_chicken)
                {
                    if (is_b1(golden_chicken))
                    {
                        float chickenx = golden_chicken.transform.position.x;
                        float chickenz = golden_chicken.transform.position.z;
                        float d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                        if (d < best_distance)
                        {
                            best_chicken = golden_chicken;
                            best_distance = d;
                        }
                    }
                }

                if (best_chicken == null)
                {
                    Debug.Log("No more free chickens");
                }
                else
                {
                    float dx = best_chicken.transform.position.x - transform.position.x;
                    float dz = best_chicken.transform.position.z - transform.position.z;

                    float d = Mathf.Sqrt(dx * dx + dz * dz);

                    Vector3 v = new Vector3(dx, 0, dz) * speed_fox / d;
                    if (d > 0.5)
                    {
                        Quaternion rotation = Quaternion.LookRotation(v, Vector3.up);
                        transform.rotation = rotation;

                    }
                    else
                    {
                        Destroy(best_chicken);
                        SoundManager.Instance.PlayChickenKilled();
                        cooldown = 0;
                        if (best_chicken.CompareTag("Chicken"))
                        {
                            PointSystem.Instance.ErasePointP1();
                        }
                        else
                        {
                            PointSystem.Instance.Erase3PointsP1();
                        }
                    }
                    transform.position += v * Time.deltaTime;
                }

            }

            if (im_b2())
            {
                float x = transform.position.x;
                float z = transform.position.z;

                float vx = 0;
                float vz = 0;

                GameObject best_chicken = null;
                float best_distance = 1000;

                for (int i = 0; i < chickens.Length; i++)
                {
                    GameObject myChicken = chickens[i];
                    if (is_b2(myChicken))
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
                if (golden_chicken)
                {
                    if (is_b2(golden_chicken))
                    {
                        float chickenx = golden_chicken.transform.position.x;
                        float chickenz = golden_chicken.transform.position.z;
                        float d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                        if (d < best_distance)
                        {
                            best_chicken = golden_chicken;
                            best_distance = d;
                        }
                    }
                }

                if (best_chicken == null)
                {
                    Debug.Log("No more free chickens");
                }
                else
                {
                    float dx = best_chicken.transform.position.x - transform.position.x;
                    float dz = best_chicken.transform.position.z - transform.position.z;

                    float d = Mathf.Sqrt(dx * dx + dz * dz);

                    Vector3 v = new Vector3(dx, 0, dz) * speed_fox / d;
                    if (d > 0.5)
                    {
                        Quaternion rotation = Quaternion.LookRotation(v, Vector3.up);
                        transform.rotation = rotation;

                    }
                    else
                    {
                        Destroy(best_chicken);
                        SoundManager.Instance.PlayChickenKilled();
                        cooldown = 0;
                        if (best_chicken.CompareTag("Chicken"))
                        {
                            PointSystem.Instance.ErasePointP2();
                        }
                        else
                        {
                            PointSystem.Instance.Erase3PointsP2();
                        }
                    }
                    transform.position += v * Time.deltaTime;
                }

            }
        }
        cooldown += Time.deltaTime;
    }
}
