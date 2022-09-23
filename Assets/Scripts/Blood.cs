using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite[] blood;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, blood.Length);
        sr.sprite = blood[rand];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
