using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Camera;
    private AudioSource source;
    public AudioClip splat;
    private static bool paused = false;


    void Start()
    {
        Canvas.gameObject.SetActive(false);
        source = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1f;
                Canvas.gameObject.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                Canvas.gameObject.SetActive(true);
                paused = true;
            }
        }
            // Application.Quit();
        }

    public void Resume()
    {
        Time.timeScale = 1f;
        Canvas.gameObject.SetActive(false);
        source.PlayOneShot(splat);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        source.PlayOneShot(splat);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        source.PlayOneShot(splat);
        Application.Quit();
    }
}
