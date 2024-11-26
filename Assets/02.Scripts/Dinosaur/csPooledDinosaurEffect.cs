using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledDinosaurEffect : MonoBehaviour
{
    public static csPooledDinosaurEffect instance;

    public GameObject poolObj_DinosaurEffect;
    public GameObject group_DinosaurEffect;
    public int poolAmount_DinosaurEffect;
    [HideInInspector] public List<GameObject> poolObjs_DinosaurEffect = new List<GameObject>();

    public Transform spawnDinosaurEffectPoint;

    void Awake()
    {
        if (csPooledDinosaurEffect.instance == null)
        {
            csPooledDinosaurEffect.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_DinosaurEffect; i++)
        {
            GameObject obj_DinosaurEffect = (GameObject)Instantiate(poolObj_DinosaurEffect, spawnDinosaurEffectPoint.position, Quaternion.identity);

            obj_DinosaurEffect.name = "DinosaurEffect";
            obj_DinosaurEffect.transform.parent = group_DinosaurEffect.transform;
            obj_DinosaurEffect.SetActive(false);
            poolObjs_DinosaurEffect.Add(obj_DinosaurEffect);
        }
    }

    public GameObject GetPooledObject_DinosaurEffect(Transform posi)
    {
        for (int i = 0; i < poolObjs_DinosaurEffect.Count; i++)
        {
            if (!poolObjs_DinosaurEffect[i].activeInHierarchy)
            {
                poolObjs_DinosaurEffect[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_DinosaurEffect[i];
            }
        }

        return null;
    }
}
