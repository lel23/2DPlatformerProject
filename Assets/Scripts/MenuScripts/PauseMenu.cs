using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour
{
    public GameObject menuUI;
    bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) Resume();
            else Pause();
        }
    }
    void Pause()
    {
        Debug.Log("pause!");
        paused = true;
        Time.timeScale = 0.0f;
        menuUI.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
    }

    public void Resume()
    {
        Debug.Log("resume!");
        paused = false;
        Time.timeScale = 1f;
        menuUI.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000, 0);
    }

    public void MainMenu()
    {
        Resume();
        Debug.Log("mainmenu!");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Resume();
        Debug.Log("Quit!");
        Application.Quit();
    }
}
