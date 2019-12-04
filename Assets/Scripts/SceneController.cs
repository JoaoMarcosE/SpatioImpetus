using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int lastSceneImage;

    void Start()
    {
        lastSceneImage = PlayerPrefs.GetInt("lastSceneImage");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlayerPrefs.SetInt("lastSceneImage", ++lastSceneImage);
        }

        GameObject canvas = GameObject.Find("Canvas");
        Transform[] trs = canvas.GetComponentsInChildren<Transform>(true);

        GameObject firstScene = null;
        GameObject secondScene = null;
        GameObject thirdScene = null;

        foreach (Transform t in trs)
        {
            if (t.name == "FirstScene")
            {
                firstScene = t.gameObject;
            }
            else if (t.name == "SecondScene")
            {
                secondScene = t.gameObject;
            }
            else if (t.name == "ThirdScene")
            {
                thirdScene = t.gameObject;
            }
        }

        switch (lastSceneImage)
        {
            case 0:
                if (firstScene != null) firstScene.SetActive(true);
                if (secondScene != null) secondScene.SetActive(false);
                if (thirdScene != null) thirdScene.SetActive(false);
                break;
            case 1:
                if (secondScene != null) secondScene.SetActive(true);
                if (firstScene != null) firstScene.SetActive(false);
                if (thirdScene != null) thirdScene.SetActive(false);
                break;
            case 2:
                if (thirdScene != null) thirdScene.SetActive(true);
                if (firstScene != null) firstScene.SetActive(false);
                if (secondScene != null) secondScene.SetActive(false);
                break;
            default:
                PlayerPrefs.SetInt("lastSceneIndex", SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
