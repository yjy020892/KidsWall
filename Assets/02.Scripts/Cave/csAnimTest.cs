using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAnimTest : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.Play("Run");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.Play("Walk");
        }
    }
}
