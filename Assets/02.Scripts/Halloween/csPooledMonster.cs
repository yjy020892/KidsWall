using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledMonster : MonoBehaviour
{
    public static csPooledMonster instance;

    public GameObject[] poolObj_Monster;
    public GameObject group_Monster;
    public int poolAmount_Monster;
    [HideInInspector]public List<GameObject> poolObjs_Monster = new List<GameObject>();

    public Transform[] spawnMonsterPoint;

    private bool b_SpawnMonsterFinish = false;

    void Awake()
    {
        if (csPooledMonster.instance == null)
        {
            csPooledMonster.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Monster; i++)
        {
            int ran = Random.Range(0, 8);
            int ran2 = Random.Range(0, 8);

            GameObject obj_Monster = (GameObject)Instantiate(poolObj_Monster[ran], spawnMonsterPoint[ran2].position, Quaternion.identity);

            obj_Monster.name = "Monster";
            obj_Monster.transform.parent = group_Monster.transform;

            obj_Monster.SetActive(false);
            poolObjs_Monster.Add(obj_Monster);

            if(i + 1 == poolAmount_Monster)
            {
                b_SpawnMonsterFinish = true;
            }
        }
    }

    void Update()
    {
        if(poolObjs_Monster.Count < poolAmount_Monster && b_SpawnMonsterFinish)
        {
            int ran = Random.Range(0, 8);
            int ran2 = Random.Range(0, 8);

            GameObject obj_Monster = (GameObject)Instantiate(poolObj_Monster[ran], spawnMonsterPoint[ran2].position, Quaternion.identity);

            obj_Monster.name = "Monster";
            obj_Monster.transform.parent = group_Monster.transform;

            obj_Monster.SetActive(false);
            poolObjs_Monster.Add(obj_Monster);
        }
    }



    public GameObject GetPooledObject_Monster()
    {
        for (int i = 0; i < poolObjs_Monster.Count; i++)
        {
            if (!poolObjs_Monster[i].activeInHierarchy)
            {
                return poolObjs_Monster[i];
            }
        }

        return null;
    }
}
