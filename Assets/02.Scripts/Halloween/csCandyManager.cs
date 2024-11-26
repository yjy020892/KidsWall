using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCandyManager : MonoBehaviour
{
    public GameObject batParticle;
    public GameObject starParticle;

    private float timer = 0.0f;
    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1.5f)
        {
            batParticle.SetActive(false);
            starParticle.SetActive(false);
        }

        //if(timer > 1.5f)
        //{
        //    starParticle.SetActive(false);
        //}

        if (timer > 3.0f)
        {
            timer = 0.0f;

            //Debug.Log(csPooledPaint.instance.poolObjs_Paint.Count);

            csPooledCandy.instance.poolObjs_Candy.Remove(csPooledCandy.instance.poolObjs_Candy[0]);

            //csPooledMonster.instance.poolObjs_Monster.Add(this.gameObject);

            //gameObject.transform.SetAsLastSibling();
            //gameObject.SetActive(false);

            Destroy(this.gameObject);
        }
    }
}
