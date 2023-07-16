using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Player : MonoBehaviour
{
    public C_Bullet Bullet;
    Rigidbody2D rb;
    SpriteRenderer spriteRend;
    bool forceOn = false;
    float forceAmount = 10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        // spriteRend.color = new Color(0.1f, 1f, 0.1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        forceOn = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            C_Bullet theBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            theBullet.fire(transform.up);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), 180f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            torqueDirection = 1f;
        }else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            torqueDirection = -1f;
        }
        else
        {
            torqueDirection = 0f;
        }
        wrapAroundBoundary();
    }
    void wrapAroundBoundary()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if(x > 8f) { x = x - 16f; }
        if(x < -8f) { x = x + 16f;}
        if (y > 4.5f) { y = y - 9f;}
        if (y < -4.5f) { y = y + 9f; }
        transform.position = new Vector2(x, y);
    }
    void FixedUpdate()
    {
        if (forceOn)
        {
            rb.AddForce(transform.up * forceAmount);
        }
        if(torqueDirection != 0)
        {
            rb.AddTorque(torqueDirection * torqueAmount);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            transform.position = new Vector2(40000f, 40000f);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            turnOffCollisions();
            Invoke("reset", 5f);
        }
    }
    void turnOffCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore");
    }
    void reset()
    {
        // spriteRend.color = new Color(0f, 1f, 0f, 0.2f);
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Invoke("turnOnCollisions", 5f);
    }
    void turnOnCollisions()
    {
       //  spriteRend.color = new Color(0f, 1f, 0f, 1f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    
}
