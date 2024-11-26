using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledCandy : MonoBehaviour
{
    public static csPooledCandy instance;

    public GameObject poolObj_Candy;
    public GameObject group_Candy;
    public int poolAmount_Candy;
    [HideInInspector] public List<GameObject> poolObjs_Candy = new List<GameObject>();

    public Transform spawnCandyPoint;

    private bool b_SpawnCandyFinish = false;

    void Awake()
    {
        if(csPooledCandy.instance == null)
        {
            csPooledCandy.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Candy; i++)
        {
            GameObject obj_Candy = (GameObject)Instantiate(poolObj_Candy, spawnCandyPoint.position, Quaternion.identity);

            obj_Candy.name = "Candy";
            obj_Candy.transform.parent = group_Candy.transform;

            obj_Candy.SetActive(false);
            poolObjs_Candy.Add(obj_Candy);

            if(i + 1 == poolAmount_Candy)
            {
                b_SpawnCandyFinish = true;
            }
        }
    }

    void Update()
    {
        if(poolObjs_Candy.Count < poolAmount_Candy && b_SpawnCandyFinish)
        {
            GameObject obj_Candy = (GameObject)Instantiate(poolObj_Candy, spawnCandyPoint.position, Quaternion.identity);

            obj_Candy.name = "Candy";
            obj_Candy.transform.parent = group_Candy.transform;

            obj_Candy.SetActive(false);
            poolObjs_Candy.Add(obj_Candy);
        }
    }

    public GameObject GetPooledObject_Candy(Transform posi)
    {
        for (int i = 0; i < poolObjs_Candy.Count; i++)
        {
            if (!poolObjs_Candy[i].activeInHierarchy)
            {
                poolObjs_Candy[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_Candy[i];
            }
        }

        return null;
    }
}
