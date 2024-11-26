using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCandyEffect : MonoBehaviour
{
    Rigidbody rb;
    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        int forceX = Random.Range(-130, 130);
        int forceY = Random.Range(100, 250);
        int forceZ = Random.Range(50, 130);

        rb.AddRelativeForce(new Vector3(forceX, forceY, forceZ));

        rb.transform.Rotate(new Vector3(forceX, forceY, forceZ));
    }
}
