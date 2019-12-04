using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastSceneIndex"));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
