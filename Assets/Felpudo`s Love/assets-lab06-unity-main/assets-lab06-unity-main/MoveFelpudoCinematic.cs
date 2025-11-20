using UnityEngine;

public class MoveFelpudoCinematic : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 moviment;
	SpriteRenderer sprite;
	public float timer;
	public float waitTime = 2f;
	[SerializeField]
	GameObject canvas;

	// Use this for initialization
	void Start()
	{
       waitTime = 5f * Time.deltaTime;
        sprite = GetComponent<SpriteRenderer>();
	}

	void Wait()
	{	
		timer += Time.deltaTime;
        if (timer <= waitTime)
		{
			moviment = new Vector3(0, 0, 0);
		}
		else
		{
			moviment = new Vector3(1, 0, 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		Wait();
		moviment.Normalize();
		transform.position += moviment * moveSpeed * Time.deltaTime;

		if (moviment.x > 0)
		{
			sprite.flipX = false;
		}
		else
		{
			sprite.flipX = true;
		}
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Fofura"))
		{
			this.enabled = false;
		}
		if (collision.CompareTag("Particles"))
        {
			collision.GetComponent<ParticleSystem>().Play();
			canvas.SetActive(true);
        }
    }
}
