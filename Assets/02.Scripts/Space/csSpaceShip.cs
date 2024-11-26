using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class csSpaceShip : MonoBehaviour
{
    Transform tr;
    private Transform[] points;

    private float speed = 1.0f;
    public float damping = 3.0f;

    private int preNextIdx = 0;
    private int nextIdx = 1;

    private bool b_Suffle = false;

    [HideInInspector] public int spaceShipWay;

    void OnEnable()
    {
        nextIdx = 1;

        spaceShipWay = Random.Range(0, 1);
        speed = Random.Range(3, 7);

        tr = GetComponent<Transform>();

        StringBuilder sb = new StringBuilder("WayPoint");
        sb.Append(spaceShipWay.ToString());

        points = GameObject.Find(sb.ToString()).GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        MoveWayPoint();

        if (b_Suffle)
        {
            b_Suffle = false;

            int ran = Random.Range(1, points.Length);

            if (nextIdx != ran)
            {
                nextIdx = ran;
            }
            else
            {
                b_Suffle = true;
            }
        }

        //if (nextIdx == preNextIdx)
        //{
        //    if (nextIdx + 1 >= points.Length)
        //    {
        //        nextIdx -= 1;
        //    }
        //    else if (nextIdx - 1 <= 0)
        //    {
        //        nextIdx += 1;
        //    }
        //    else
        //    {
        //        nextIdx += 1;
        //    }
        //}
    }

    void MoveWayPoint()
    {
        Vector3 direction = points[nextIdx].position - tr.position;

        Quaternion rot = Quaternion.LookRotation(direction);

        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if(nextIdx == preNextIdx)
        {
            return;
        }
        
        StringBuilder sb = new StringBuilder("WAY");
        sb.Append(spaceShipWay.ToString());

        //if (col.CompareTag(sb.ToString()))
        //{
        //    nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        //}

        int ran = Random.Range(1, points.Length);

        if (nextIdx != ran)
        {
            nextIdx = ran;
        }
        else
        {
            b_Suffle = true;
        }
    }

    
}
