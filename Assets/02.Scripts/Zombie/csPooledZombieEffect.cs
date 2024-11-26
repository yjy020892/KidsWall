using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledZombieEffect : MonoBehaviour
{
    public static csPooledZombieEffect instance;

    public GameObject poolObj_ZombieEffect;
    public GameObject group_ZombieEffect;
    public int poolAmount_ZombieEffect;
    [HideInInspector] public List<GameObject> poolObjs_ZombieEffect = new List<GameObject>();

    public Transform spawnZombieEffectPoint;

    void Awake()
    {
        if (csPooledZombieEffect.instance == null)
        {
            csPooledZombieEffect.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_ZombieEffect; i++)
        {
            GameObject obj_ZombieEffect = (GameObject)Instantiate(poolObj_ZombieEffect, spawnZombieEffectPoint.position, Quaternion.identity);

            obj_ZombieEffect.name = "ZombieEffect";
            obj_ZombieEffect.transform.parent = group_ZombieEffect.transform;
            obj_ZombieEffect.SetActive(false);
            poolObjs_ZombieEffect.Add(obj_ZombieEffect);
        }
    }

    public GameObject GetPooledObject_ZombieEffect(Transform posi)
    {
        for (int i = 0; i < poolObjs_ZombieEffect.Count; i++)
        {
            if (!poolObjs_ZombieEffect[i].activeInHierarchy)
            {
                poolObjs_ZombieEffect[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_ZombieEffect[i];
            }
        }

        return null;
    }
}
