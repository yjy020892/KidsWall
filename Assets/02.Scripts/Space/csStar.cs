using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csStar : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        float ran = Random.Range(3.0f, 10.0f);

        yield return new WaitForSeconds(ran);

        this.gameObject.SetActive(false);
    }
}
