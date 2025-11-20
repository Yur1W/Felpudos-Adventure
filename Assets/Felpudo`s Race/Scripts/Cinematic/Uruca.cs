using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uruca : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    float input;
    float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        input = -1;
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2)
        {
            speed = 8f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if (transform.position.x > 50f)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fofura"))
        {
            input = 1;
            sprite.flipX = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(new Vector2(800f, 500f));
        }
    }
}
