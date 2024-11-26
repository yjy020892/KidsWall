using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledDragonEffect : MonoBehaviour
{
    public static csPooledDragonEffect instance;

    public GameObject poolObj_DragonEffect;
    public GameObject group_DragonEffect;
    public int poolAmount_DragonEffect;
    [HideInInspector] public List<GameObject> poolObjs_DragonEffect = new List<GameObject>();

    public Transform spawnDragonEffectPoint;

    void Awake()
    {
        if (csPooledDragonEffect.instance == null)
        {
            csPooledDragonEffect.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_DragonEffect; i++)
        {
            GameObject obj_DragonEffect = (GameObject)Instantiate(poolObj_DragonEffect, spawnDragonEffectPoint.position, Quaternion.identity);

            obj_DragonEffect.name = "ForestEffect";
            obj_DragonEffect.transform.parent = group_DragonEffect.transform;
            obj_DragonEffect.SetActive(false);
            poolObjs_DragonEffect.Add(obj_DragonEffect);
        }
    }

    public GameObject GetPooledObject_DragonEffect(Transform posi)
    {
        for (int i = 0; i < poolObjs_DragonEffect.Count; i++)
        {
            if (!poolObjs_DragonEffect[i].activeInHierarchy)
            {
                poolObjs_DragonEffect[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_DragonEffect[i];
            }
        }

        return null;
    }
}
