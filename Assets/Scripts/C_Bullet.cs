using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float speed = 15f;

    public System.Action onDestroyAsteroid;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void fire(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        transform.up = direction;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Asteroid")
        {
            this.onDestroyAsteroid.Invoke();
            Destroy(gameObject);
        }
    }
   
}
