using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPooledDinosaur : MonoBehaviour
{
    public static csPooledDinosaur instance;

    public GameObject[] poolObj_Dinosaur;
    public GameObject group_Dinosaur;
    public int poolAmount_Dinosaur;
    [HideInInspector] public List<GameObject> poolObjs_Dinosaur = new List<GameObject>();

    public Transform spawnDinosaurPoint;

    void Awake()
    {
        if (csPooledDinosaur.instance == null)
        {
            csPooledDinosaur.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount_Dinosaur; i++)
        {
            int ran = Random.Range(0, poolObj_Dinosaur.Length);

            GameObject obj_Dinosaur = (GameObject)Instantiate(poolObj_Dinosaur[ran], spawnDinosaurPoint.position, Quaternion.identity);

            switch(ran)
            {
                case 0:
                    obj_Dinosaur.name = "T_REX";
                    obj_Dinosaur.GetComponent<csDinosaur>().dinosaurs = Dinosaurs.T_REX;
                    break;

                case 1:
                    obj_Dinosaur.name = "VELOCIRAPTOR";
                    obj_Dinosaur.GetComponent<csDinosaur>().dinosaurs = Dinosaurs.VELOCIRAPTOR;
                    break;

                case 2:
                    obj_Dinosaur.name = "STEGOSAURUS1";
                    obj_Dinosaur.GetComponent<csDinosaur>().dinosaurs = Dinosaurs.STEGOSAURUS;
                    break;

                case 3:
                    obj_Dinosaur.name = "STEGOSAURUS2";
                    obj_Dinosaur.GetComponent<csDinosaur>().dinosaurs = Dinosaurs.STEGOSAURUS;
                    break;

                case 4:
                    obj_Dinosaur.name = "TRICERATOPS";
                    obj_Dinosaur.GetComponent<csDinosaur>().dinosaurs = Dinosaurs.TRICERATOPS;
                    break;

                case 5:
                    obj_Dinosaur.name = "PTERANODON";
                    obj_Dinosaur.GetComponent<csDinosaur>().dinosaurs = Dinosaurs.PTERANODON;
                    break;
            }
            
            obj_Dinosaur.transform.parent = group_Dinosaur.transform;
            obj_Dinosaur.SetActive(false);
            poolObjs_Dinosaur.Add(obj_Dinosaur);
        }
    }

    public GameObject GetPooledObject_Dinosaur()
    {
        for (int i = 0; i < poolObjs_Dinosaur.Count; i++)
        {
            if (!poolObjs_Dinosaur[i].activeInHierarchy)
            {
                return poolObjs_Dinosaur[i];
            }
        }

        return null;
    }
}
