using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMole : MonoBehaviour
{
    public MoleState moleState;

    private float on_Ground_Timer = 0.0f;

    private float offsetPosiX;
    private float offsetPosiY;
    private float offsetPosiZ;

    void OnEnable()
    {
        csMoleManager.instance.list_Mole.Add(this.gameObject);

        //Debug.Log(csMoleManager.instance.list_Mole.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        moleState = MoleState.UNDER_GROUND;

        offsetPosiX = transform.position.x;
        offsetPosiY = transform.position.y;
        offsetPosiZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        switch(moleState)
        {
            case MoleState.UNDER_GROUND:
                
                break;

            case MoleState.UP:
                transform.Translate(0, 0.1f, 0);

                if (transform.position.y > -0.4f)
                {
                    transform.position = new Vector3(transform.position.x, -0.4f, transform.position.z);

                    moleState = MoleState.ON_GROUND;
                }
                break;

            case MoleState.ON_GROUND:
                on_Ground_Timer += Time.deltaTime;

                if(on_Ground_Timer > 2.0f)
                {
                    moleState = MoleState.DOWN;

                    on_Ground_Timer = 0.0f;
                }
                break;

            case MoleState.DOWN:
                transform.Translate(0, -0.1f, 0);

                if (transform.position.y < offsetPosiY)
                {
                    transform.position = new Vector3(offsetPosiX, offsetPosiY, offsetPosiZ);

                    moleState = MoleState.UNDER_GROUND;
                }
                break;

            
        }
    }
}
