using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class csMoveMonster : MonoBehaviour
{
    public float speed = 1.0f;
    public float damping = 3.0f;

    private Transform tr;
    private Transform[] points;

    private int nextIdx = 1;
    [HideInInspector]public int randomWay;

    void OnEnable()
    {
        nextIdx = 1;

        randomWay = Random.Range(0, 8);

        tr = GetComponent<Transform>();

        StringBuilder sb = new StringBuilder("WayPoint");
        sb.Append(randomWay.ToString());

        //Debug.Log(sb);

        points = GameObject.Find(sb.ToString()).GetComponentsInChildren<Transform>();
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveWayPoint();
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
        StringBuilder sb = new StringBuilder("WAY");
        sb.Append(randomWay.ToString());

        if (col.CompareTag(sb.ToString()))
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
    }
}
