using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public static void LoadGameScene()
    {
        PlayerData.ResetData();
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("Home");
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
