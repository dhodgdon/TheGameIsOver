using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Player2 : MonoBehaviour
{
    public C_Bullet2 Bullet2;
    public float speed;
    public float resetDelay = 3f;
    public float vanishDuration = 2f;

    private SpriteRenderer spriteRend;
    private Rigidbody2D rb;

    private bool isMoving = true; // Added flag to control player movement

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isMoving) // Added check to prevent movement when not allowed
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            C_Bullet2 theBullet2 = Instantiate(Bullet2, transform.position, Quaternion.identity);
            theBullet2.fire(transform.up);
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;
        // Calculate new position
        pos += direction * speed * Time.deltaTime;
        // Make sure the new position isn't outside of the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        // Update player's position
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (spriteRend.enabled) // Check if the player is visible
            {
                StartCoroutine(ResetWithDelay());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Conqueror"))
        {
            if (spriteRend.enabled) // Check if the player is visible
            {
                StartCoroutine(ResetWithDelay());
            }
        }
    }

    IEnumerator ResetWithDelay()
    {
        isMoving = false; // Stop player movement

        // Turn off collisions and vanish
        TurnOffCollisions();
        Vanish();

        yield return new WaitForSeconds(vanishDuration);

        // Reset player
        Reset();

        yield return new WaitForSeconds(resetDelay - vanishDuration);

        // Turn on collisions and reappear
        TurnOnCollisions();
        Reappear();

        isMoving = true; // Allow player movement again
    }

    void TurnOffCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore");
    }

    void Vanish()
    {
        spriteRend.enabled = false;
    }

    void Reset()
    {
        spriteRend.color = new Color(0f, 1f, 0f, 0.2f);
        Vector2 centerPosition = new Vector2(0f, 0f);
        transform.position = centerPosition;
        transform.eulerAngles = Vector3.zero;
    }

    void TurnOnCollisions()
    {
        spriteRend.color = new Color(0f, 1f, 0f, 1f);
        gameObject.layer = LayerMask.NameToLayer("Default"); // Replace "Player" with the desired layer name or use the default layer
    }

    void Reappear()
    {
        spriteRend.enabled = true;
    }
}
