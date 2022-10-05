using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public bool verticalOnly;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (verticalOnly) transform.position = new Vector3(transform.position.x, player.transform.position.y + 3.5f, -10);
        else transform.position = player.transform.position + offset;
    }
}