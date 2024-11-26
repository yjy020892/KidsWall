using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledDragon : MonoBehaviour
{
    public static csPooledDragon instance;

    public GameObject[] poolObj_Dragon;
    public GameObject group_Dragon;
    public int poolAmount_Dragon;
    [HideInInspector] public List<GameObject> poolObjs_Dragon = new List<GameObject>();

    public Transform spawnDragonPoint;

    void Awake()
    {
        if (csPooledDragon.instance == null)
        {
            csPooledDragon.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Dragon; i++)
        {
            int ran = Random.Range(0, 5);

            GameObject obj_Dragon = (GameObject)Instantiate(poolObj_Dragon[ran], spawnDragonPoint.position, Quaternion.identity);

            obj_Dragon.name = "Dragon";
            obj_Dragon.transform.parent = group_Dragon.transform;
            obj_Dragon.SetActive(false);
            poolObjs_Dragon.Add(obj_Dragon);
        }
    }

    public GameObject GetPooledObject_Dragon()
    {
        for (int i = 0; i < poolObjs_Dragon.Count; i++)
        {
            if (!poolObjs_Dragon[i].activeInHierarchy)
            {
                return poolObjs_Dragon[i];
            }
        }

        return null;
    }
}
