using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledReSpawn : MonoBehaviour
{
    public static csPooledReSpawn instance;

    public GameObject poolObj_ReSpawn;
    public GameObject group_ReSpawn;
    public int poolAmount_ReSpawn;
    [HideInInspector] public List<GameObject> poolObjs_ReSpawn = new List<GameObject>();

    public Transform spawnReSpawnPoint;

    void Awake()
    {
        if (csPooledReSpawn.instance == null)
        {
            csPooledReSpawn.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_ReSpawn; i++)
        {
            GameObject obj_ReSpawn = (GameObject)Instantiate(poolObj_ReSpawn, spawnReSpawnPoint.position, Quaternion.identity);

            obj_ReSpawn.name = "ReSpawn";
            obj_ReSpawn.transform.parent = group_ReSpawn.transform;
            obj_ReSpawn.SetActive(false);
            poolObjs_ReSpawn.Add(obj_ReSpawn);
        }
    }

    public GameObject GetPooledObject_ReSpawn(Transform posi)
    {
        for (int i = 0; i < poolObjs_ReSpawn.Count; i++)
        {
            if (!poolObjs_ReSpawn[i].activeInHierarchy)
            {
                poolObjs_ReSpawn[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_ReSpawn[i];
            }
        }

        return null;
    }
}
