using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WallContents
{
    NONE,
    HALLOWEEN,
    MOLE,
    SPACE,
    CAVE,
    SEA,
    FOREST,
    DRAGON,
    BUTTERFLY,
    ZOMBIE,
    DINOSAUR,
}

public class csMainManager : MonoBehaviour
{
    public WallContents wallContents = WallContents.NONE;

    private float sceneTimer = 0.0f;
    private float sceneTimerVal = 60.0f;

    #region Halloween Data

    #endregion

    void Awake()
    {
        ReadSETTING();
        CheckSceneName();
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, true);
    }

    void CheckSceneName()
    {
        string sceneNameStr = SceneManager.GetActiveScene().name;

        switch (sceneNameStr)
        {
            case "Halloween_wall":
                wallContents = WallContents.HALLOWEEN;
                break;

            case "Mole_wall":
                wallContents = WallContents.MOLE;
                break;

            case "Space_wall":
                wallContents = WallContents.SPACE;
                break;

            case "Cave_wall":
                wallContents = WallContents.CAVE;
                break;

            case "Sea_wall":
                wallContents = WallContents.SEA;
                break;

            case "Forest_wall":
                wallContents = WallContents.FOREST;
                break;

            case "Dragon_wall":
                wallContents = WallContents.DRAGON;
                break;

            case "Butterfly_wall":
                wallContents = WallContents.BUTTERFLY;
                break;

            case "Zombie_wall":
                wallContents = WallContents.ZOMBIE;
                break;

            case "Dinosaur_wall":
                wallContents = WallContents.DINOSAUR;
                break;

                //default:
                //    wallContents = WallContents.NONE;
                //    break;
        }
    }

    void Update()
    {
        SceneRoutine();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    void SceneRoutine()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            int sceneNum = SceneManager.GetActiveScene().buildIndex;

            if(wallContents == WallContents.MOLE)
            {
                csMoleHandler.HitMole -= HummerController.instance.HitMoleKidVer;
            }

            if (sceneNum != 9)
            {
                SceneManager.LoadScene(sceneNum + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int sceneNum = SceneManager.GetActiveScene().buildIndex;

            if (wallContents == WallContents.MOLE)
            {
                csMoleHandler.HitMole -= HummerController.instance.HitMoleKidVer;
            }

            if (sceneNum != 0)
            {
                SceneManager.LoadScene(sceneNum - 1);
            }
            else
            {
                SceneManager.LoadScene(9);
            }
        }

        sceneTimer += Time.deltaTime;

        if (sceneTimer > sceneTimerVal)
        {
            switch (wallContents)
            {
                case WallContents.HALLOWEEN:
                    SceneManager.LoadScene(1);
                    break;

                case WallContents.MOLE:
                    csMoleHandler.HitMole -= HummerController.instance.HitMoleKidVer;
                    SceneManager.LoadScene(4);
                    break;

                case WallContents.SPACE:
                    SceneManager.LoadScene(0);
                    break;

                case WallContents.CAVE:
                    SceneManager.LoadScene(5);
                    break;

                case WallContents.SEA:
                    SceneManager.LoadScene(7);
                    break;

                case WallContents.FOREST:
                    SceneManager.LoadScene(9);
                    break;

                case WallContents.DRAGON:
                    SceneManager.LoadScene(2);
                    break;

                case WallContents.BUTTERFLY:
                    SceneManager.LoadScene(6);
                    break;

                case WallContents.ZOMBIE:
                    SceneManager.LoadScene(3);
                    break;

                case WallContents.DINOSAUR:
                    SceneManager.LoadScene(8);
                    break;
            }
        }
    }

    void ReadSETTING()
    {
        string configPath = "";
        configPath = "./DATA.CFG";

        System.IO.StreamReader sr = new System.IO.StreamReader(configPath);

        if (sr == null)
        {
            sceneTimerVal = 60.0f;
            //SerialPORT = "COM9";
            return;
        }

        string line = "";
        line = sr.ReadLine();


        while (line != null)
        {
            string[] tokens = line.Split(',');
            if (tokens[0] == "SCENE_TIMER")
            {
                sceneTimerVal = float.Parse(tokens[1]);
                //SerialPORT = tokens[1];
            }
            line = sr.ReadLine();
        }
        sr.Close();
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
