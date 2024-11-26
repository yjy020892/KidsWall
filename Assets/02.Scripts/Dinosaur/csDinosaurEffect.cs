using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDinosaurEffect : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            timer = 0.0f;

            csPooledDinosaurEffect.instance.poolObjs_DinosaurEffect.Remove(this.gameObject);
            csPooledDinosaurEffect.instance.poolObjs_DinosaurEffect.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
