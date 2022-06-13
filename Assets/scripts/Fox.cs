using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private GameObject[] chickens;
    private GameObject golden_chicken = null;

    public float speed_fox;
    float minx = 30;
    float maxx = 70;
    float minz = 0;
    float maxz = 100;

    // Params

    private void Start()
    {
        speed_fox = 15;
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
        golden_chicken = GameObject.FindGameObjectWithTag("GoldenChicken");
    }

    bool im_free()
    {
        return (transform.position.x > minx) && (transform.position.x < maxx);
    }

    bool is_free(GameObject go)
    {
        return (go.transform.position.x > minx) && (go.transform.position.x < maxx);
    }

    private void Update()
    {
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
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
                if (is_free(golden_chicken)){
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

            if (best_chicken != null)
            {
                float dx = best_chicken.transform.position.x - transform.position.x;
                float dz = best_chicken.transform.position.z - transform.position.z;

                Vector3 v = new Vector3(dx, 0, dz);
                v *= speed_fox / Mathf.Sqrt(dx * dx + dz * dz);
                if ((dx > 0.7 && dz > 0.7) || (dx < -0.7 && dz < -0.7) || (dx < -0.7 && dz > 0.7) || (dx > 0.7 && dz < -0.7))
                {
                    Quaternion rotation = Quaternion.LookRotation(v, Vector3.up);
                    transform.rotation = rotation;

                }
                transform.position += v * Time.deltaTime;
            }
            
        }
    }
}
