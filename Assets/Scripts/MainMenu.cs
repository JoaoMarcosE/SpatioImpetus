using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.SetInt("lastSceneImage", 0);
        GameObject canvas = GameObject.Find("Canvas");
        Transform[] trs = canvas.GetComponentsInChildren<Transform>(true);

        GameObject firstScene = null;
        foreach (Transform t in trs)
        {
            if (t.name == "FirstScene")
            {
                firstScene = t.gameObject;
            }
        }

        firstScene.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}