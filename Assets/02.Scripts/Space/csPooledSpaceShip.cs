using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledSpaceShip : MonoBehaviour
{
    public static csPooledSpaceShip instance;

    public GameObject[] poolObj_SpaceShip;
    public GameObject group_SpaceShip;
    public int poolAmount_SpaceShip;
    [HideInInspector] public List<GameObject> poolObjs_SpaceShip = new List<GameObject>();

    public Transform[] spawnSpaceShipPoint;

    private bool b_SpawnSpaceShipFinish = false;

    void Awake()
    {
        if (csPooledSpaceShip.instance == null)
        {
            csPooledSpaceShip.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolAmount_SpaceShip; i++)
        {
            int ran = Random.Range(0, 8);
            int ran2 = Random.Range(0, 13);

            GameObject obj_SpaceShip = (GameObject)Instantiate(poolObj_SpaceShip[ran], spawnSpaceShipPoint[ran2].position, Quaternion.identity);

            obj_SpaceShip.name = "SpaceShip";
            obj_SpaceShip.transform.parent = group_SpaceShip.transform;
            obj_SpaceShip.SetActive(false);
            poolObjs_SpaceShip.Add(obj_SpaceShip);

            if (i + 1 == poolAmount_SpaceShip)
            {
                b_SpawnSpaceShipFinish = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (poolObjs_SpaceShip.Count < poolAmount_SpaceShip && b_SpawnSpaceShipFinish)
        {
            int ran = Random.Range(0, 8);
            int ran2 = Random.Range(0, 13);

            GameObject obj_SpaceShip = (GameObject)Instantiate(poolObj_SpaceShip[ran], spawnSpaceShipPoint[ran2].position, Quaternion.identity);

            obj_SpaceShip.name = "SpaceShip";
            obj_SpaceShip.transform.parent = group_SpaceShip.transform;

            obj_SpaceShip.SetActive(false);
            poolObjs_SpaceShip.Add(obj_SpaceShip);
        }
    }

    public GameObject GetPooledObject_SpaceShip()
    {
        for (int i = 0; i < poolObjs_SpaceShip.Count; i++)
        {
            if (!poolObjs_SpaceShip[i].activeInHierarchy)
            {
                return poolObjs_SpaceShip[i];
            }
        }

        return null;
    }
}
