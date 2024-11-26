using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledForestEffect : MonoBehaviour
{
    public static csPooledForestEffect instance;

    public GameObject poolObj_ForestEffect;
    public GameObject group_ForestEffect;
    public int poolAmount_ForestEffect;
    [HideInInspector] public List<GameObject> poolObjs_ForestEffect = new List<GameObject>();

    public Transform spawnForestEffectPoint;

    void Awake()
    {
        if (csPooledForestEffect.instance == null)
        {
            csPooledForestEffect.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_ForestEffect; i++)
        {
            GameObject obj_ForestEffect = (GameObject)Instantiate(poolObj_ForestEffect, spawnForestEffectPoint.position, Quaternion.identity);

            obj_ForestEffect.name = "ForestEffect";
            obj_ForestEffect.transform.parent = group_ForestEffect.transform;
            obj_ForestEffect.SetActive(false);
            poolObjs_ForestEffect.Add(obj_ForestEffect);
        }
    }

    public GameObject GetPooledObject_ForestEffect(Transform posi)
    {
        for (int i = 0; i < poolObjs_ForestEffect.Count; i++)
        {
            if (!poolObjs_ForestEffect[i].activeInHierarchy)
            {
                poolObjs_ForestEffect[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_ForestEffect[i];
            }
        }

        return null;
    }
}
