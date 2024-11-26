using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csForestManager : MonoBehaviour
{
    public static csForestManager instance;

    public bool b_GameStart = false;
    public bool b_SpawnAnimal = false;

    public int animalVal = 15;
    [HideInInspector] public int animalCnt = 0;
    [HideInInspector] public int birdCnt = 0;

    public float spawnAniamlTimer = 1.0f;

    void Awake()
    {
        if(csForestManager.instance == null)
        {
            csForestManager.instance = this;
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
        if (b_GameStart && !b_SpawnAnimal && animalCnt < animalVal)
        {
            StartCoroutine(SpawnAnimal());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("ANIMAL"))
                {
                    animalCnt -= 1;

                    if(hit.transform.name.Equals("Bear"))
                    {
                        csSoundManager.instance.PlayBearHitSound();
                    }
                    else if(hit.transform.name.Equals("Wolf") || hit.transform.name.Equals("Fox"))
                    {
                        csSoundManager.instance.PlayWolfHitSound();
                    }
                    else if (hit.transform.name.Equals("Stag"))
                    {
                        csSoundManager.instance.PlayStagHitSound();
                    }
                    else if(hit.transform.name.Equals("Boar"))
                    {
                        csSoundManager.instance.PlayPigHitSound();
                    }
                    else if(hit.transform.name.Equals("Bird"))
                    {
                        birdCnt -= 1;
                        csSoundManager.instance.PlayBirdHitSound();
                    }

                    GameObject obj = csPooledForestEffect.instance.GetPooledObject_ForestEffect(hit.transform);
                    Transform objPosi = obj.transform;
                    objPosi.position = new Vector3(objPosi.position.x, objPosi.position.y + 3, objPosi.position.z);
                    obj.SetActive(true);

                    csPooledAnimal.instance.poolObjs_Animal.Remove(hit.transform.gameObject);
                    csPooledAnimal.instance.poolObjs_Animal.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledAnimal.instance.poolObjs_Animal.Count; i++)
        {
            csPooledAnimal.instance.poolObjs_Animal[i].GetComponent<csForestHandler>().CheckArea(bytes);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnAnimal()
    {
        b_SpawnAnimal = true;

        GameObject obj = csPooledAnimal.instance.GetPooledObject_Animal();
        if (obj != null)
        {
            obj.SetActive(true);

            animalCnt += 1;

            if(obj.GetComponent<csAnimal>().animal == Animal.BIRD && birdCnt < 5)
            {
                birdCnt += 1;
            }
            else if(obj.GetComponent<csAnimal>().animal == Animal.BIRD && birdCnt >= 5)
            {
                animalCnt -= 1;
                obj.SetActive(false);

                csPooledAnimal.instance.poolObjs_Animal.Remove(obj.transform.gameObject);
                csPooledAnimal.instance.poolObjs_Animal.Add(obj.transform.gameObject);
                obj.transform.SetAsLastSibling();
            }
        }

        yield return new WaitForSeconds(spawnAniamlTimer);

        b_SpawnAnimal = false;
    }
}
