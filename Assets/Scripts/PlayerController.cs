using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool Muere = false;
    // public properties
    public float velocityX = 15f;
    public float jumpForce = 40;
    private int contador = 0;
    // private components
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    private const int ANIMATION_RUN = 0;
    private const int ANIMATION_JUMP = 1;
    private const int ANIMATION_SLIDE = 2;
    private const int ANIMATION_DEAD = 3;
    private const int ANIMATION_IDLE = 4;


    private const string TAG_ENEMY = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando Game Object");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Muere == false)
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            changeAnimation(ANIMATION_RUN);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            changeAnimation(ANIMATION_DEAD);
        }

        if (Input.GetKey(KeyCode.X))
        {
            changeAnimation(ANIMATION_SLIDE);
            Muere = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // salta
            changeAnimation(ANIMATION_JUMP); // saltar
        }
        if (contador == 10)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            changeAnimation(ANIMATION_IDLE);
            Muere = false;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Input.GetKey(KeyCode.X))
            {                
                velocityX += 1.5f;
                Debug.Log("velocidad es de: " + velocityX);
                Destroy(collision.gameObject);
                contador += 1;
            }
            Muere = true;            
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            velocityX += 1.5f;
            contador += 1;
            Debug.Log("velocidad es de: " + velocityX);
        }
    }
    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
