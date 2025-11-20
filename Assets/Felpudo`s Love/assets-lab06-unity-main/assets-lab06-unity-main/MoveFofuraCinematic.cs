using UnityEngine;

public class MoveFofuraCinematic : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 moviment;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start()
	{
        moviment = new Vector3(1, 0, 0); 
        sprite = GetComponent<SpriteRenderer>();
	}

	void ChangeSpeed()
	{
        
	}

	// Update is called once per frame
	void Update()
    {   
        moviment.Normalize();
        transform.position += moviment * moveSpeed * Time.deltaTime;
        
        if (moviment.x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
	}
}
