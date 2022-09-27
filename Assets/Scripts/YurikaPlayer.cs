using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YurikaPlayer : MonoBehaviour
{
    public string level;
    public float blood = 100;
    private Vector3 startPos;
    private float livesLost;

    [Header("Movement")]
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public bool grounded = false;
    public bool crouched = false;

    [Header("Grounding")]
    public LayerMask ground;

    [Header("Animation")]
    public SpriteRenderer sr;
    public SpriteRenderer bloodSr;

    public Sprite[] framesRightLeft;
    // public Sprite[] framesUp;
   //  public Sprite[] framesDown;
    public Sprite still;
    public Sprite jumpFall;
    public float framesPerSecond = 2;
    // int currentFrameIndexUpDown = 0;
    int currentFrameIndexLeftRight = 0;
    float frameTimer;

    public Sprite[] framesBlood;

    //public Sprite side;
    //public Sprite crouch;
    //public Sprite jump;

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
        //sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();

        startPos = transform.position;
        livesLost = 0;

        frameTimer = (1f / framesPerSecond);
        currentFrameIndexLeftRight = 0;
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

        bloodSr.sprite = framesBlood[(int)livesLost];

        // on death here we use uimanager to update the view on bloodcount
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(effect, transform.position, new Quaternion(90, 0, 0, 1));
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            blood -= 50;
            transform.position = startPos;

            livesLost++;
            if (livesLost == 6)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Sprite animations
        if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0)
        {
            sr.sprite = still;
        }

        if (rb2d.velocity.x > 0) // Moving right
        {
            sr.flipX = false;

            frameTimer -= Time.deltaTime;

            if (frameTimer <= 0)
            {
                currentFrameIndexLeftRight++;
                if (currentFrameIndexLeftRight == 3)
                {
                    currentFrameIndexLeftRight = 0;
                }

                frameTimer = (1f / framesPerSecond);
                sr.sprite = framesRightLeft[currentFrameIndexLeftRight];
            }
        }

        if (rb2d.velocity.x < 0) // Moving left
        {
            sr.flipX = true;

            frameTimer -= Time.deltaTime;

            if (frameTimer <= 0)
            {
                currentFrameIndexLeftRight++;
                if (currentFrameIndexLeftRight == 3)
                {
                    currentFrameIndexLeftRight = 0;
                }

                frameTimer = (1f / framesPerSecond);
                sr.sprite = framesRightLeft[currentFrameIndexLeftRight];
            }
        }

        if (rb2d.velocity.y > 0 || rb2d.velocity.y < 0) // Jumping
        {
            sr.sprite = jumpFall;
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
        }
        else
        {
            grounded = false;
        }
    }
}
