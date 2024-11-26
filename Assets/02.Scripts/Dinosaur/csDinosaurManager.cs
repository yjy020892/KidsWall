using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDinosaurManager : MonoBehaviour
{
    public static csDinosaurManager instance;

    [HideInInspector] public bool b_T_RexWayNum1 = false;

    public bool b_GameStart = false;
    public bool b_SpawnDinosaur = false;

    public int dinosaurVal = 15;
    [HideInInspector] public int dinosaurCnt = 0;
    [HideInInspector] public int t_RexCnt = 0;

    public float spawnDinosaurTimer = 1.0f;

    void Awake()
    {
        if (csDinosaurManager.instance == null)
        {
            csDinosaurManager.instance = this;
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
        if (b_GameStart && !b_SpawnDinosaur && dinosaurCnt < dinosaurVal)
        {
            StartCoroutine(SpawnDinosaur());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("DINOSAUR"))
                {
                    if(hit.transform.gameObject.GetComponent<csDinosaur>().wayNum == 1)
                    {
                        b_T_RexWayNum1 = false;
                    }

                    dinosaurCnt -= 1;

                    if (hit.transform.name.Equals("T_REX"))
                    {
                        t_RexCnt -= 1;
                    }

                    csSoundManager.instance.PlayDinosaurHitSound();

                    GameObject obj = csPooledDinosaurEffect.instance.GetPooledObject_DinosaurEffect(hit.transform);
                    obj.SetActive(true);

                    csPooledDinosaur.instance.poolObjs_Dinosaur.Remove(hit.transform.gameObject);
                    csPooledDinosaur.instance.poolObjs_Dinosaur.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledDinosaur.instance.poolObjs_Dinosaur.Count; i++)
        {
            csPooledDinosaur.instance.poolObjs_Dinosaur[i].GetComponent<csDinosaurHandler>().CheckArea(bytes);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnDinosaur()
    {
        b_SpawnDinosaur = true;

        GameObject obj = csPooledDinosaur.instance.GetPooledObject_Dinosaur();
        if (obj != null)
        {
            obj.SetActive(true);

            dinosaurCnt += 1;

            if (obj.GetComponent<csDinosaur>().dinosaurs == Dinosaurs.T_REX && t_RexCnt < 3)
            {
                t_RexCnt += 1;
            }
            else if (obj.GetComponent<csDinosaur>().dinosaurs == Dinosaurs.T_REX && t_RexCnt >= 3)
            {
                dinosaurCnt -= 1;
                obj.SetActive(false);

                csPooledDinosaur.instance.poolObjs_Dinosaur.Remove(obj.transform.gameObject);
                csPooledDinosaur.instance.poolObjs_Dinosaur.Add(obj.transform.gameObject);
                obj.transform.SetAsLastSibling();
            }
        }

        yield return new WaitForSeconds(spawnDinosaurTimer);

        b_SpawnDinosaur = false;
    }
}
