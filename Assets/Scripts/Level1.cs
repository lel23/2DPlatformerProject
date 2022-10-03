using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public Image imgRightKey;
    public Image imgLeftKey;
    public Image imgUpKey;

    // Start is called before the first frame update
    void Start()
    {
        imgRightKey.color = new Color(1, 1, 1, 0);
        imgLeftKey.color = new Color(1, 1, 1, 0);
        imgUpKey.color = new Color(1, 1, 1, 0);
        StartCoroutine(FadeImage(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (YurikaPlayer.livesLost == 1)
        {
            StartCoroutine(FadeImage(false));
        }
    }

    IEnumerator FadeImage(bool isBeginning)
    {
        if (isBeginning)
        { for (float i = 0; i <= 3; i += Time.deltaTime)
            {
                imgRightKey.color = new Color(1, 1, 1, i);
                imgLeftKey.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
     
        else
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
