using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // change to name maybe
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(2);
    }

    public void Level3()
    {
        SceneManager.LoadScene(3);
    }

    public void Level4()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
