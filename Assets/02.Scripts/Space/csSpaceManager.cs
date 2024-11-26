using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSpaceManager : MonoBehaviour
{
    public static csSpaceManager instance;

    public bool b_GameStart = false;

    public int spaceShipCnt = 0;
    public float startTimer = 1.0f;

    private bool b_SpawnStar = false;
    private bool b_SpawnSpaceShip = false;

    void Awake()
    {
        if(csSpaceManager.instance == null)
        {
            csSpaceManager.instance = this;
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
        if (b_GameStart && !b_SpawnStar)
        {
            StartCoroutine(SpawnStar());
        }

        if(b_GameStart && !b_SpawnSpaceShip && spaceShipCnt < 12)
        {
            StartCoroutine(SpawnSpaceShip());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("SPACESHIP"))
                {
                    spaceShipCnt -= 1;

                    csSoundManager.instance.PlaySpaceShipHitSound();

                    GameObject obj = csPooledExplosion.instance.GetPooledObject_Explosion(hit.transform);
                    obj.SetActive(true);

                    int ran = Random.Range(0, 13);
                    hit.transform.position = csPooledSpaceShip.instance.spawnSpaceShipPoint[ran].position;

                    csPooledSpaceShip.instance.poolObjs_SpaceShip.Remove(hit.transform.gameObject);
                    csPooledSpaceShip.instance.poolObjs_SpaceShip.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledSpaceShip.instance.poolObjs_SpaceShip.Count; i++)
            csPooledSpaceShip.instance.poolObjs_SpaceShip[i].GetComponent<csSpaceHandler>().CheckArea(bytes);
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(startTimer);

        b_GameStart = true;
    }

    IEnumerator SpawnStar()
    {
        b_SpawnStar = true;

        GameObject obj = csPooledStar.instance.GetPooledObject_Star();

        if (obj != null)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(0.05f);

        b_SpawnStar = false;
    }

    IEnumerator SpawnSpaceShip()
    {
        b_SpawnSpaceShip = true;

        GameObject obj = csPooledSpaceShip.instance.GetPooledObject_SpaceShip();

        if(obj != null)
        {
            obj.SetActive(true);

            spaceShipCnt += 1;
        }

        yield return new WaitForSeconds(1.0f);

        b_SpawnSpaceShip = false;
    }
}
