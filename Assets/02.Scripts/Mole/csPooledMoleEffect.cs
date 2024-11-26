using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledMoleEffect : MonoBehaviour
{
    public static csPooledMoleEffect instance;

    public GameObject poolObj_MoleEffect;
    public GameObject group_MoleEffect;
    public int poolAmount_MoleEffect;
    [HideInInspector] public List<GameObject> poolObjs_MoleEffect = new List<GameObject>();

    public Transform spawnMoleEffectPoint;

    void Awake()
    {
        if (csPooledMoleEffect.instance == null)
        {
            csPooledMoleEffect.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_MoleEffect; i++)
        {
            GameObject obj_MoleEffect = (GameObject)Instantiate(poolObj_MoleEffect, spawnMoleEffectPoint.position, Quaternion.identity);

            obj_MoleEffect.name = "MoleEffect";
            obj_MoleEffect.transform.parent = group_MoleEffect.transform;
            obj_MoleEffect.SetActive(false);
            poolObjs_MoleEffect.Add(obj_MoleEffect);
        }
    }

    public GameObject GetPooledObject_MoleEffect(Transform posi)
    {
        for (int i = 0; i < poolObjs_MoleEffect.Count; i++)
        {
            if (!poolObjs_MoleEffect[i].activeInHierarchy)
            {
                poolObjs_MoleEffect[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_MoleEffect[i];
            }
        }

        return null;
    }
}
