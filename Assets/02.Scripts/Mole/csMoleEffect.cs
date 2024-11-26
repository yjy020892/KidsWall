using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMoleEffect : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            timer = 0.0f;

            csPooledMoleEffect.instance.poolObjs_MoleEffect.Remove(this.gameObject);
            csPooledMoleEffect.instance.poolObjs_MoleEffect.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
