using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LesliePlayer : MonoBehaviour
{
    public float blood = 100;
    private Vector3 startPos;

    [Header("Movement")]
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public bool crouched = false;

    [Header("Grounding")]
    public bool grounded = false;
    public LayerMask ground;
    public Transform groundCheckPoint;
    public float groundCheckRadius;

    [Header("Animation")]
    private SpriteRenderer sr;
    public Sprite side;
    public Sprite crouch;
    public Sprite jump;

    public GameObject bloodSplash;

    [Header("Audio")]
    private AudioSource source;
    public AudioClip jumpSound;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = rb2d.velocity;
        vel.x = Input.GetAxis("Horizontal") * speed;

        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);

        bool inputJump1 = Input.GetKeyDown(KeyCode.Space);
        bool inputJump2 = Input.GetKeyDown(KeyCode.UpArrow);
        if ((inputJump1 || inputJump2) && grounded)
        {
            vel.y = jumpForce;
        }

        rb2d.velocity = vel;

        // on death here we use uimanager to update the view on bloodcount
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Instantiate(effect, transform.position, new Quaternion(90, 0, 0, 1));
            OnDeath();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnDeath()
    {
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        blood -= 50;
        transform.position = startPos;
    }
}
