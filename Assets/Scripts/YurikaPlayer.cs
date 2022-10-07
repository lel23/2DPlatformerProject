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
    public static bool isInSecretLevel;
    public static bool hasPartyHat;
    public GameObject partyHat;

    // MOVEMMENT
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;

    // GROUNDING & OnPlatform
    public bool grounded = false;
    public LayerMask ground;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    private bool onPlatform = false;
    public LayerMask platformMask;

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
    public AudioClip bloodSound;
    public AudioClip backgroundSound;

    void Start()
    {
        isDead = false;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();

        startPos = transform.position;
        livesLost = 0;
        isInSecretLevel = false;
        hasPartyHat = false;

        frameTimer = (1f / framesPerSecond);
        currentFrameIndexLeftRight = 0;

        source.clip = backgroundSound;
        source.Play();
    }

    void Update()
    {
        if (PauseMenu.paused || isDead) return;

        ///////////// MOVEMENT
        Vector2 vel = rb2d.velocity;

        if (Input.GetKey(KeyCode.LeftArrow)) vel.x = (-1) * speed;
        else if (Input.GetKey(KeyCode.RightArrow)) vel.x = speed;
        else if (grounded) vel.x = 0;

        // move with moving platform
        onPlatform = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, platformMask);
        if (onPlatform)
        {
            Collider2D platform = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, platformMask);
            Vector2 platformVel = platform.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
            Debug.Log(platformVel);
            vel += platformVel;
        }

        rb2d.velocity = vel;

        // jump with horizontal momentum
        bool inputJump = Input.GetKeyDown(KeyCode.UpArrow);
        if (inputJump && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            source.clip = jumpSound;
            source.Play();
        }

        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);
        // Debug.Log(grounded);

        // kys
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDead = true;
            StartCoroutine(OnDeath());
        }

        ///////////// ANIMATION
        if (livesLost >= 0 && livesLost <= 5)
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

        // jumping animation
        if (!grounded) sr.sprite = jumpFall;

        if (hasPartyHat)
        {
            partyHat.SetActive(true);
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

        if (other.gameObject.CompareTag("EnterSecret"))
        {
            isInSecretLevel = true;
            
        }
        //if (other.gameObject.CompareTag("ExitSecret"))
        //{
        //    isInSecretLevel = false;
        //}
        //if (other.gameObject.CompareTag("ExitSecretFinal"))
        //{
        //    isInSecretLevel = false;
        //    hasPartyHat = true;
        //}
    }

    public IEnumerator OnDeath()
    {
        source.PlayOneShot(bloodSound);

        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        livesLost++;

        sr.enabled = false;
        bloodSr.enabled = false;
        if (hasPartyHat)
            partyHat.GetComponent<SpriteRenderer>().enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1);

        sr.enabled = true;
        bloodSr.enabled = true;
        if (hasPartyHat)
            partyHat.GetComponent<SpriteRenderer>().enabled = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        transform.position = startPos;

        isDead = false;
        if (livesLost > 5)
        {
            livesLost = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
