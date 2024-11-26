using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledExplosion : MonoBehaviour
{
    public static csPooledExplosion instance;

    public GameObject poolObj_Explosion;
    public GameObject group_Explosion;
    public int poolAmount_Explosion;
    [HideInInspector] public List<GameObject> poolObjs_Explosion = new List<GameObject>();

    public Transform spawnExplosionPoint;

    void Awake()
    {
        if(csPooledExplosion.instance == null)
        {
            csPooledExplosion.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolAmount_Explosion; i++)
        {
            GameObject obj_Explosion = (GameObject)Instantiate(poolObj_Explosion, spawnExplosionPoint.position, Quaternion.identity);

            obj_Explosion.name = "Explosion";
            obj_Explosion.transform.parent = group_Explosion.transform;
            obj_Explosion.SetActive(false);
            poolObjs_Explosion.Add(obj_Explosion);


        }
    }

    public GameObject GetPooledObject_Explosion(Transform posi)
    {
        for (int i = 0; i < poolObjs_Explosion.Count; i++)
        {
            if (!poolObjs_Explosion[i].activeInHierarchy)
            {
                poolObjs_Explosion[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_Explosion[i];
            }
        }

        return null;
    }
}
