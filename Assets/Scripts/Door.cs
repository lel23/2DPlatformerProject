using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private string level;
    public bool finalLevel;

    private AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();

        if (finalLevel) level = "MainMenu";
        else
        {
            level = SceneManager.GetActiveScene().name;
            float levelNumber = float.Parse(level.Substring(5)) + 1;
            level = "Level" + levelNumber;
        }

        if (gameObject.tag.Equals("EnterSecret"))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            LPlayer player = other.GetComponent<LPlayer>();
            if (gameObject.name.Equals("Door"))
            {
                player.isDead = true;
                StartCoroutine(NextLevel());
            }
            else if (gameObject.name.Equals("SecretLevelDoor"))
            {
                GameObject entrance = GameObject.Find("EnterSecret");
                StartCoroutine(TurnOffCollision(entrance.GetComponent<Collider2D>()));
                Camera.main.backgroundColor = Color.black;

                player.InOrOutSecret(entrance.transform.position);
                player.transform.position = entrance.transform.position;

                GameObject.Find("SecretControls").GetComponent<Canvas>().sortingOrder = 1; // FOR MOBILE
                GameObject.Find("Fixed Joystick").GetComponent<Image>().color = Color.white;
                GameObject.Find("Handle").GetComponent<Image>().color = Color.white;
            }
            else if (gameObject.name.Equals("EnterSecret"))
            {
                GameObject secretEntrace = GameObject.Find("SecretLevelDoor");
                StartCoroutine(TurnOffCollision(secretEntrace.GetComponent<Collider2D>()));
                Camera.main.backgroundColor = Color.white;

                GameObject.Find("SecretControls").GetComponent<Canvas>().sortingOrder = -1; // FOR MOBILE
                GameObject.Find("Fixed Joystick").GetComponent<Image>().color = Color.black;
                GameObject.Find("Handle").GetComponent<Image>().color = Color.black;

                player.InOrOutSecret(secretEntrace.transform.position);
                player.transform.position = secretEntrace.transform.position;

            }
            else if (gameObject.name.Equals("ExitSecret"))
            {
                player.InOrOutSecret(transform.position);
                player.PutOnHat();
                player.transform.position = GameObject.Find("SecretLevelDoor").transform.position;
                Camera.main.backgroundColor = Color.white;

                GameObject.Find("SecretControls").GetComponent<Canvas>().sortingOrder = 1; // FOR MOBILE
                GameObject.Find("Fixed Joystick").GetComponent<Image>().color = Color.black;
                GameObject.Find("Handle").GetComponent<Image>().color = Color.black;

                GameObject secretEntrace = GameObject.Find("SecretLevelDoor");
                StartCoroutine(TurnOffCollision(secretEntrace.GetComponent<Collider2D>()));
            }
        }
    }

    IEnumerator TurnOffCollision(Collider2D door)
    {
        door.enabled = false;
        yield return new WaitForSeconds(2);
        door.enabled = true;

    }

    IEnumerator NextLevel()
    {
        source.Play();
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
    }
}
