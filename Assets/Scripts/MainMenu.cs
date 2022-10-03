using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelzero;
    public GameObject credits;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenCredits()
    {
    }
    public void CloseCredits()
    {
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
