using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController1 : MonoBehaviour
{
    public GameObject jumpBtn;
    public GameObject firstArrow;
    public GameObject secondArrow;
    //public Image imgRightKey;
    //public Image imgLeftKey;
    //public Image imgUpKey;

    LPlayer player;

    void Start()
    {
        //imgRightKey.color = new Color(1, 1, 1, 0);
        //imgLeftKey.color = new Color(1, 1, 1, 0);
        //imgUpKey.color = new Color(1, 1, 1, 0);
        //StartCoroutine(FadeImage(true));

        secondArrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        player = GameObject.Find("Player").GetComponent<LPlayer>();
    }

    void Update()
    {
        if (player.livesLost == 1) // Once player dies, they learn how to jump
        {
            firstArrow.SetActive(false);
            jumpBtn.SetActive(true);
            StartCoroutine(FadeImage(false));
        }
    }

    IEnumerator FadeImage(bool isBeginning)
    {
        //if (isBeginning) // Fade in arrow key instructions
        //{ for (float i = 0; i <= 3; i += Time.deltaTime)
        //    {
        //        imgRightKey.color = new Color(1, 1, 1, i);
        //        imgLeftKey.color = new Color(1, 1, 1, i);
        //        yield return null;
        //    }
        //}
     
        if (!isBeginning) // Fade in up key instruction
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                secondArrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
