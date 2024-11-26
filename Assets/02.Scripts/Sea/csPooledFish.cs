using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledFish : MonoBehaviour
{
    public static csPooledFish instance;

    public GameObject[] poolObj_Fish;
    public GameObject group_Fish;
    public int poolAmount_Fish;
    [HideInInspector] public List<GameObject> poolObjs_Fish = new List<GameObject>();

    public Transform spawnFishPoint;

    private int fishCnt;
    private int whaleCnt;
    private int dolphinCnt;
    private int octopusCnt;
    private int seaTurtleCnt;

    void Awake()
    {
        if(csPooledFish.instance == null)
        {
            csPooledFish.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolAmount_Fish; i++)
        {
            int ran = Random.Range(0, 12);

            GameObject obj_Fish = (GameObject)Instantiate(poolObj_Fish[ran], spawnFishPoint.position, Quaternion.identity);

            switch(ran)
            {
                case 0:
                    obj_Fish.name = "Fish1";
                    obj_Fish.GetComponent<csFish>().fish = Fish.FISH;
                    break;

                case 1:
                    obj_Fish.name = "Fish2";
                    obj_Fish.GetComponent<csFish>().fish = Fish.FISH;
                    break;

                case 2:
                    obj_Fish.name = "Whale1";
                    obj_Fish.GetComponent<csFish>().fish = Fish.WHALE;
                    break;

                case 3:
                    obj_Fish.name = "Whale2";
                    obj_Fish.GetComponent<csFish>().fish = Fish.WHALE;
                    break;

                case 4:
                    obj_Fish.name = "Fish5";
                    obj_Fish.GetComponent<csFish>().fish = Fish.FISH;
                    break;

                case 5:
                    obj_Fish.name = "Fish6";
                    obj_Fish.GetComponent<csFish>().fish = Fish.FISH;
                    break;

                case 6:
                    obj_Fish.name = "Octopus1";
                    obj_Fish.GetComponent<csFish>().fish = Fish.OCTOPUS;
                    break; 

                case 7:
                    obj_Fish.name = "Octopus2";
                    obj_Fish.GetComponent<csFish>().fish = Fish.OCTOPUS;
                    break;

                case 8:
                    obj_Fish.name = "SeaTurtle1";
                    obj_Fish.GetComponent<csFish>().fish = Fish.SEATURTLE;
                    break;

                case 9:
                    obj_Fish.name = "SeaTurtle2";
                    obj_Fish.GetComponent<csFish>().fish = Fish.SEATURTLE;
                    break;

                case 10:
                    obj_Fish.name = "Fish3";
                    obj_Fish.GetComponent<csFish>().fish = Fish.FISH;
                    break;

                case 11:
                    obj_Fish.name = "Fish4";
                    obj_Fish.GetComponent<csFish>().fish = Fish.FISH;
                    break;
            }

            obj_Fish.transform.parent = group_Fish.transform;
            obj_Fish.SetActive(false);
            poolObjs_Fish.Add(obj_Fish);
        }
    }

    public GameObject GetPooledObject_Fish()
    {
        for (int i = 0; i < poolObjs_Fish.Count; i++)
        {
            if (!poolObjs_Fish[i].activeInHierarchy)
            {
                return poolObjs_Fish[i];
            }
        }

        return null;
    }
}
