using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // match sorting layer of splat to sorting layer of parent object
        GameObject parentObject = transform.parent.gameObject;
        SpriteRenderer parentSR = parentObject.GetComponent<SpriteRenderer>();

        if (parentSR != null)
        {
            string parentSortingLayer = parentSR.sortingLayerName;
            SpriteRenderer mySR = GetComponent<SpriteRenderer>();
            mySR.sortingLayerName = parentSortingLayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
