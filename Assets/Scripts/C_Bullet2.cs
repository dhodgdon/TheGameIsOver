using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Bullet2 : MonoBehaviour
{
    public int damage = 5; // Damage value of the bullet

    Rigidbody2D rb;
    SpriteRenderer sr;
    float speed = 15f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        transform.up = direction;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Conqueror"))
        {
            Destroy(gameObject);

            // Check if the Conqueror object has the Boss component
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                // Deal damage to the boss
                boss.TakeDamage(damage);
            }
        }
    }
}
