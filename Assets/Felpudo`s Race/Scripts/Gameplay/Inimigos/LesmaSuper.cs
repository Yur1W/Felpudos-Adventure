using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesmaSuper : MonoBehaviour
{
    float velocidade = 3f;
    float limiteDestruicaoX = -12f;
    public float vida = 3f;
    // Start is called before the first frame update
    

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
