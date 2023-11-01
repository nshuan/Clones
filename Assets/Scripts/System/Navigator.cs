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

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
