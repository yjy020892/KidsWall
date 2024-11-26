using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

public class MMFManager : MonoBehaviour
{
    [DllImport("MMF_Manager")]
    public static extern bool OpenMemFile(char[] strFilePath, int nBufSize);
    [DllImport("MMF_Manager")]
    public static extern bool ReadMemFile(byte[] lpBits, int nLen);
    [DllImport("MMF_Manager")]
    public static extern void ReleaseMemFile();

    public csMainManager mainManager;

    //public delegate void ReadBytes(byte[] bytes);
    //public event ReadBytes readBytes;
    
    private byte[] bytes;
    public string fileName = string.Empty;
    public bool isOpen = false;
    public bool isRead = false;
    public bool isWrite = false;
    
    //0 캐치 안된거 
    //1 캐치된 부분
    private void Start()
    {
        fileName = "ORIGINAL";
        bytes = new byte[1024 * 768];


        ReleaseMemFile();
        isOpen = OpenMemFile(fileName.ToCharArray(0, fileName.Length), bytes.Length);
    }

    private void write()
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            if (bytes.Length / 2 > i)
                bytes[i] = 0;
            else
                bytes[i] = 255;
        }

        //if (classManager.mainManager.contentType == MainManager.ContentType.Floor)
        //{
        //    if (classManager.spacShipManager != null)
        //        classManager.spacShipManager.CheckBytes(bytes);
        //}
        //else if (classManager.mainManager.contentType == MainManager.ContentType.Slider)
        //{
        //    if (classManager.balloonManager != null)
        //        classManager.balloonManager.CheckBytes(bytes);
        //}
    }

    private void Update()
    {
        if (!isOpen)
        {
            isOpen = OpenMemFile(fileName.ToCharArray(0, fileName.Length), bytes.Length);
        }
        else
        {
            isRead = ReadMemFile(bytes, bytes.Length);
            if (isRead)
            {
                checkItem();
            }
            //Test용 가상 데이터
            //write();
        }
    }

    private void checkItem()
    {
        //읽어온 바이트 데이터 델리게이트 이벤트.
        //if (readBytes != null)
        //    readBytes(bytes);

        switch (mainManager.wallContents)
        {
            case WallContents.HALLOWEEN:
                csHalloweenManager.instance.CheckBytes(bytes);
                break;

            case WallContents.MOLE:
                GameManager.instance.CheckBytes(bytes);
                break;

            case WallContents.SPACE:
                csSpaceManager.instance.CheckBytes(bytes);
                break;

            case WallContents.CAVE:
                csCaveManager.instance.CheckBytes(bytes);
                break;

            case WallContents.SEA:
                csSeaManager.instance.CheckBytes(bytes);
                break;

            case WallContents.FOREST:
                csForestManager.instance.CheckBytes(bytes);
                break;

            case WallContents.DRAGON:
                csDragonManager.instance.CheckBytes(bytes);
                break;

            case WallContents.BUTTERFLY:
                csButterflyManager.instance.CheckBytes(bytes);
                break;

            case WallContents.ZOMBIE:
                csZombieManager.instance.CheckBytes(bytes);
                break;

            case WallContents.DINOSAUR:
                csDinosaurManager.instance.CheckBytes(bytes);
                break;

            default:
                break;
        }
    }




 


}


