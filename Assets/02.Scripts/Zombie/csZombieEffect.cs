using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csZombieEffect : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            timer = 0.0f;

            csPooledZombieEffect.instance.poolObjs_ZombieEffect.Remove(this.gameObject);
            csPooledZombieEffect.instance.poolObjs_ZombieEffect.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
