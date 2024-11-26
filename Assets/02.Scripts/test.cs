using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(1024, 768, true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Screen.width / 2);
        Debug.Log(Screen.height / 2);
    }
}
