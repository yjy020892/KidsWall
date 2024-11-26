using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csForestEffect : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            timer = 0.0f;

            csPooledForestEffect.instance.poolObjs_ForestEffect.Remove(this.gameObject);
            csPooledForestEffect.instance.poolObjs_ForestEffect.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
