using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class csMoveCaveMonster : MonoBehaviour
{
    #region Public

    [HideInInspector] public Animation animation;
    [HideInInspector] public Animator animator;

    public float speed = 1.0f;
    public float damping = 3.0f;

    [HideInInspector] public bool b_SpiderAnimDeath = false;
    [HideInInspector] public bool b_HornetAnimDeath = false;
    [HideInInspector] public bool b_GolemAnimDeath = false;
    [HideInInspector] public bool b_BatAnimDeath = false;

    #endregion

    #region Private

    private IEnumerator animControlCoroutine;

    private Transform spawnCaveMonsterPoint;
    private Transform tr;
    private Transform[] points;

    private string objNameStr;
    private int nextIdx = 1;

    private bool b_SpiderAnimWalk = true;
    private bool b_HornetAnimFly = true;
    private bool b_GolemAnimWalk = true;
    private bool b_BatAnimFly = true;

    #endregion

    void OnEnable()
    {
        objNameStr = gameObject.name;

        nextIdx = 1;

        b_SpiderAnimWalk = true;
        b_SpiderAnimDeath = false;
        b_HornetAnimFly = true;
        b_HornetAnimDeath = false;
        b_GolemAnimWalk = true;
        b_GolemAnimDeath = false;
        b_BatAnimFly = true;
        b_BatAnimDeath = false;

        CheckObjName();
    }

    void Start()
    {
        tr = this.gameObject.transform;
    }
    
    void Update()
    {
        MoveWayPoint();
    }

    void CheckObjName()
    {
        //string str = gameObject.name;

        switch (objNameStr)
        {
            case "Spider":
                int ran = Random.Range(0, 3);

                StringBuilder sb = new StringBuilder("SpiderPoint");
                sb.Append(ran.ToString());
                spawnCaveMonsterPoint = GameObject.Find(sb.ToString()).transform;
                this.transform.position = spawnCaveMonsterPoint.position;
                points = GameObject.Find(sb.ToString()).GetComponentsInChildren<Transform>();

                animation = this.gameObject.GetComponent<Animation>();

                animControlCoroutine = SpiderAnimControl(ran);

                StartCoroutine(animControlCoroutine);
                break;

            case "Hornet":
                int ran2 = Random.Range(0, 3);

                StringBuilder sb2 = new StringBuilder("HornetPoint");
                sb2.Append(ran2.ToString());
                spawnCaveMonsterPoint = GameObject.Find(sb2.ToString()).transform;
                this.transform.position = spawnCaveMonsterPoint.position;
                points = GameObject.Find(sb2.ToString()).GetComponentsInChildren<Transform>();

                animator = this.gameObject.GetComponent<Animator>();

                animControlCoroutine = HornetAnimControl(ran2);

                StartCoroutine(animControlCoroutine);

                break;

            case "Golem":
                int ran3 = Random.Range(0, 2);

                StringBuilder sb3 = new StringBuilder("GolemPoint");
                sb3.Append(ran3.ToString());
                spawnCaveMonsterPoint = GameObject.Find(sb3.ToString()).transform;
                this.transform.position = spawnCaveMonsterPoint.position;
                points = GameObject.Find(sb3.ToString()).GetComponentsInChildren<Transform>();

                animator = this.gameObject.GetComponent<Animator>();

                animControlCoroutine = GolemAnimControl(ran3);

                StartCoroutine(animControlCoroutine);

                break;

            case "Bat":
                int ran4 = Random.Range(0, 3);

                StringBuilder sb4 = new StringBuilder("BatPoint");
                sb4.Append(ran4.ToString());
                spawnCaveMonsterPoint = GameObject.Find(sb4.ToString()).transform;
                this.transform.position = spawnCaveMonsterPoint.position;
                points = GameObject.Find(sb4.ToString()).GetComponentsInChildren<Transform>();

                animator = this.gameObject.GetComponent<Animator>();

                animControlCoroutine = BatAnimControl(ran4);

                StartCoroutine(animControlCoroutine);

                break;

            default:
                break;
        }
    }

    void MoveWayPoint()
    {
        switch(objNameStr)
        {
            case "Spider":
                if(!b_SpiderAnimWalk)
                {
                    speed = 0;
                }
                else
                {
                    speed = 2;
                }

                if(b_SpiderAnimDeath)
                {
                    speed = 0;
                    StopCoroutine(animControlCoroutine);
                }   
                
                break;

            case "Hornet":
                if (b_HornetAnimFly)
                {
                    speed = 3;
                }
                else
                {
                    speed = 0;
                }

                if(b_HornetAnimDeath)
                {
                    speed = 0;
                    StopCoroutine(animControlCoroutine);
                }

                break;

            case "Golem":
                if (b_GolemAnimWalk)
                {
                    speed = 1.5f;
                }
                else
                {
                    speed = 0;
                }

                if (b_GolemAnimDeath)
                {
                    speed = 0;
                    StopCoroutine(animControlCoroutine);
                }
                break;

            case "Bat":
                if (b_BatAnimFly)
                {
                    speed = 2.5f;
                }
                else
                {
                    speed = 0;
                }

                if (b_BatAnimDeath)
                {
                    speed = 0;
                    StopCoroutine(animControlCoroutine);
                }
                break;
        }
        
        Vector3 direction = points[nextIdx].position - tr.position;

        Quaternion rot = Quaternion.LookRotation(direction);

        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        switch(objNameStr)
        {
            case "Spider":
                if (col.CompareTag("SPIDERPOINT"))
                {
                    nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
                }
                else if(col.CompareTag("DeathZone"))
                {
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(this.gameObject);
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(this.gameObject);
                    this.transform.SetAsLastSibling();

                    this.transform.gameObject.SetActive(false);
                }
                break;

            case "Hornet":
                if (col.CompareTag("HORNETPOINT"))
                {
                    nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
                }
                else if (col.CompareTag("DeathZone"))
                {
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(this.gameObject);
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(this.gameObject);
                    this.transform.SetAsLastSibling();

                    this.transform.gameObject.SetActive(false);
                }
                break;

            case "Golem":
                if (col.CompareTag("GOLEMPOINT"))
                {
                    nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
                }
                else if (col.CompareTag("DeathZone"))
                {
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(this.gameObject);
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(this.gameObject);
                    this.transform.SetAsLastSibling();

                    this.transform.gameObject.SetActive(false);
                }
                break;

            case "Bat":
                if (col.CompareTag("BATPOINT"))
                {
                    nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
                }
                else if (col.CompareTag("DeathZone"))
                {
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(this.gameObject);
                    csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(this.gameObject);
                    this.transform.SetAsLastSibling();

                    this.transform.gameObject.SetActive(false);
                }
                break;

            default:

                break;
        }
    }

    IEnumerator SpiderAnimControl(int ranPosi)
    {
        if (animation != null)
        {
            switch(ranPosi)
            {
                case 0:
                    b_SpiderAnimWalk = true;
                    animation.Play("Walk");

                    yield return new WaitForSeconds(10.0f);

                    int ran = Random.Range(0, 3);

                    if(ran == 0)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack");
                    }
                    else if(ran == 1)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Left");
                    }
                    else if(ran == 2)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Right");
                    }
                    
                    yield return new WaitForSeconds(1.5f);
                    ran = Random.Range(0, 3);

                    if (ran == 0)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack");
                    }
                    else if (ran == 1)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Left");
                    }
                    else if (ran == 2)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Right");
                    }

                    yield return new WaitForSeconds(2.0f);

                    b_SpiderAnimWalk = true;
                    animation.Play("Walk");

                    break;

                case 1:
                    animation.Play("Walk");

                    yield return new WaitForSeconds(10.0f);

                    ran = Random.Range(0, 3);

                    if (ran == 0)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack");
                    }
                    else if (ran == 1)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Left");
                    }
                    else if (ran == 2)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Right");
                    }
                    yield return new WaitForSeconds(1.5f);
                    ran = Random.Range(0, 3);

                    if (ran == 0)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack");
                    }
                    else if (ran == 1)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Left");
                    }
                    else if (ran == 2)
                    {
                        b_SpiderAnimWalk = false;
                        animation.Play("Attack_Right");
                    }

                    yield return new WaitForSeconds(2.0f);

                    b_SpiderAnimWalk = true;
                    animation.Play("Walk");

                    break;

                case 2:
                    b_SpiderAnimWalk = true;
                    animation.Play("Walk");
                    break;

                default:
                    break;
            }
        }
    }

    IEnumerator HornetAnimControl(int ranPosi)
    {
        switch(ranPosi)
        {
            case 0:
                b_HornetAnimFly = true;
                animator.SetBool("Fly", true);

                yield return new WaitForSeconds(5.0f);

                b_HornetAnimFly = false;
                animator.SetBool("Fly", false);
                animator.SetBool("Attack", true);

                yield return new WaitForSeconds(2.0f);

                b_HornetAnimFly = true;
                animator.SetBool("Fly", true);
                animator.SetBool("Attack", false);

                break;

            case 1:
                b_HornetAnimFly = true;
                animator.SetBool("Fly", true);
                animator.SetBool("Attack", false);
                break;
        }

        
    }

    IEnumerator GolemAnimControl(int ranPosi)
    {
        switch (ranPosi)
        {
            case 0:
                b_GolemAnimWalk = true;
                animator.SetBool("Walk", true);

                yield return new WaitForSeconds(15.0f);

                b_GolemAnimWalk = false;
                animator.SetBool("Walk", false);
                animator.SetBool("Attack", true);

                yield return new WaitForSeconds(1.6f);

                b_GolemAnimWalk = true;
                animator.SetBool("Walk", true);
                animator.SetBool("Attack", false);

                yield return new WaitForSeconds(5.0f);

                b_GolemAnimWalk = false;
                animator.SetBool("Walk", false);
                animator.SetBool("Attack2", true);

                yield return new WaitForSeconds(1.6f);

                b_GolemAnimWalk = true;
                animator.SetBool("Walk", true);
                animator.SetBool("Attack2", false);

                break;

            case 1:
                b_GolemAnimWalk = true;
                animator.SetBool("Walk", true);
                animator.SetBool("Attack", false);
                break;
        }


    }

    IEnumerator BatAnimControl(int ranPosi)
    {
        switch (ranPosi)
        {
            case 0:
                b_BatAnimFly = true;
                animator.SetBool("Fly", true);

                yield return new WaitForSeconds(8.0f);

                b_BatAnimFly = false;
                animator.SetBool("Fly", false);
                animator.SetBool("Attack", true);

                yield return new WaitForSeconds(1.6f);

                b_BatAnimFly = true;
                animator.SetBool("Fly", true);
                animator.SetBool("Attack", false);

                break;

            case 1:
                b_BatAnimFly = true;
                animator.SetBool("Fly", true);
                animator.SetBool("Attack", false);
                break;
        }


    }
}
