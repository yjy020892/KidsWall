using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum Dinosaurs
{
    NONE,
    T_REX,
    VELOCIRAPTOR,
    STEGOSAURUS,
    TRICERATOPS,
    PTERANODON,
}

public class csDinosaur : MonoBehaviour
{
    public Dinosaurs dinosaurs;

    string nameStr;

    [HideInInspector]public int wayNum;

    public float speed = 1.0f;
    public float damping = 3.0f;
    public float gravity = 5.0f;

    StringBuilder sbWay;
    StringBuilder wayNameStr;

    [HideInInspector] public Animator animator;

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
        b_RunAnimation = false;
        b_StopAnimation = false;

        animator = this.GetComponent<Animator>();

        nextIdx = 1;

        tr = this.GetComponent<Transform>();

        moveDir = Vector3.zero;
        controller = this.GetComponent<CharacterController>();
        
        switch (dinosaurs)
        {
            case Dinosaurs.T_REX:
                if(csDinosaurManager.instance.b_T_RexWayNum1)
                {
                    //Debug.Log("false");
                    wayNum = Random.Range(2, 4);
                }
                else
                {
                    //Debug.Log("true");
                    wayNum = Random.Range(1, 4);
                }

                if(wayNum == 1)
                {
                    csDinosaurManager.instance.b_T_RexWayNum1 = true;
                }

                sbWay = new StringBuilder("T_RexWayGroup");

                sbWay.Append(wayNum);
                points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();
                tr.position = GameObject.Find(sbWay.ToString()).transform.position;
                StartCoroutine(DinosaurAnimation(wayNum));
                break;

            case Dinosaurs.VELOCIRAPTOR:
                wayNum = Random.Range(1, 4);

                sbWay = new StringBuilder("VelociraptorWayGroup");

                sbWay.Append(wayNum);
                points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();
                tr.position = GameObject.Find(sbWay.ToString()).transform.position;
                StartCoroutine(DinosaurAnimation(wayNum));
                break;

            case Dinosaurs.STEGOSAURUS:
                wayNum = Random.Range(1, 3);

                sbWay = new StringBuilder("StegosaurusWayGroup");

                sbWay.Append(wayNum);
                points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();
                tr.position = GameObject.Find(sbWay.ToString()).transform.position;
                StartCoroutine(DinosaurAnimation(wayNum));
                break;

            case Dinosaurs.TRICERATOPS:
                wayNum = Random.Range(1, 4);

                sbWay = new StringBuilder("TriceratopsWayGroup");

                sbWay.Append(wayNum);
                points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();
                tr.position = GameObject.Find(sbWay.ToString()).transform.position;
                StartCoroutine(DinosaurAnimation(wayNum));
                break;

            case Dinosaurs.PTERANODON:
                wayNum = Random.Range(1, 4);

                sbWay = new StringBuilder("PteranodonWayGroup");

                sbWay.Append(wayNum);
                points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();
                tr.position = GameObject.Find(sbWay.ToString()).transform.position;
                StartCoroutine(DinosaurAnimation(wayNum));
                break;
        }
        
        if (dinosaurs == Dinosaurs.T_REX)
        {
            
        }
        //else if (nameStr.Equals("Crow"))
        //{
        //    points = GameObject.Find("CrowWayGroup").GetComponentsInChildren<Transform>();

        //    nextIdx = Random.Range(1, points.Length);

        //    StringBuilder sb = new StringBuilder("CrowWay");
        //    sb.Append(nextIdx);

        //    tr.position = GameObject.Find(sb.ToString()).transform.position;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (nameStr.Equals("Crow"))
        //{
        //    if (b_suffle)
        //    {
        //        b_suffle = false;

        //        int ran = Random.Range(1, points.Length);

        //        if (nextIdx != ran)
        //        {
        //            nextIdx = ran;
        //        }
        //        else
        //        {
        //            b_suffle = true;
        //        }
        //    }
        //}

        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (controller != null && controller.isGrounded)
        {
            switch(dinosaurs)
            {
                case Dinosaurs.T_REX:
                    if(b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if(b_RunAnimation)
                        {
                            speed = 7.0f;
                        }
                        else
                        {
                            speed = 3.0f;
                        }
                    }
                    break;

                case Dinosaurs.VELOCIRAPTOR:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 6.0f;
                        }
                        else
                        {
                            speed = 2.5f;
                        }
                    }
                    break;

                case Dinosaurs.STEGOSAURUS:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 6.0f;
                        }
                        else
                        {
                            speed = 2.5f;
                        }
                    }
                    break;

                case Dinosaurs.TRICERATOPS:
                    if (b_StopAnimation)
                    {
                        speed = 0.0f;
                    }
                    else
                    {
                        if (b_RunAnimation)
                        {
                            speed = 6.0f;
                        }
                        else
                        {
                            speed = 2.5f;
                        }
                    }
                    break;
                    
            }

            controller.Move(moveDir);
        }
        else if (controller != null && !controller.isGrounded)
        {
            if (dinosaurs != Dinosaurs.PTERANODON)
            {
                moveDir.y -= gravity * Time.deltaTime;

                controller.Move(moveDir);
            }
            else if(dinosaurs == Dinosaurs.PTERANODON)
            {
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
                        speed = 2.5f;
                    }
                }
            }
        }
        //moveDir = transform.TransformDirection(moveDir);

        Vector3 direction = points[nextIdx].position - tr.position;

        Quaternion rot = Quaternion.LookRotation(direction);

        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        switch(dinosaurs)
        {
            case Dinosaurs.T_REX:
                wayNameStr = new StringBuilder("T_RexWay");
                wayNameStr.Append(nextIdx);
                break;

            case Dinosaurs.VELOCIRAPTOR:
                wayNameStr = new StringBuilder("VelociraptorWay");
                wayNameStr.Append(nextIdx);
                break;

            case Dinosaurs.STEGOSAURUS:
                wayNameStr = new StringBuilder("StegosaurusWay");
                wayNameStr.Append(nextIdx);
                break;

            case Dinosaurs.TRICERATOPS:
                wayNameStr = new StringBuilder("TriceratopsWay");
                wayNameStr.Append(nextIdx);
                break;

            case Dinosaurs.PTERANODON:
                wayNameStr = new StringBuilder("PteranodonWay");
                wayNameStr.Append(nextIdx);
                break;
        }

        if (col.CompareTag("DeathZone"))
        {
            if(csDinosaurManager.instance.b_T_RexWayNum1 && wayNum == 1)
            {
                csDinosaurManager.instance.b_T_RexWayNum1 = false;
            }

            csDinosaurManager.instance.dinosaurCnt -= 1;

            if(dinosaurs == Dinosaurs.T_REX)
            {
                csDinosaurManager.instance.t_RexCnt -= 1;
            }

            csPooledDinosaur.instance.poolObjs_Dinosaur.Remove(this.gameObject);
            csPooledDinosaur.instance.poolObjs_Dinosaur.Add(this.gameObject);
            this.transform.SetAsLastSibling();

            this.transform.gameObject.SetActive(false);
        }
        else if (col.name.Equals(wayNameStr.ToString()))
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
        //else if (nameStr.Equals("Crow"))
        //{
        //    if (nextIdx == preNextIdx)
        //    {
        //        return;
        //    }
        //    wayNameStr = new StringBuilder("CrowWay");
        //    wayNameStr.Append(nextIdx);

        //    if (col.name.Equals(wayNameStr.ToString()))
        //    {
        //        int ran = Random.Range(1, points.Length);

        //        if (nextIdx != ran)
        //        {
        //            nextIdx = ran;
        //        }
        //        else
        //        {
        //            b_suffle = true;
        //        }

        //    }
        //}
    }

    IEnumerator DinosaurAnimation(int wayNum)
    {
        switch(dinosaurs)
        {
            case Dinosaurs.T_REX:
                if(wayNum == 1)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    yield return new WaitForSeconds(5.4f);
                    b_StopAnimation = true;
                    animator.SetBool("Walk", false);
                    animator.SetBool("Eat", true);
                    yield return new WaitForSeconds(12.0f);
                    b_StopAnimation = false;
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Eat", false);
                }
                else if(wayNum == 2)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    yield return new WaitForSeconds(5.4f);
                    b_StopAnimation = true;
                    animator.SetBool("Walk", false);
                    animator.SetBool("Roar", true);
                    yield return new WaitForSeconds(3.6f);
                    b_StopAnimation = false;
                    b_RunAnimation = true;
                    animator.SetBool("Roar", false);
                    animator.SetBool("Run", true);
                }
                else if (wayNum == 3)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                }
                break;

            case Dinosaurs.VELOCIRAPTOR:
                if (wayNum == 1)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    yield return new WaitForSeconds(3.0f);
                    b_StopAnimation = true;
                    animator.SetBool("Sight", true);
                    animator.SetBool("Walk", false);
                    yield return new WaitForSeconds(2.0f);
                    b_StopAnimation = false;
                    b_RunAnimation = true;
                    animator.SetBool("Sight", false);
                    animator.SetBool("Run", true);
                }
                else if (wayNum == 2 || wayNum == 3)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                }
                break;

            case Dinosaurs.STEGOSAURUS:
                if (wayNum == 1)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                }
                else if (wayNum == 2 || wayNum == 3)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                }
                break;

            case Dinosaurs.TRICERATOPS:
                if (wayNum == 1)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                }
                else if (wayNum == 2 || wayNum == 3)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                }
                break;

            case Dinosaurs.PTERANODON:
                if (wayNum == 1)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Fly", true);
                    animator.SetBool("Idle", false);
                }
                else if (wayNum == 2 || wayNum == 3)
                {
                    b_RunAnimation = true;
                    animator.SetBool("Fly", true);
                    animator.SetBool("Idle", false);
                }
                break;
        }
    }
}
