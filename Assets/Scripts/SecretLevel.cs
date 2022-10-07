using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretLevel : MonoBehaviour
{
    public GameObject secretPlayer;
    public GameObject secretCamera;
    public GameObject normalPlayer;
    public GameObject normalCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (YurikaPlayer.isInSecretLevel)
        {
            normalPlayer.SetActive(false);
            normalCamera.SetActive(false);
            secretPlayer.SetActive(true);
            secretCamera.SetActive(true);

            
            
        }
        else
        {
            secretPlayer.SetActive(false);
            secretCamera.SetActive(false);
            normalPlayer.SetActive(true);
            normalCamera.SetActive(true);
        }
    }
}
