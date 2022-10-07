using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public bool verticalOnly;
    public bool stationary;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (stationary) return;
        if (verticalOnly) transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
        else transform.position = player.transform.position + offset;
    }
}