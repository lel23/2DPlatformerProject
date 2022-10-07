using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public Image imgRightKey;
    public Image imgLeftKey;
    public Image imgUpKey;


    void Start()
    {
        imgRightKey.color = new Color(1, 1, 1, 0);
        imgLeftKey.color = new Color(1, 1, 1, 0);
        imgUpKey.color = new Color(1, 1, 1, 0);
        StartCoroutine(FadeImage(true));
    }

    void Update()
    {
        if (YurikaPlayer.livesLost == 1) // Once player dies, they learn how to jump
        {
            StartCoroutine(FadeImage(false));
        }
    }

    IEnumerator FadeImage(bool isBeginning)
    {
        if (isBeginning) // Fade in arrow key instructions
        { for (float i = 0; i <= 3; i += Time.deltaTime)
            {
                imgRightKey.color = new Color(1, 1, 1, i);
                imgLeftKey.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
     
        else // Fade in up key instruction
        {
            // loop over 1 second
            for (float i = 0; i <= 2; i += Time.deltaTime)
            {
                imgUpKey.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
