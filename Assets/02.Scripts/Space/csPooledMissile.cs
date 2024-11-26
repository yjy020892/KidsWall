using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledMissile : MonoBehaviour
{
    public static csPooledMissile instance;

    public GameObject poolObj_Missile;
    public GameObject group_Missile;
    public int poolAmount_Missile;
    [HideInInspector] public List<GameObject> poolObjs_Missile = new List<GameObject>();

    public Transform spawnMissilePoint;

    private bool b_SpawnMissileFinish = false;

    void Awake()
    {
        if(csPooledMissile.instance == null)
        {
            csPooledMissile.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolAmount_Missile; i++)
        {
            GameObject obj_Missile = (GameObject)Instantiate(poolObj_Missile, spawnMissilePoint.position, Quaternion.identity);

            obj_Missile.name = "Missile";
            obj_Missile.transform.parent = group_Missile.transform;
            obj_Missile.SetActive(false);
            poolObjs_Missile.Add(obj_Missile);

            if (i + 1 == poolAmount_Missile)
            {
                b_SpawnMissileFinish = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject_Missile()
    {
        for (int i = 0; i < poolObjs_Missile.Count; i++)
        {
            if (!poolObjs_Missile[i].activeInHierarchy)
            {
                return poolObjs_Missile[i];
            }
        }

        return null;
    }
}
