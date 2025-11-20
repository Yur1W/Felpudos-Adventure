using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{   
    // Input Settings
    [Header("Input Settings")]
    float horizontalInput;
    bool jumpInput;
    bool crouchInput;
    // Movement Settings
    [Header("Movement Settings")]
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpForce = 8f;
    private Vector3 originalScale;
    [SerializeField]
    private float crouchScale = 0.5f;

    // Components
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public Animator animator;
    public GameController gc;
    // Player Stats
    [Header("Player Stats")]

    // Player Check Settings
    [Header("Player Check Settings")]
    [SerializeField]
    bool isGrounded = true;
    [SerializeField]
    bool isDead = false;

    public enum PlayerState {Running, Jumping, Falling, Crouching, Hurt}
    public PlayerState playerState = PlayerState.Running;
    public enum PlayerHpState { Hurt, Normal, Invincible}
    public PlayerHpState playerHpState = PlayerHpState.Normal;

    void Start()
    {   
        originalScale = transform.localScale;
        // get components
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {   
         // input catch
        jumpInput = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
        crouchInput = Input.GetKey(KeyCode.LeftShift);
        // direction
        horizontalInput = 0;

        // sprite direction
        sprite.flipX = horizontalInput > 0;
        // movemnent
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        // State Machine
        switch (playerState)
        {
            case PlayerState.Running: Run(); break;
            case PlayerState.Jumping: Jump(); break;
            case PlayerState.Crouching: Crouch(); break;
            case PlayerState.Falling: Fall(); break;
            default: Run(); break;
        }
        switch (playerHpState)
        {
            case PlayerHpState.Hurt: TakeDamage(); break;
            case PlayerHpState.Normal: break;
            case PlayerHpState.Invincible: Invicibility(); break;
            default: break;
        }
    }
    private void Update()
    {   
        Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);
        GroundCheck();
    }
    void TakeDamage()
    {
        Debug.Log("Player took damage! Vida : " + GameController.lives);

        StartCoroutine(BlinkEffect());


        GameController.lives--;
        GameController.score = 0;
        gc.ui.UpdateLives(GameController.lives);
        gc.ui.UpdateScore(GameController.score);
        if (GameController.lives <= 0)
        {
            gc.GameOver();
        }
        else
        {
            playerHpState = PlayerHpState.Invincible;
        }

    }
    IEnumerator InvicibilityDuration()
    {   
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<CapsuleCollider2D>().enabled = true;   
        playerHpState = PlayerHpState.Normal;
    }
    private void Invicibility()
    {
        StartCoroutine(InvicibilityDuration());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Enemy"))
        {
            playerHpState = PlayerHpState.Hurt;
            Destroy(collision.gameObject);
        } 
    }

    private void Jump()
    {       
            animator.Play("Pulando");
            rb.velocity = Vector2.up * jumpForce;
            
            playerState = PlayerState.Falling;
    }

    private void Crouch()
    {
        animator.Play("Abaixando");
        transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchScale, originalScale.z);
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;

        
        if (!crouchInput)
        {
            transform.localScale = originalScale;
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            playerState = PlayerState.Running;
        }
    }
    private void Run()
    {   
        animator.Play("Correndo");   
        if (jumpInput && isGrounded)
        {
            playerState = PlayerState.Jumping;
        }
        if (crouchInput)
        {
            playerState = PlayerState.Crouching;
        }
        if (!isGrounded)
        {
            playerState = PlayerState.Falling;
        }   
    }
    private void Fall()
    {   
        animator.Play("Caindo");
        if (isGrounded)
        {
            playerState = PlayerState.Running;
        }

    }
    private IEnumerator BlinkEffect()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);

    }
    public void KillPlayer()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
    }
    private void GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground")))
        { isGrounded = true; }
        else
        { isGrounded = false; }
    }
}
