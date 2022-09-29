using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private string level;
    public bool finalLevel;

    private AudioSource source;
    public AudioClip doorSound;

    void Start()
    {
        source = GetComponent<AudioSource>();

        // splicing level name to automatically pull next level
        level = SceneManager.GetActiveScene().name;

        // temporary measure for a final level.
        // would want to load end scene or back to main menu
        if (finalLevel) level = "Level1";
        else
        {
            float levelNumber = float.Parse(level.Substring(5)) + 1;
            level = "Level" + levelNumber;
        }

        Debug.Log(level);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            source.clip = doorSound;
            source.Play();
            SceneManager.LoadScene(level);
        }
    }
}
