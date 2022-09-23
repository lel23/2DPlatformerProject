using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI text;
    public int bloodCount;

    private void Awake()
    {
        instance = this;
        text.text = bloodCount.ToString();
    }

    public void DecreaseBlood(int blood)
    {
        bloodCount -= blood;
        text.text = bloodCount.ToString();
    }
}
