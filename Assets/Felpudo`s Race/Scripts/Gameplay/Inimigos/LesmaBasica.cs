using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesmaBasica : MonoBehaviour
{
    float velocidade = 2f;
    float limiteDestruicaoX = -12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }
    void Mover()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        if (transform.position.x < limiteDestruicaoX)
        {
            Destroy(gameObject);

        }
    }
    
}
