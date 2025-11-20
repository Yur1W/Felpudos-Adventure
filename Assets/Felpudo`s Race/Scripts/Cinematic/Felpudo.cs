
using UnityEngine;

public class Felpudo : MonoBehaviour
{
    public float input;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField]
    GameObject player;
    float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = 0;
        anim = GetComponent<Animator>();
        anim.Play("OnFoot");
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2)
        {
            anim.Play("OnFoot");
            input = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Carro"))
        {
            Destroy(collision.gameObject);
            StartGame();
        }
    }

    void StartGame()
    {  
        player.gameObject.SetActive(true);
        GameController.GameStarted = true;
        Destroy(gameObject);
    }
}
