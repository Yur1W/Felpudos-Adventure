using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesmaAmarela : MonoBehaviour 
{
  Rigidbody2D corpoLesma;
  [SerializeField]
  float timerDuration;
  float timer;
  [SerializeField]
  float velocidade = 10f;
  float limiteDestruicaoX = -12f;
  bool jumpTime = true;
  Vector2 forcaImpulso = new Vector2(0, 580f);
  Vector2 gravityDirection = Vector2.down;
  float gravity = 9.8f;

  void Start () 
  { 
    corpoLesma = GetComponent<Rigidbody2D> ();
  }

  void Update()
  {
    Mover();
    Timer();
    if (jumpTime)
    { 
      corpoLesma.velocity = Vector2.zero;
      corpoLesma.AddForce(forcaImpulso);
      jumpTime = false;

    }

  }
    void FixedUpdate()
  {
      gravityDirection.Normalize();
      corpoLesma.AddForce(gravityDirection * gravity, ForceMode2D.Force);
    }
    void Timer()
  {
    timerDuration = Random.Range(1.20f, 2f);
    timer += Time.deltaTime;
    if (timer >= timerDuration)
    {
      jumpTime = true;
      timer = 0;
    }
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
