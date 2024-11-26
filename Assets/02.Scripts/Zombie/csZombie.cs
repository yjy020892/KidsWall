using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class csZombie : MonoBehaviour
{
    string nameStr;

    public float speed = 1.0f;
    public float damping = 3.0f;
    public float gravity = 5.0f;

    StringBuilder wayNameStr;

    [HideInInspector] public Animator animator;

    private CharacterController controller;
    private Vector3 moveDir;

    private Transform tr;
    private Transform[] points;

    private int nextIdx = 1;
    private int preNextIdx = 0;

    private bool b_suffle = false;

    void OnEnable()
    {
        animator = this.GetComponent<Animator>();

        nextIdx = 1;

        tr = this.GetComponent<Transform>();

        nameStr = gameObject.name;

        if(nameStr.Equals("Zombie"))
        {
            moveDir = Vector3.zero;
            controller = this.GetComponent<CharacterController>();

            StringBuilder sbWay = new StringBuilder("ZombieWayGroup");
            int wayNum = Random.Range(1, 6);
            sbWay.Append(wayNum);

            points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();

            tr.position = GameObject.Find(sbWay.ToString()).transform.position;

            int ranAnim = Random.Range(0, 2);
            switch (ranAnim)
            {
                case 0:
                    speed = 1.0f;

                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    break;

                case 1:
                    speed = 2.0f;

                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    break;
            }
        }
        else if(nameStr.Equals("Crow"))
        {
            points = GameObject.Find("CrowWayGroup").GetComponentsInChildren<Transform>();

            nextIdx = Random.Range(1, points.Length);

            StringBuilder sb = new StringBuilder("CrowWay");
            sb.Append(nextIdx);

            tr.position = GameObject.Find(sb.ToString()).transform.position;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (nameStr.Equals("Crow"))
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

        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (controller != null && controller.isGrounded)
        {
            controller.Move(moveDir);
        }
        else if (controller != null && !controller.isGrounded)
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
        if(nameStr.Equals("Zombie"))
        {
            wayNameStr = new StringBuilder("ZombieWay");
            wayNameStr.Append(nextIdx);

            if (col.CompareTag("DeathZone"))
            {
                csZombieManager.instance.zombieCnt -= 1;

                csPooledZombie.instance.poolObjs_Zombie.Remove(this.gameObject);
                csPooledZombie.instance.poolObjs_Zombie.Add(this.gameObject);
                this.transform.SetAsLastSibling();

                this.transform.gameObject.SetActive(false);
            }
            else if (col.name.Equals(wayNameStr.ToString()))
            {
                nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
            }
        }
        else if(nameStr.Equals("Crow"))
        {
            if (nextIdx == preNextIdx)
            {
                return;
            }
            wayNameStr = new StringBuilder("CrowWay");
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
}
