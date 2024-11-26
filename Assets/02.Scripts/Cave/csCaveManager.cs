using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCaveManager : MonoBehaviour
{
    public static csCaveManager instance;

    public bool b_GameStart = false;
    public bool b_SpawnCaveMonster = false;

    private bool b_Death = false;

    public float spawnSpiderTimer = 1.0f;

    void Awake()
    {
        if(csCaveManager.instance == null)
        {
            csCaveManager.instance = this;
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
        if(b_GameStart && !b_SpawnCaveMonster)
        {
            StartCoroutine(SpawnMonster());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("MONSTER") && !b_Death)
                {
                    if(hit.transform.gameObject.name.Equals("Spider"))
                    {
                        b_Death = true;

                        csSoundManager.instance.PlayCaveHitSound();

                        GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(hit.transform);
                        obj.SetActive(true);

                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(hit.transform.gameObject);
                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(hit.transform.gameObject);

                        csMoveCaveMonster cs = hit.transform.gameObject.GetComponent<csMoveCaveMonster>();

                        cs.animation.Play("Death");
                        cs.b_SpiderAnimDeath = true;
                        StartCoroutine(MonsterDeathMotion(hit));

                        //hit.transform.SetAsLastSibling();

                        //hit.transform.gameObject.SetActive(false);
                    }
                    else if(hit.transform.gameObject.name.Equals("Hornet"))
                    {
                        b_Death = true;

                        csSoundManager.instance.PlayCaveHitSound();

                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(hit.transform.gameObject);
                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(hit.transform.gameObject);
                            
                        csMoveCaveMonster cs = hit.transform.gameObject.GetComponent<csMoveCaveMonster>();

                        cs.animator.SetBool("Die", true);
                        cs.animator.SetBool("Fly", false);
                        cs.animator.SetBool("Attack", false);
                        cs.b_HornetAnimDeath = true;
                        StartCoroutine(MonsterDeathMotion(hit));
                    }
                    else if (hit.transform.gameObject.name.Equals("Golem"))
                    {
                        b_Death = true;

                        csSoundManager.instance.PlayCaveHitSound();

                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(hit.transform.gameObject);
                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(hit.transform.gameObject);

                        csMoveCaveMonster cs = hit.transform.gameObject.GetComponent<csMoveCaveMonster>();

                        cs.animator.SetBool("Die", true);
                        cs.animator.SetBool("Walk", false);
                        cs.animator.SetBool("Attack", false);
                        cs.animator.SetBool("Attack2", false);
                        cs.b_GolemAnimDeath = true;
                        StartCoroutine(MonsterDeathMotion(hit));
                    }
                    else if (hit.transform.gameObject.name.Equals("Bat"))
                    {
                        b_Death = true;

                        csSoundManager.instance.PlayCaveHitSound();

                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(hit.transform.gameObject);
                        csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(hit.transform.gameObject);

                        csMoveCaveMonster cs = hit.transform.gameObject.GetComponent<csMoveCaveMonster>();

                        cs.animator.SetBool("Die", true);
                        cs.animator.SetBool("Fly", false);
                        cs.animator.SetBool("Attack", false);
                        cs.animator.SetBool("Attack2", false);
                        cs.b_BatAnimDeath = true;
                        StartCoroutine(MonsterDeathMotion(hit));
                    }
                    //spaceShipCnt -= 1;
                }
            }
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnMonster()
    {
        b_SpawnCaveMonster = true;

        GameObject obj = csPooledCaveMonster.instance.GetPooledObject_CaveMonster();
        if (obj != null)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(spawnSpiderTimer);

        b_SpawnCaveMonster = false;
    }

    IEnumerator MonsterDeathMotion(RaycastHit hit)
    {
        if (hit.transform.gameObject.name.Equals("Hornet"))
        {
            yield return new WaitForSeconds(0.6f);

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(hit.transform);
            obj.SetActive(true);
        }
        else if (hit.transform.gameObject.name.Equals("Golem"))
        {
            yield return new WaitForSeconds(1.0f);

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(hit.transform);
            obj.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            obj.transform.GetChild(0).localScale = new Vector3(2.0f, 2.0f, 2.0f);
            obj.SetActive(true);
        }
        else if (hit.transform.gameObject.name.Equals("Bat"))
        {
            yield return new WaitForSeconds(1.0f);

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(hit.transform);
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);

        //if(hit.transform.gameObject.name.Equals("Spider"))
        //{
        hit.transform.SetAsLastSibling();
        hit.transform.gameObject.SetActive(false);

        b_Death = false;
        //}
    }
    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledCaveMonster.instance.poolObjs_CaveMonster.Count; i++)
        {
            csPooledCaveMonster.instance.poolObjs_CaveMonster[i].GetComponent<csCaveHandler>().CheckArea(bytes);
        }
    }
}
