using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum Fish
{
    NONE,
    FISH,
    WHALE,
    DOLPHIN,
    OCTOPUS,
    SEATURTLE,
}

public class csFish : MonoBehaviour
{
    public Fish fish = Fish.NONE;

    public float speed = 1.0f;
    public float damping = 3.0f;

    StringBuilder wayNameStr;

    private Transform tr;
    private Transform[] points;

    private int nextIdx = 1;
    private int preNextIdx = 0;

    private bool b_suffle = false;

    void OnEnable()
    {
        tr = GetComponent<Transform>();
        
        switch (fish)
        {
            case Fish.FISH:
                points = GameObject.Find("FishWayGroup").GetComponentsInChildren<Transform>();

                nextIdx = Random.Range(1, points.Length);

                StringBuilder sb1 = new StringBuilder("FishWay");
                sb1.Append(nextIdx);

                tr.position = GameObject.Find(sb1.ToString()).transform.position;
                break;

            case Fish.WHALE:
                points = GameObject.Find("WhaleWayGroup").GetComponentsInChildren<Transform>();

                nextIdx = Random.Range(1, points.Length);

                StringBuilder sb2 = new StringBuilder("WhaleWay");
                sb2.Append(nextIdx);

                tr.position = GameObject.Find(sb2.ToString()).transform.position;
                break;

            case Fish.DOLPHIN:
                points = GameObject.Find("DolphinWayGroup").GetComponentsInChildren<Transform>();

                nextIdx = Random.Range(1, points.Length);

                StringBuilder sb3 = new StringBuilder("DolphinWay");
                sb3.Append(nextIdx);

                tr.position = GameObject.Find(sb3.ToString()).transform.position;
                break;

            case Fish.OCTOPUS:
                points = GameObject.Find("OctopusWayGroup").GetComponentsInChildren<Transform>();

                nextIdx = Random.Range(1, points.Length);

                StringBuilder sb4 = new StringBuilder("OctopusWay");
                sb4.Append(nextIdx);

                tr.position = GameObject.Find(sb4.ToString()).transform.position;
                break;

            case Fish.SEATURTLE:
                points = GameObject.Find("SeaTurtleWayGroup").GetComponentsInChildren<Transform>();

                nextIdx = Random.Range(1, points.Length);

                StringBuilder sb5 = new StringBuilder("SeaTurtleWay");
                sb5.Append(nextIdx);

                tr.position = GameObject.Find(sb5.ToString()).transform.position;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveWayPoint();

        if(b_suffle)
        {
            b_suffle = false;

            int ran = Random.Range(1, points.Length);

            if(nextIdx != ran)
            {
                nextIdx = ran;
            }
            else
            {
                b_suffle = true;
            }
        }
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
        if (nextIdx == preNextIdx)
        {
            return;
        }

        switch (fish)
        {
            case Fish.FISH:
                wayNameStr = new StringBuilder("FishWay");
                wayNameStr.Append(nextIdx);
                break;

            case Fish.WHALE:
                wayNameStr = new StringBuilder("WhaleWay");
                wayNameStr.Append(nextIdx);
                break;

            case Fish.DOLPHIN:
                wayNameStr = new StringBuilder("DolphinWay");
                wayNameStr.Append(nextIdx);
                break;

            case Fish.OCTOPUS:
                wayNameStr = new StringBuilder("OctopusWay");
                wayNameStr.Append(nextIdx);
                break;

            case Fish.SEATURTLE:
                wayNameStr = new StringBuilder("SeaTurtleWay");
                wayNameStr.Append(nextIdx);
                break;
        }

        if (col.name.Equals(wayNameStr.ToString()))
        {
            //nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
            int ran = Random.Range(1, points.Length);

            if (nextIdx != ran)
            {
                nextIdx = ran;
            }
            else
            {
                b_suffle = true;
                //if (nextIdx + 1 >= points.Length)
                //{
                //    nextIdx -= 1;
                //}
                //else if (nextIdx - 1 <= 0)
                //{
                //    nextIdx += 1;
                //}
                //else
                //{
                //    nextIdx = 1;
                //}
            }

        }
    }
}
