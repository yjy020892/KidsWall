using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csHalloweenManager : MonoBehaviour
{
    public static csHalloweenManager instance;

    void Awake()
    {
        if(csHalloweenManager.instance == null)
        {
            csHalloweenManager.instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("MONSTER"))
                {
                    csSoundManager.instance.PlayHalloweenHitSound();

                    GameObject obj = csPooledCandy.instance.GetPooledObject_Candy(hit.transform);
                    obj.SetActive(true);
                    
                    int way = hit.transform.gameObject.GetComponent<csMoveMonster>().randomWay;
                    hit.transform.position = csPooledMonster.instance.spawnMonsterPoint[way].position;
                    
                    csPooledMonster.instance.poolObjs_Monster.Remove(hit.transform.gameObject);
                    csPooledMonster.instance.poolObjs_Monster.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledMonster.instance.poolObjs_Monster.Count; i++)
            csPooledMonster.instance.poolObjs_Monster[i].GetComponent<csHalloweenHandler>().CheckArea(bytes);
    }
}
