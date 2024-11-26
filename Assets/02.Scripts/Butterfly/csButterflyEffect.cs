using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csButterflyEffect : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2.0f)
        {
            timer = 0.0f;

            csPooledButterflyEffect.instance.poolObjs_ButterflyEffect.Remove(this.gameObject);
            csPooledButterflyEffect.instance.poolObjs_ButterflyEffect.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
