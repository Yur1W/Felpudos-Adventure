using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFelpudo : MonoBehaviour
{
	SpriteRenderer sprite;
	Rigidbody2D rb;
	BoxCollider2D boxCollider;
	Animator anim;
	[SerializeField]
	public float currentSpeed;
	[SerializeField]
	float jumpSpeed = 8f;
	[SerializeField]
	float moveSpeed = 8f;
	public float jumpForce = 6f;
	[SerializeField]
	public float moveInput;
	public bool jumpInput;
	Vector3 movement = new Vector3();
	[SerializeField]
	float groundCheckDistance = 0.75f;

	// Use this for initialization
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();



	
	}

	// Update is called once per frame
	void Update()
	{
		moveInput = Input.GetAxisRaw("Horizontal");
		jumpInput = Input.GetKey(KeyCode.Space);
		Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);
	}
	bool GroundCheck()
	{
		return Physics2D.CircleCast(transform.position, 0.12f, Vector2.down, groundCheckDistance, LayerMask.GetMask("Floor"));
	}
	bool BoxCheck()
	{
		return Physics2D.CircleCast(transform.position, 0.12f, Vector2.down, groundCheckDistance, LayerMask.GetMask("Box"));
	}
    void FixedUpdate()
    {
		Movement();
    }

	void Movement()
	{
		currentSpeed = moveSpeed;
		movement = new Vector3(moveInput, 0, 0);
		movement.Normalize();
		transform.position += movement * currentSpeed * Time.deltaTime;

		if (moveInput > 0)
		{
			sprite.flipX = false;
		}
		else if (moveInput < 0)
		{
			sprite.flipX = true;
		}
		if (jumpInput && (GroundCheck() || BoxCheck()))
		{
			currentSpeed = jumpSpeed;
			rb.linearVelocity = Vector2.up * jumpForce;
		}
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Pedra"))
        {
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

}