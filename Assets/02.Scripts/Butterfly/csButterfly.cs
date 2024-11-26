using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csButterfly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        csButterflyManager.instance.list_Butterfly.Add(this.gameObject);
        //Debug.Log(csButterflyManager.instance.list_Butterfly.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
