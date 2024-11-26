using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csZombieManager : MonoBehaviour
{
    public static csZombieManager instance;

    public bool b_GameStart = false;
    public bool b_SpawnZombie = false;

    public int zombieVal = 15;
    [HideInInspector] public int zombieCnt = 0;

    public float spawnZombieTimer = 1.0f;

    void Awake()
    {
        if (csZombieManager.instance == null)
        {
            csZombieManager.instance = this;
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
        if (b_GameStart && !b_SpawnZombie && zombieCnt < zombieVal)
        {
            StartCoroutine(SpawnZombie());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals("ZOMBIE"))
                {
                    zombieCnt -= 1;

                    if(hit.transform.gameObject.name.Equals("Zombie"))
                    {
                        csSoundManager.instance.PlayZombieHitSound();
                    }
                    else if(hit.transform.gameObject.name.Equals("Crow"))
                    {
                        csSoundManager.instance.PlayBirdHitSound();
                    }
                    

                    GameObject obj = csPooledZombieEffect.instance.GetPooledObject_ZombieEffect(hit.transform);
                    obj.SetActive(true);

                    csPooledZombie.instance.poolObjs_Zombie.Remove(hit.transform.gameObject);
                    csPooledZombie.instance.poolObjs_Zombie.Add(hit.transform.gameObject);
                    hit.transform.SetAsLastSibling();

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBytes(byte[] bytes)
    {
        for (int i = 0; i < csPooledZombie.instance.poolObjs_Zombie.Count; i++)
        {
            csPooledZombie.instance.poolObjs_Zombie[i].GetComponent<csZombieHandler>().CheckArea(bytes);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        b_GameStart = true;
    }

    IEnumerator SpawnZombie()
    {
        b_SpawnZombie = true;

        GameObject obj = csPooledZombie.instance.GetPooledObject_Zombie();
        if (obj != null)
        {
            obj.SetActive(true);

            zombieCnt += 1;
        }

        yield return new WaitForSeconds(spawnZombieTimer);

        b_SpawnZombie = false;
    }
}
