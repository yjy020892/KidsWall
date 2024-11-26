using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csHummer : MonoBehaviour
{
    Transform tr;
    
    private float _hummerSpeed;
    private float timer = 0.0f;

    private bool b_HitMole;

    void OnEnable()
    {
        b_HitMole = false;
        _hummerSpeed = -2.2f;

        tr = gameObject.transform;
        tr.position = new Vector3(tr.position.x, tr.position.y + 7f, tr.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(!b_HitMole)
        {
            tr.Translate(0, 0, _hummerSpeed);
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= 0.1f)
            {
                timer = 0.0f;

                csPooledHummer.instance.poolObjs_Hummer.Remove(this.gameObject);
                csPooledHummer.instance.poolObjs_Hummer.Add(this.gameObject);

                gameObject.transform.SetAsLastSibling();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag.Equals("Mole"))
        {
            b_HitMole = true;

            csSoundManager.instance.PlayMoleHitSound();

            col.transform.gameObject.GetComponent<csMole>().moleState = MoleState.DOWN;

            //col.transform.gameObject.GetComponent<csMoleHandler>().b_Check = false;

            GameObject obj = csPooledMoleEffect.instance.GetPooledObject_MoleEffect(col.transform);
            Transform objPosi = obj.transform;
            objPosi.position = new Vector3(objPosi.position.x, objPosi.position.y + 1.3f, objPosi.position.z);
            obj.SetActive(true);
        }
    }
}
