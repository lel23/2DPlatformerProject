using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCombustion : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public Image spaceKeyImg;


    // Start is called before the first frame update
    void Start()
    {
        platform1.SetActive(false);
        platform2.SetActive(false);

        spaceKeyImg.color = new Color(1, 1, 1, 0);

        StartCoroutine(ExecuteAfterTime(10));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        platform1.SetActive(true);
       
        Debug.Log(platform1.activeSelf);
        platform2.SetActive(true);

        StartCoroutine(FadeImage(true));

    }

    IEnumerator FadeImage(bool b)
    {
        if (b)
        {
            for (float i = 0; i <= 3; i += Time.deltaTime)
            {
                spaceKeyImg.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
