using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMonsterManager : MonoBehaviour
{
    private bool b_SpawnMonster = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(!b_SpawnMonster)
        {
            StartCoroutine(SpawnMonster());
        }
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(2.5f);

        b_SpawnMonster = false;
    }

    IEnumerator SpawnMonster()
    {
        b_SpawnMonster = true;

        GameObject obj = csPooledMonster.instance.GetPooledObject_Monster();
        if(obj != null)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);

        b_SpawnMonster = false;
    }
}
