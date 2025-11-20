using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Token : MonoBehaviour
{
    public float speed = 5f;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {   
            animator.Play("Explode");
            FindObjectOfType<GameController>().AddToken();
            
            Destroy(gameObject);
        }
    }
}
