using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csRespawn : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.9f)
        {
            timer = 0.0f;

            csPooledReSpawn.instance.poolObjs_ReSpawn.Remove(this.gameObject);
            csPooledReSpawn.instance.poolObjs_ReSpawn.Add(this.gameObject);

            this.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            this.transform.GetChild(0).localScale = new Vector3(1.0f, 1.0f, 1.0f);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
