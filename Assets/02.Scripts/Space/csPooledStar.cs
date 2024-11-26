using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledStar : MonoBehaviour
{
    public static csPooledStar instance;

    public GameObject poolObj_Star;
    public GameObject group_Star;
    public int poolAmount_Star;
    [HideInInspector]public List<GameObject> poolObjs_Star = new List<GameObject>();

    public Transform spawnStarPoint;

    void Awake()
    {
        if(csPooledStar.instance == null)
        {
            csPooledStar.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolAmount_Star; i++)
        {
            GameObject obj_Star = (GameObject)Instantiate(poolObj_Star, spawnStarPoint.position, Quaternion.identity);

            obj_Star.name = "Star";
            obj_Star.transform.parent = group_Star.transform;

            obj_Star.SetActive(false);
            poolObjs_Star.Add(obj_Star);
        }
    }

    public GameObject GetPooledObject_Star()
    {
        for (int i = 0; i < poolObjs_Star.Count; i++)
        {
            if (!poolObjs_Star[i].activeInHierarchy)
            {
                int ranPosiX = Random.Range(1, Screen.width);
                int ranPosiY = Random.Range(1, Screen.height);

                float ranScale = Random.Range(0.1f, 0.4f);

                Vector3 objScreenPosi = Camera.main.WorldToScreenPoint(poolObjs_Star[i].transform.position);
                Vector3 objWolrdPosi = Camera.main.ScreenToWorldPoint(new Vector3(ranPosiX, ranPosiY, objScreenPosi.z));

                poolObjs_Star[i].transform.SetPositionAndRotation(objWolrdPosi, Quaternion.identity);
                poolObjs_Star[i].transform.localScale = new Vector3(ranScale, ranScale, 1.0f);

                return poolObjs_Star[i];
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("world : " + this.transform.position);
        //Debug.Log("screen : " + Camera.main.WorldToScreenPoint(this.transform.position));
    }
}
