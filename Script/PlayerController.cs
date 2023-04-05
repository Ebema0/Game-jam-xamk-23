using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }
    
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private AudioSource jumpSoundEffect;
    



    private Animator playerAnimation;

    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public GameObject Portal;

    public Text scoreText;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;
    }

    // Update is called once per frame
   void Update()
{
    isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    direction = Input.GetAxis("Horizontal");
    Debug.Log("Direction: " + direction);

    if (direction > 0f)
    {
        player.velocity = new Vector2(direction * speed, player.velocity.y);
        transform.localScale = new Vector2(0.2469058f, 0.2469058f);
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
        
    }
    else if (direction < 0f)
    {
        player.velocity = new Vector2(direction * speed, player.velocity.y);
        transform.localScale = new Vector2(-0.2469058f, 0.2469058f);
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
        
    }
    else
    {
        player.velocity = new Vector2(0, player.velocity.y);
    }

    if (Input.GetButtonDown("Jump") && isTouchingGround)
    {
        jumpSoundEffect.Play();
        player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        transform.position += new Vector3(0f, jumpSpeed * Time.deltaTime, 0f);
        }

    playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
    playerAnimation.SetBool("OnGround", isTouchingGround);

    fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            Scoring.totalScore = 0; // Reset the score to 0
            scoreText.text = "Score: " + Scoring.totalScore; // Update the score text
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // Can also use SceneManager.LoadScene(1); to load a specific scene
            respawnPoint = transform.position;
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
        else if (collision.tag == "Crystal")
        {
            
            Scoring.totalScore += 1;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        }
         else if (collision.tag == "Portal")
        {
            if (Scoring.totalScore > 5) 
            SceneManager.LoadScene(4);
            respawnPoint = transform.position;
            collision.gameObject.SetActive(true);
            

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            healthBar.Damage(0.002f);
        }
    }

  

}
