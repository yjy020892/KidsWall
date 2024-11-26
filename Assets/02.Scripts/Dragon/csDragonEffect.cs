using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDragonEffect : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2.5f)
        {
            timer = 0.0f;

            csPooledDragonEffect.instance.poolObjs_DragonEffect.Remove(this.gameObject);
            csPooledDragonEffect.instance.poolObjs_DragonEffect.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
