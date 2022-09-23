using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string level;
    public float blood = 100;
    private Vector3 startPos;

    [Header("Movement")]
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public bool grounded = false;
    public bool crouched = false;

    [Header("Grounding")]
    public LayerMask ground;

    [Header("Animation")]
    private SpriteRenderer sr;
    public Sprite side;
    public Sprite crouch;
    public Sprite jump;

    public GameObject effect;
    public GameObject bloodSplash;

    [Header("Audio")]
    private AudioSource source;
    public AudioClip jumpSound;
    public AudioClip flagGetSound;

    // Start is called before the first frame update
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

        UpdateGrounding();

        bool inputJump1 = Input.GetKeyDown(KeyCode.Space);
        bool inputJump2 = Input.GetKeyDown(KeyCode.UpArrow);
        if ((inputJump1 || inputJump2) && grounded)
        {
            vel.y = jumpForce;
        }

        Debug.Log(grounded);
        rb2d.velocity = vel;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(effect, transform.position, new Quaternion(90, 0, 0, 1));
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            blood -= 50;
            transform.position = startPos;
        } else if (Input.GetKeyDown(KeyCode.T)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flag"))
        {
            source.clip = flagGetSound;
            source.Play();
            SceneManager.LoadScene(level);
        }
    }

    // I think I will update this method with a box collider rather
    // than using ray casts
    private void UpdateGrounding()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1.1f, ground);

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);

        if (hit.collider != null)
        {
            grounded = true;
        } else {
            grounded = false;
        }
    }
}
