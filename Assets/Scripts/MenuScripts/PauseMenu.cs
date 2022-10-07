using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour
{
    public GameObject menuUI;
    private AudioSource source;
    public AudioClip splat;
    public static bool paused = false;

    void Start()
    {
        menuUI.gameObject.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
        menuUI.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("resume!");
        paused = false;
        Time.timeScale = 1f;
        menuUI.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        paused = false;
        Debug.Log("mainmenu!");
        SceneManager.LoadScene("MainMenu");
        source.PlayOneShot(splat);
    }

    public void QuitGame()
    {
        paused = false;
        Debug.Log("Quit!");
        source.PlayOneShot(splat);
        Application.Quit();
    }
}
