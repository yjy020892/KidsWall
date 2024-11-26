using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledZombie : MonoBehaviour
{
    public static csPooledZombie instance;

    public GameObject[] poolObj_Zombie;
    public GameObject group_Zombie;
    public int poolAmount_Zombie;
    [HideInInspector] public List<GameObject> poolObjs_Zombie = new List<GameObject>();

    public Transform spawnZombiePoint;

    void Awake()
    {
        if (csPooledZombie.instance == null)
        {
            csPooledZombie.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Zombie; i++)
        {
            int ran = Random.Range(0, poolObj_Zombie.Length);

            GameObject obj_Zombie = (GameObject)Instantiate(poolObj_Zombie[ran], spawnZombiePoint.position, Quaternion.identity);
            
            if(ran == 22 || ran == 23 || ran == 24)
            {
                obj_Zombie.name = "Crow";
            }
            else
            {
                obj_Zombie.name = "Zombie";
            }
            
            obj_Zombie.transform.parent = group_Zombie.transform;
            obj_Zombie.SetActive(false);
            poolObjs_Zombie.Add(obj_Zombie);
        }
    }

    public GameObject GetPooledObject_Zombie()
    {
        for (int i = 0; i < poolObjs_Zombie.Count; i++)
        {
            if (!poolObjs_Zombie[i].activeInHierarchy)
            {
                return poolObjs_Zombie[i];
            }
        }

        return null;
    }
}
