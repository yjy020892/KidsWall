using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBubble : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.2f)
        {
            timer = 0.0f;

            csPooledBubble.instance.poolObjs_Bubble.Remove(this.gameObject);
            csPooledBubble.instance.poolObjs_Bubble.Add(this.gameObject);

            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }
    }
}
