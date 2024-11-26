using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledAnimal : MonoBehaviour
{
    public static csPooledAnimal instance;

    public GameObject[] poolObj_Animal;
    public GameObject group_Animal;
    public int poolAmount_Animal;
    [HideInInspector] public List<GameObject> poolObjs_Animal = new List<GameObject>();

    public Transform spawnAnimalPoint;

    void Awake()
    {
        if(csPooledAnimal.instance == null)
        {
            csPooledAnimal.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Animal; i++)
        {
            int ran = Random.Range(0, poolObj_Animal.Length);

            GameObject obj_Animal = (GameObject)Instantiate(poolObj_Animal[ran], spawnAnimalPoint.position, Quaternion.identity);

            switch(ran)
            {
                case 0:
                    obj_Animal.name = "Bear";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.BEAR;
                    break;

                case 1:
                    obj_Animal.name = "Wolf";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.WOLF;
                    break;

                case 2:
                    obj_Animal.name = "Bird";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.BIRD;
                    break;

                case 3:
                    obj_Animal.name = "Bird";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.BIRD;
                    break;

                case 4:
                    obj_Animal.name = "Bird";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.BIRD;
                    break;

                case 5:
                    obj_Animal.name = "Fox";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.FOX;
                    break;

                case 6:
                    obj_Animal.name = "Stag";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.STAG;
                    break;

                case 7:
                    obj_Animal.name = "Boar";
                    obj_Animal.GetComponent<csAnimal>().animal = Animal.BOAR;
                    break;
            }

            obj_Animal.transform.parent = group_Animal.transform;
            obj_Animal.SetActive(false);
            poolObjs_Animal.Add(obj_Animal);
        }
    }
    
    public GameObject GetPooledObject_Animal()
    {
        for (int i = 0; i < poolObjs_Animal.Count; i++)
        {
            if (!poolObjs_Animal[i].activeInHierarchy)
            {
                return poolObjs_Animal[i];
            }
        }

        return null;
    }
}
