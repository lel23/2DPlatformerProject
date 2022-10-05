using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YurikaPlayer : MonoBehaviour
{
    public static float livesLost;
    private Vector3 startPos;
    private bool isDead;

    // MOVEMMENT
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public bool crouched = false;

    // GROUNDING
    public bool grounded = false;
    public LayerMask ground;
    public Transform groundCheckPoint;
    public float groundCheckRadius;

    // ANIMATION
    private SpriteRenderer sr;
    public SpriteRenderer bloodSr;

    public Sprite[] framesRightLeft;
    public Sprite still;
    public Sprite jumpFall;
    public float framesPerSecond = 2;
    int currentFrameIndexLeftRight = 0;
    float frameTimer;

    public Sprite[] framesBlood;
    public GameObject bloodSplash;

    // AUDIO
    private AudioSource source;
    public AudioClip jumpSound;

    void Start()
    {
        isDead = false;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();

        startPos = transform.position;
        livesLost = 0;

        frameTimer = (1f / framesPerSecond);
        currentFrameIndexLeftRight = 0;
    }

    // Update is called once per frame
    void Update()
    {

        ///////////// MOVEMENT
        Vector2 vel = rb2d.velocity;

        if (Input.GetKey(KeyCode.LeftArrow)) vel.x = (-1) * speed;
        else if (Input.GetKey(KeyCode.RightArrow)) vel.x = speed;
        else vel.x = 0;

        bool inputJump = Input.GetKeyDown(KeyCode.UpArrow);
        if (inputJump && grounded)
        {
            vel.y = jumpForce;
            source.clip = jumpSound;
            source.Play();
        }
        rb2d.velocity = vel;

        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);
        // Debug.Log(grounded);

        // kys
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDead = true;
            StartCoroutine(OnDeath());
        }
        // REMOVE WHEN OUT OF DEVELOPMENT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        else if (Input.GetKeyDown(KeyCode.T)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        ///////////// ANIMATION
        bloodSr.sprite = framesBlood[(int)livesLost];
        if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0)
        {
            sr.sprite = still;
        }

        if (rb2d.velocity.x < 0 || rb2d.velocity.x > 0) // Moving left or right
        {
            if (rb2d.velocity.x <0) // Left
                sr.flipX = true;
            if (rb2d.velocity.x > 0) // Right
                sr.flipX = false;

            frameTimer -= Time.deltaTime;

            if (frameTimer <= 0)
            {
                currentFrameIndexLeftRight++;
                if (currentFrameIndexLeftRight == 5)
                {
                    currentFrameIndexLeftRight = 0;
                }

                frameTimer = (1f / framesPerSecond);
                sr.sprite = framesRightLeft[currentFrameIndexLeftRight];
            }
        }

        if (!grounded) // Jumping
        {
            sr.sprite = jumpFall;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Obstacle")
        {
            if (!isDead)
            {
                isDead = true;
                StartCoroutine(OnDeath());
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Replenish"))
        {
            if (livesLost > 0)
            {
                livesLost--;
                Destroy(other.gameObject);
            }
        }
    }

    public IEnumerator OnDeath()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        if (isDead)
            livesLost++;
        sr.enabled = false;
        bloodSr.enabled = false;
        yield return new WaitForSeconds(1);

        sr.enabled = true;
        bloodSr.enabled = true;
        transform.position = startPos;
        isDead = false;
        if (livesLost > 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
