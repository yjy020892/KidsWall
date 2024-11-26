using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledBubble : MonoBehaviour
{
    public static csPooledBubble instance;

    public GameObject poolObj_Bubble;
    public GameObject group_Bubble;
    public int poolAmount_Bubble;
    [HideInInspector] public List<GameObject> poolObjs_Bubble = new List<GameObject>();

    public Transform spawnBubblePoint;

    void Awake()
    {
        if (csPooledBubble.instance == null)
        {
            csPooledBubble.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Bubble; i++)
        {
            GameObject obj_Bubble = (GameObject)Instantiate(poolObj_Bubble, spawnBubblePoint.position, Quaternion.identity);
            obj_Bubble.name = "Bubble";
            obj_Bubble.transform.parent = group_Bubble.transform;
            obj_Bubble.SetActive(false);
            poolObjs_Bubble.Add(obj_Bubble);
        }
    }

    public GameObject GetPooledObject_Bubble(Transform posi)
    {
        for (int i = 0; i < poolObjs_Bubble.Count; i++)
        {
            if (!poolObjs_Bubble[i].activeInHierarchy)
            {
                poolObjs_Bubble[i].transform.SetPositionAndRotation(posi.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

                return poolObjs_Bubble[i];
            }
        }

        return null;
    }
}
