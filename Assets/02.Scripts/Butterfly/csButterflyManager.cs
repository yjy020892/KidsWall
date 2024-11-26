using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csButterflyManager : MonoBehaviour
{
    public static csButterflyManager instance;

    [HideInInspector] public List<GameObject> list_Butterfly = new List<GameObject>();

    public GameObject[] butterflyZone;

    public bool b_GameStart = false;
    public bool b_SpawnDragon = false;

    public float spawnDragonTimer = 1.0f;

    void Awake()
    {
        if (csButterflyManager.instance == null)
        {
            csButterflyManager.instance = this;
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
        if (b_GameStart && !b_SpawnDragon)
        {
            StartCoroutine(SpawnButterfly());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("Butterfly"))
                {
                    csSoundManager.instance.PlayButterflyHitSound();

                    GameObject obj = csPooledButterflyEffect.instance.GetPooledObject_ButterflyEffect(hit.transform);
                    obj.SetActive(true);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < list_Butterfly.Count; i++)
        {
            list_Butterfly[i].GetComponent<csButterflyHandler>().CheckArea(bytes);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnButterfly()
    {
        b_SpawnDragon = true;

        yield return new WaitForSeconds(spawnDragonTimer);

        for(int i = 0; i < butterflyZone.Length; i++)
        {
            butterflyZone[i].SetActive(true);
        }

        b_SpawnDragon = false;
    }
}
