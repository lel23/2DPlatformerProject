using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float pauseAtPoint = 0;
    public Transform[] points;

    private Rigidbody2D rb2d;
    private Vector3[] pointPositions;
    private int nextIndex = 0;
    private float pauseTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pointPositions = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            pointPositions[i] = points[i].position;
            Destroy(points[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = pointPositions[nextIndex] - transform.position;
        if (moveVector.magnitude <= speed * Time.fixedDeltaTime)
        {
            nextIndex++;
            nextIndex %= pointPositions.Length;
            pauseTimer = pauseAtPoint;
        }

        moveVector.Normalize();
        pauseTimer -= Time.deltaTime;
        if (pauseTimer <= 0) rb2d.velocity = moveVector * speed;
        else rb2d.velocity = Vector2.zero;
    }
}
