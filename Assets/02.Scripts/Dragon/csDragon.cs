using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class csDragon : MonoBehaviour
{
    public float speed = 1.0f;
    public float damping = 3.0f;

    StringBuilder wayNameStr;
    StringBuilder sbWay;

    private Transform tr;
    private Transform[] points;

    private int nextIdx = 1;

    void OnEnable()
    {
        nextIdx = 1;

        tr = this.GetComponent<Transform>();

        sbWay = new StringBuilder("DragonWayGroup");
        int wayNum = Random.Range(1, 12);
        sbWay.Append(wayNum);

        points = GameObject.Find(sbWay.ToString()).GetComponentsInChildren<Transform>();
        
        StringBuilder sb = new StringBuilder(sbWay.ToString());
        sb.Append("DragonWay");
        sb.Append(nextIdx);

        tr.position = GameObject.Find(sb.ToString()).transform.position;
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
        wayNameStr = new StringBuilder(sbWay.ToString());
        wayNameStr.Append("DragonWay");
        wayNameStr.Append(nextIdx);

        if (col.CompareTag("DeathZone"))
        {
            csPooledDragon.instance.poolObjs_Dragon.Remove(this.gameObject);
            csPooledDragon.instance.poolObjs_Dragon.Add(this.gameObject);
            this.transform.SetAsLastSibling();

            this.transform.gameObject.SetActive(false);

            csDragonManager.instance.dragonCnt -= 1;
        }
        else if (col.name.Equals(wayNameStr.ToString()))
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
    }
}
