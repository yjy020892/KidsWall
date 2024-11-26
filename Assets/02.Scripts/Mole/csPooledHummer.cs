using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledHummer : MonoBehaviour
{
    public static csPooledHummer instance;

    public GameObject poolObj_Hummer;
    public GameObject group_Hummer;
    public int poolAmount_Hummer;
    [HideInInspector] public List<GameObject> poolObjs_Hummer = new List<GameObject>();

    public Transform spawnHummerPoint;

    void Awake()
    {
        if (csPooledHummer.instance == null)
        {
            csPooledHummer.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Hummer; i++)
        {
            GameObject obj_Hummer = (GameObject)Instantiate(poolObj_Hummer, spawnHummerPoint.position, Quaternion.identity);

            obj_Hummer.name = "Hummer";
            obj_Hummer.transform.parent = group_Hummer.transform;
            obj_Hummer.SetActive(false);
            poolObjs_Hummer.Add(obj_Hummer);
        }
    }

    public GameObject GetPooledObject_Hummer(Transform posi)
    {
        for (int i = 0; i < poolObjs_Hummer.Count; i++)
        {
            if (!poolObjs_Hummer[i].activeInHierarchy)
            {
                poolObjs_Hummer[i].transform.SetPositionAndRotation(posi.position, Quaternion.Euler(-90.0f, 0, 52.263f));

                return poolObjs_Hummer[i];
            }
        }

        return null;
    }
}
