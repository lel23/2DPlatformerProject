using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController2 : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject arrow;
    public GameObject deathBtn;

    void Start()
    {
        platform1.SetActive(false);
        platform2.SetActive(false);
        deathBtn.SetActive(false);

        arrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        StartCoroutine(FadeInInstruction());
    }

    IEnumerator FadeInInstruction()
    {
        yield return new WaitForSeconds(6);

        platform1.SetActive(true);
        platform2.SetActive(true);
        deathBtn.SetActive(true);

        for (float i = 0; i <= 3; i += Time.deltaTime)
        {
            arrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return null;
        }

    }
}
