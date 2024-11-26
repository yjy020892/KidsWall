using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSeaManager : MonoBehaviour
{
    public static csSeaManager instance;

    public bool b_GameStart = false;
    public bool b_SpawnFish = false;
    
    public int fishVal = 15;
    [HideInInspector]public int fishCnt = 0;

    public float spawnFishTimer = 1.0f;
    
    void Awake()
    {
        if(csSeaManager.instance == null)
        {
            csSeaManager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (b_GameStart && !b_SpawnFish && fishCnt < fishVal)
        {
            StartCoroutine(SpawnFish());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("FISH"))
                {
                    fishCnt -= 1;

                    csSoundManager.instance.PlaySeaHitSound();

                    GameObject obj = csPooledBubble.instance.GetPooledObject_Bubble(hit.transform);
                    obj.SetActive(true);

                    csPooledFish.instance.poolObjs_Fish.Remove(hit.transform.gameObject);
                    csPooledFish.instance.poolObjs_Fish.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledFish.instance.poolObjs_Fish.Count; i++)
        {
            csPooledFish.instance.poolObjs_Fish[i].GetComponent<csSeaHandler>().CheckArea(bytes);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnFish()
    {
        b_SpawnFish = true;

        GameObject obj = csPooledFish.instance.GetPooledObject_Fish();
        if (obj != null)
        {
            obj.SetActive(true);

            fishCnt += 1;
        }

        yield return new WaitForSeconds(spawnFishTimer);

        b_SpawnFish = false;
    }
}
