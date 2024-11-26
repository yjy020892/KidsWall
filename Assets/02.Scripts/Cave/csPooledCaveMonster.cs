using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledCaveMonster : MonoBehaviour
{
    public static csPooledCaveMonster instance;

    public GameObject[] poolObj_CaveMonster;
    public GameObject group_CaveMonster;
    public int poolAmount_CaveMonster;
    [HideInInspector] public List<GameObject> poolObjs_CaveMonster = new List<GameObject>();

    public Transform spawnCaveMonsterPoint;

    void Awake()
    {
        if(csPooledCaveMonster.instance == null)
        {
            csPooledCaveMonster.instance = this;
        }
    }

    void Start()
    {
        for(int i = 0; i < poolAmount_CaveMonster; i++)
        {
            int ran = Random.Range(0, 4);

            GameObject obj_CaveMonster = (GameObject)Instantiate(poolObj_CaveMonster[ran], spawnCaveMonsterPoint.position, Quaternion.identity);

            switch(ran)
            {
                case 0:
                    obj_CaveMonster.name = "Spider";
                    break;

                case 1:
                    obj_CaveMonster.name = "Hornet";
                    break;

                case 2:
                    obj_CaveMonster.name = "Golem";
                    break;

                case 3:
                    obj_CaveMonster.name = "Bat";
                    break;

                default:
                    break;
            }

            obj_CaveMonster.transform.parent = group_CaveMonster.transform;
            obj_CaveMonster.SetActive(false);
            poolObjs_CaveMonster.Add(obj_CaveMonster);
        }
    }

    public GameObject GetPooledObject_CaveMonster()
    {
        for (int i = 0; i < poolObjs_CaveMonster.Count; i++)
        {
            if (!poolObjs_CaveMonster[i].activeInHierarchy)
            {
                return poolObjs_CaveMonster[i];
            }
        }

        return null;
    }
}
