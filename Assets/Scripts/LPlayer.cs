using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LPlayer : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;

    [HideInInspector] public float livesLost = 0;
    [HideInInspector] public Vector3 startPos;
    Vector3 temp;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isInSecretLevel = false;
    [HideInInspector] public bool hasPartyHat = false;
    public GameObject partyHat;

    [Header("Grounding")]
    bool grounded = true;
    public LayerMask ground;
    public Transform groundCheckPoint;
    float groundCheckRadius = 0.2f;
    private bool onPlatform = false;
    public LayerMask platformMask;

    [Header("Animation")]
    [HideInInspector] public SpriteRenderer sr;
    private SpriteRenderer bloodSr;

    public Sprite[] movementFrames;
    public Sprite still;
    public Sprite jump;
    public float framesPerSecond = 2;
    int currentFrame = 0;
    float frameTimer;

    public Sprite[] deathFrames;
    public GameObject bloodSplash;

    [Header("Audio")]
    private AudioSource source;
    public AudioClip jumpSound;
    public AudioClip bloodSound;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        source = gameObject.GetComponent<AudioSource>();

        bloodSr = GameObject.Find("BloodCounter").GetComponent<SpriteRenderer>();
        partyHat = GameObject.Find("PartyHat");
        partyHat.SetActive(false);

        startPos = transform.position;
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);
        onPlatform = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, platformMask);

        // movement and suicide
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            {
                rb2d.AddForce(new Vector2(0, jumpForce));
                source.clip = jumpSound;
                source.Play();
            }
            if (onPlatform)
            {
                Collider2D platform = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, platformMask);
                Vector2 platformVel = platform.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
                rb2d.velocity += platformVel;
            }
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

            // suicide
            if (Input.GetKey(KeyCode.Space))
            {
                isDead = true;
                StartCoroutine(OnDeath());
            }
        }
        
        // animation
        if (livesLost >= 0 && livesLost <= 5)
            bloodSr.sprite = deathFrames[(int)livesLost];
        if (rb2d.velocity == Vector2.zero) sr.sprite = still;
        else if (rb2d.velocity.x != 0)
        {
            if (rb2d.velocity.x > 0)
            {
                sr.flipX = false;
                if (hasPartyHat) 
                    partyHat.GetComponent<SpriteRenderer>().flipX = false;
            }
            else 
            { 
                sr.flipX = true; 
                if (hasPartyHat) 
                    partyHat.GetComponent<SpriteRenderer>().flipX = true;
            }

            frameTimer -= Time.deltaTime;

            if (frameTimer <= 0)
            {
                currentFrame++;
                if (currentFrame == movementFrames.Length - 1)
                    currentFrame = 0;

                frameTimer = (1f / framesPerSecond);
                sr.sprite = movementFrames[currentFrame];
            }
        }
        if (!grounded) sr.sprite = jump;
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

    public IEnumerator OnDeath()
    {
        source.PlayOneShot(bloodSound);
        if (!isInSecretLevel) livesLost++;

        Instantiate(bloodSplash, transform.position, Quaternion.identity);

        sr.enabled = false;
        bloodSr.enabled = false;
        if (hasPartyHat)
            partyHat.GetComponent<SpriteRenderer>().enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1);

        sr.enabled = true;
        bloodSr.enabled = true;
        if (hasPartyHat)
            partyHat.SetActive(false);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        transform.position = startPos;

        isDead = false;
        if (livesLost > 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PutOnHat()
    {
        hasPartyHat = true;
        partyHat.SetActive(true);
    }

    public void InOrOutSecret(Vector2 position)
    {
        
        if (!isInSecretLevel)
        {
            isInSecretLevel = true;
            sr.color = Color.white;
            temp = startPos;
            startPos = new Vector2(33, 51);
        }
        else
        {
            isInSecretLevel = false;
            sr.color = Color.black;
            startPos = temp;
        }
    }
}
