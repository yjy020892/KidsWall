using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csExplosion : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2.0f)
        {
            timer = 0.0f;

            csPooledExplosion.instance.poolObjs_Explosion.Remove(this.gameObject);
            csPooledExplosion.instance.poolObjs_Explosion.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
