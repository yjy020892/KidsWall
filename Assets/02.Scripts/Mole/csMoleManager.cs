using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleState
{
    UNDER_GROUND,
    UP,
    ON_GROUND,
    DOWN,
}

public class csMoleManager : MonoBehaviour
{
    public static csMoleManager instance;

    public GameObject moles_gruop;

    [HideInInspector] public List<GameObject> list_Mole = new List<GameObject>();

    public bool b_GameStart = false;
    public bool b_SpawnAnimal = false;

    public float spawnAniamlTimer = 2.0f;

    void Awake()
    {
        if (csMoleManager.instance == null)
        {
            csMoleManager.instance = this;
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
        if (b_GameStart && !b_SpawnAnimal)
        {
            StartCoroutine(GenerateMole());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("Mole"))
                {
                    if(hit.transform.gameObject.GetComponent<csMole>().moleState == MoleState.ON_GROUND)
                    {
                        GameObject hummerObj = csPooledHummer.instance.GetPooledObject_Hummer(hit.transform);
                        hummerObj.SetActive(true);
                    }
                }
            }
        }
    }

    //public void CheckBytes(byte[] bytes)
    //{
    //    for (int i = 0; i < list_Mole.Count; i++)
    //    {
    //        list_Mole[i].GetComponent<csMoleHandler>().CheckArea(bytes);
    //    }
    //}

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        moles_gruop.SetActive(true);

        b_GameStart = true;
    }

    IEnumerator GenerateMole()
    {
        b_SpawnAnimal = true;

        yield return new WaitForSeconds(spawnAniamlTimer);

        for (int i = 0; i < 2; i++)
        {
            int ran = Random.Range(0, list_Mole.Count);

            if (list_Mole[ran].GetComponent<csMole>().moleState == MoleState.UNDER_GROUND)
            {
                list_Mole[ran].GetComponent<csMole>().moleState = MoleState.UP;
            }
        }

        b_SpawnAnimal = false;
    }
}
