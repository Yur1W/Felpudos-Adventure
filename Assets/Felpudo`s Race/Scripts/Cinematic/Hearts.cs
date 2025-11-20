using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField]
    Felpudo felpudo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (collision.CompareTag("Fofura"))
            {
                felpudo.input = 1;
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fofura"))
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
}
