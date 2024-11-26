using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledButterflyEffect : MonoBehaviour
{
    public static csPooledButterflyEffect instance;

    public GameObject poolObj_ButterflyEffect;
    public GameObject group_ButterflyEffect;
    public int poolAmount_ButterflyEffect;
    [HideInInspector] public List<GameObject> poolObjs_ButterflyEffect = new List<GameObject>();

    public Transform spawnButterflyEffectPoint;

    void Awake()
    {
        if (csPooledButterflyEffect.instance == null)
        {
            csPooledButterflyEffect.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_ButterflyEffect; i++)
        {
            GameObject obj_ButterflyEffect = (GameObject)Instantiate(poolObj_ButterflyEffect, spawnButterflyEffectPoint.position, Quaternion.identity);

            obj_ButterflyEffect.name = "ButterflyEffect";
            obj_ButterflyEffect.transform.parent = group_ButterflyEffect.transform;
            obj_ButterflyEffect.SetActive(false);
            poolObjs_ButterflyEffect.Add(obj_ButterflyEffect);
        }
    }

    public GameObject GetPooledObject_ButterflyEffect(Transform posi)
    {
        for (int i = 0; i < poolObjs_ButterflyEffect.Count; i++)
        {
            if (!poolObjs_ButterflyEffect[i].activeInHierarchy)
            {
                poolObjs_ButterflyEffect[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_ButterflyEffect[i];
            }
        }

        return null;
    }
}
