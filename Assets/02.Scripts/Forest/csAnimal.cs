using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum Animal
{
    NONE,
    BEAR,
    WOLF,
    BIRD,
    FOX,
    STAG,
    BOAR,
}

public class csAnimal : MonoBehaviour
{
    public Animal animal = Animal.NONE;

    public float speed = 1.0f;
    public float damping = 3.0f;
    public float gravity = 5.0f;

    public Animation animation;

    StringBuilder wayNameStr;

    private CharacterController controller;
    private Vector3 moveDir;

    private Transform tr;
    private Transform[] points;

    private int nextIdx = 1;
    private int preNextIdx = 0;

    private bool b_suffle = false;

    private bool b_StopAnimation;
    private bool b_RunAnimation;

    void OnEnable()
    {
        if (animal != Animal.BIRD)
        {
            moveDir = Vector3.zero;
            controller = this.GetComponent<CharacterController>();
            animation = this.GetComponent<Animation>();
        }

        nextIdx = 1;
        
        b_RunAnimation = false;
        b_StopAnimation = false;
        tr = this.GetComponent<Transform>();

        StringBuilder sbWay = new StringBuilder("AnimalWayGroup");
        int wayNum = Random.Range(1, 6);
        sbWay.Append(wayNum);

        switch (animal)
        {
            case Animal.BIRD:
                points = GameObject.Find("BirdWayGroup").GetComponentsInChildren<Transform>();

                nextIdx = Random.Range(1, points.Length);

                StringBuilder sb = new StringBuilder("BirdWay");
                sb.Append(nextIdx);

                tr.position = GameObject.Find(sb.ToString()).transform.position;
                
                break;

            default:
                points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();

                tr.position = GameObject.Find(sbWay.ToString()).transform.position;

                StartCoroutine(AnimalAnimation(wayNum));
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        MoveWayPoint();

        if (animal == Animal.BIRD)
        {
            if (b_suffle)
            {
                b_suffle = false;

                int ran = Random.Range(1, points.Length);

                if (nextIdx != ran)
                {
                    nextIdx = ran;
                }
                else
                {
                    b_suffle = true;
                }
            }
        }
    }

    void MoveWayPoint()
    {
        if(controller != null && controller.isGrounded)
        {
            switch(animal)
            {
                case Animal.BEAR:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 7.5f;
                        }
                        else
                        {
                            speed = 4.0f;
                        }
                    }
                    break;

                case Animal.WOLF:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 7.5f;
                        }
                        else
                        {
                            speed = 2.0f;
                        }
                    }
                    break;

                case Animal.FOX:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 9.5f;
                        }
                        else
                        {
                            speed = 4.0f;
                        }
                    }
                    break;

                case Animal.STAG:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 8.5f;
                        }
                        else
                        {
                            speed = 5.0f;
                        }
                    }
                    break;

                case Animal.BOAR:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 8.5f;
                        }
                        else
                        {
                            speed = 5.0f;
                        }
                    }
                    break;
            }

            controller.Move(moveDir);
        }
        else if(controller != null && !controller.isGrounded)
        {
            moveDir.y -= gravity * Time.deltaTime;

            controller.Move(moveDir);
        }
        //moveDir = transform.TransformDirection(moveDir);
        
        Vector3 direction = points[nextIdx].position - tr.position;

        Quaternion rot = Quaternion.LookRotation(direction);

        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if (animal != Animal.BIRD)
        {
            wayNameStr = new StringBuilder("AnimalWay");
            wayNameStr.Append(nextIdx);

            if (col.CompareTag("DeathZone"))
            {
                csPooledAnimal.instance.poolObjs_Animal.Remove(this.gameObject);
                csPooledAnimal.instance.poolObjs_Animal.Add(this.gameObject);
                this.transform.SetAsLastSibling();

                this.transform.gameObject.SetActive(false);

                csForestManager.instance.animalCnt -= 1;
            }
            else if (col.name.Equals(wayNameStr.ToString()))
            {
                nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
            }
        }
        else if(animal == Animal.BIRD)
        {
            if (nextIdx == preNextIdx)
            {
                return;
            }
            Debug.Log("BirdCol");
            wayNameStr = new StringBuilder("BirdWay");
            wayNameStr.Append(nextIdx);

            if (col.name.Equals(wayNameStr.ToString()))
            {
                int ran = Random.Range(1, points.Length);

                if (nextIdx != ran)
                {
                    nextIdx = ran;
                }
                else
                {
                    b_suffle = true;
                }

            }
        }
    }

    IEnumerator AnimalAnimation(int wayNum)
    {
        switch(animal)
        {
            case Animal.BEAR:
                if (wayNum == 1 || wayNum == 2)
                {
                    yield return new WaitForSeconds(8.3f);
                    b_StopAnimation = true;
                    animation.Play("Arm_bear|idle_1");
                    yield return new WaitForSeconds(1.0f);
                    animation.Play("Arm_bear|eat");
                    yield return new WaitForSeconds(3.0f);
                    b_StopAnimation = false;
                    animation.Play("Arm_bear|walk._1");
                    yield return new WaitForSeconds(4.0f);
                    animation.Play("Arm_bear|walk_search");
                }
                else if(wayNum == 3)
                {
                    yield return new WaitForSeconds(7.0f);
                    b_StopAnimation = true;
                    animation.Play("Arm_bear|idle_1");
                    yield return new WaitForSeconds(1.0f);
                    animation.Play("Arm_bear|up");
                    yield return new WaitForSeconds(1.2f);
                    animation.Play("Arm_bear|idle_3");
                    yield return new WaitForSeconds(2.2f);
                    animation.Play("Arm_bear|attack_4");
                    yield return new WaitForSeconds(2.0f);
                    animation.Play("Arm_bear|idle_3");
                    yield return new WaitForSeconds(0.5f);
                    animation.Play("Arm_bear|down");
                    yield return new WaitForSeconds(0.5f);
                    b_StopAnimation = false;
                    animation.Play("Arm_bear|walk._1");
                }
                else if(wayNum == 4)
                {

                }
                else if (wayNum == 5)
                {
                    yield return new WaitForSeconds(5.0f);
                    b_RunAnimation = true;
                    animation.Play("Arm_bear|run");
                }
                break;

            case Animal.WOLF:
                int moveWolf = Random.Range(0, 2);

                if (wayNum == 1)
                {
                    yield return new WaitForSeconds(1.0f);
                    b_RunAnimation = true;
                    animation.Play("Armature_wolf|run");
                }
                else if(wayNum == 2)
                {
                    yield return new WaitForSeconds(10.0f);
                    b_RunAnimation = true;
                    animation.Play("Armature_wolf|run");
                    yield return new WaitForSeconds(5.5f);
                    b_RunAnimation = false;
                    animation.Play("Armature_wolf|walk_1");
                    yield return new WaitForSeconds(1.0f);
                    b_StopAnimation = true;
                    animation.Play("Armature_wolf|Idle_1");
                    yield return new WaitForSeconds(1.5f);
                    animation.Play("Armature_wolf|jump");
                    yield return new WaitForSeconds(1.5f);
                    b_StopAnimation = false;
                    animation.Play("Armature_wolf|walk_1");
                }
                else if (wayNum == 3)
                {
                    yield return new WaitForSeconds(10.0f);
                    b_StopAnimation = true;
                    animation.Play("Armature_wolf|Idle_2");
                    yield return new WaitForSeconds(4.0f);
                    animation.Play("Armature_wolf|Howl");
                    yield return new WaitForSeconds(3.0f);
                    b_StopAnimation = false;
                    b_RunAnimation = true;
                    animation.Play("Armature_wolf|run");
                }
                else if (wayNum == 4)
                {
                    if(moveWolf == 0)
                    {
                        yield return new WaitForSeconds(10.0f);
                        b_RunAnimation = true;
                        animation.Play("Armature_wolf|run");
                    }
                }
                break;

            case Animal.FOX:
                int moveFox = Random.Range(0, 2);

                if (wayNum == 1)
                {
                }
                else if (wayNum == 2)
                {
                }
                else if (wayNum == 3)
                {
                }
                else if (wayNum == 4)
                {
                    if (moveFox == 0)
                    {
                        yield return new WaitForSeconds(10.0f);
                        b_RunAnimation = true;
                        animation.Play("Arm_fox|run");
                    }
                }
                break;

            case Animal.STAG:
                if (wayNum == 1)
                {
                }
                else if (wayNum == 2)
                {
                }
                else if (wayNum == 3)
                {
                }
                else if (wayNum == 4)
                {
                }
                break;

            case Animal.BOAR:
                if (wayNum == 1)
                {
                }
                else if (wayNum == 2)
                {
                }
                else if (wayNum == 3)
                {
                }
                else if (wayNum == 4)
                {
                }
                break;
        }

        
    }
}
