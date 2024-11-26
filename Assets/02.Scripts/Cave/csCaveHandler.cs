﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCaveHandler : MonoBehaviour
{
    public bool b_Check = false;

    void OnEnable()
    {
        b_Check = false;
    }

    public void CheckArea(byte[] bytes)
    {
        if (b_Check)
        {
            return;
        }

        //오브젝트 포지션을 화면상의 포지션으로 변경.
        Vector2 pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        bool isCheck = true;
        int pointX = 0;  //byte상의 포지션x
        int pointY = 0;  //byte상의 포지션y
        int width = 100; //체크할 오브젝트 넓이.
        int height = 100; //체크할 오브젝트 높이.
        int value = 0; //검사할 배열번호

        //오브젝트 피벗에서 UI상의 피벗으로 치환.
        pos = new Vector2(pos.x - (Screen.width / 2) - (width / 2), pos.y - (Screen.height / 2) - (height / 2));

        //오브젝트가 화면 밖을 완전 벗어나는지 체크.
        if (pos.x > ((Screen.width / -2) - (width / 2)) && pos.x < ((Screen.width / 2) + (width / 2)))
        {
            if (pos.y < ((Screen.height / 2) + (height / 2)) && pos.y > ((Screen.height / -2) - (height / 2)))
                isCheck = true;
        }

        if (isCheck)
        {
            pointX = ((int)pos.x + Screen.width / 2) - (width / 2);
            pointY = (((int)pos.y - Screen.height / 2) + (height / 2)) * (-1);

            if (pointX < 0)
            {
                //오브젝트가 0보다 작아질때 예외처리
                width = width + pointX;
                pointX = 0;
            }
            if (pointY < 0)
            {
                //오브젝트가 0보다 작아질때 예외처리
                height = height + pointY;
                pointY = 0;
            }
            if ((pointX + width) > 1024)
            {
                //오브젝트가 화면을 벗어나는 부분에 대한 With축소.
                width = width - ((pointX + width) - 1024);
            }
            if ((pointY + height) > 768)
            {
                //오브젝트가 화면을 벗어나는 부분에 대한 height축소.
                height = height - ((pointY + height) - 768);
            }


            //오브젝트 넓이를 바이트 배열의 넘버로 변환해 바이트 배열의 해당 값을 체크해 이벤트 발생.
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    value = ((pointY * 1024) + (i * 1024)) + (pointX + j);
                    if (bytes[value] > 0)
                    {
                        if (!b_Check)
                        {
                            Check();
                        }
                        //if (isPlay)
                        //    HitEvent();
                        break;
                    }

                }
            }
        }
    }

    private void Check()
    {
        b_Check = true;

        //csSpaceManager.instance.spaceShipCnt -= 1;

        if (gameObject.name.Equals("Spider"))
        {
            csSoundManager.instance.PlayCaveHitSound();

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(gameObject.transform);
            obj.SetActive(true);

            csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(gameObject);
            csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(gameObject);

            csMoveCaveMonster cs = gameObject.GetComponent<csMoveCaveMonster>();

            cs.animation.Play("Death");
            cs.b_SpiderAnimDeath = true;

            StartCoroutine(MonsterDeathMotion(gameObject));
        }
        else if (gameObject.name.Equals("Hornet"))
        {
            csSoundManager.instance.PlayCaveHitSound();

            csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(gameObject);
            csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(gameObject);

            csMoveCaveMonster cs = gameObject.GetComponent<csMoveCaveMonster>();

            cs.animator.SetBool("Die", true);
            cs.animator.SetBool("Fly", false);
            cs.animator.SetBool("Attack", false);
            cs.b_HornetAnimDeath = true;

            StartCoroutine(MonsterDeathMotion(gameObject));
        }
        else if (gameObject.name.Equals("Golem"))
        {
            csSoundManager.instance.PlayCaveHitSound();

            csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(gameObject);
            csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(gameObject);

            csMoveCaveMonster cs = gameObject.GetComponent<csMoveCaveMonster>();

            cs.animator.SetBool("Die", true);
            cs.animator.SetBool("Fly", false);
            cs.animator.SetBool("Attack", false);
            cs.animator.SetBool("Attack2", false);
            cs.b_GolemAnimDeath = true;

            StartCoroutine(MonsterDeathMotion(gameObject));
        }
        else if (gameObject.name.Equals("Bat"))
        {
            csSoundManager.instance.PlayCaveHitSound();

            csPooledCaveMonster.instance.poolObjs_CaveMonster.Remove(gameObject);
            csPooledCaveMonster.instance.poolObjs_CaveMonster.Add(gameObject);

            csMoveCaveMonster cs = gameObject.GetComponent<csMoveCaveMonster>();

            cs.animator.SetBool("Die", true);
            cs.animator.SetBool("Fly", false);
            cs.animator.SetBool("Attack", false);
            cs.animator.SetBool("Attack2", false);
            cs.b_HornetAnimDeath = true;

            StartCoroutine(MonsterDeathMotion(gameObject));
        }
    }

    IEnumerator MonsterDeathMotion(GameObject monster)
    {
        if (monster.name.Equals("Hornet"))
        {
            yield return new WaitForSeconds(0.6f);

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(monster.transform);
            obj.SetActive(true);
        }
        else if (monster.name.Equals("Golem"))
        {
            yield return new WaitForSeconds(1.0f);

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(monster.transform);
            obj.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            obj.transform.GetChild(0).localScale = new Vector3(2.0f, 2.0f, 2.0f);
            obj.SetActive(true);
        }
        else if (monster.name.Equals("Bat"))
        {
            yield return new WaitForSeconds(1.0f);

            GameObject obj = csPooledReSpawn.instance.GetPooledObject_ReSpawn(monster.transform);
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);

        monster.transform.SetAsLastSibling();
        monster.gameObject.SetActive(false);
    }
}
