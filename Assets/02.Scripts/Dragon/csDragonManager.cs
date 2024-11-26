using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDragonManager : MonoBehaviour
{
    public static csDragonManager instance;

    public bool b_GameStart = false;
    public bool b_SpawnDragon = false;

    public int dragonVal = 15;
    [HideInInspector] public int dragonCnt = 0;

    public float spawnDragonTimer = 1.0f;

    void Awake()
    {
        if (csDragonManager.instance == null)
        {
            csDragonManager.instance = this;
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
        if (b_GameStart && !b_SpawnDragon && dragonCnt < dragonVal)
        {
            StartCoroutine(SpawnDragon());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("Dragon"))
                {
                    dragonCnt -= 1;

                    csSoundManager.instance.PlayDragonHitSound();

                    GameObject obj = csPooledDragonEffect.instance.GetPooledObject_DragonEffect(hit.transform);
                    obj.SetActive(true);

                    csPooledDragon.instance.poolObjs_Dragon.Remove(hit.transform.gameObject);
                    csPooledDragon.instance.poolObjs_Dragon.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledDragon.instance.poolObjs_Dragon.Count; i++)
        {
            csPooledDragon.instance.poolObjs_Dragon[i].GetComponent<csDragonHandler>().CheckArea(bytes);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnDragon()
    {
        b_SpawnDragon = true;

        GameObject obj = csPooledDragon.instance.GetPooledObject_Dragon();
        if (obj != null)
        {
            obj.SetActive(true);

            dragonCnt += 1;
        }

        yield return new WaitForSeconds(spawnDragonTimer);

        b_SpawnDragon = false;
    }
}
