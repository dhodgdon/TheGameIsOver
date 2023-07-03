using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK_Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] runSprites;
    public Sprite climbSprite;
    private int spriteIndex;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private Collider2D[] overlaps = new Collider2D[4];
    private Vector2 direction;

    private bool grounded;
    private bool climbing;

    public float moveSpeed = 3f;
    public float jumpStrength = 4f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void CheckCollision()
    {
        grounded = false;
        climbing = false;

        float skinwidth = 0.1f;

        Vector2 size = collider.bounds.size;
        size.y += skinwidth; // buffer for player not overlapping with platform
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, overlaps);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = overlaps[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.6f + skinwidth);

                Physics2D.IgnoreCollision(overlaps[i], collider, !grounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }
        }
    }

    // private void OnCollisionEnter2D(Collision collision)
    // {
    //     grounded = false;

    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         grounded = true;
    //     }
    // }

    private void Update()
    {
        CheckCollision();

        if (climbing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                direction = Vector2.up * jumpStrength;
            }
            
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        }
        // else if (climbing && !Input.GetButtonDown("Jump"))
        // {
        //     direction.y = Input.GetAxis("Vertical") * moveSpeed;
        // }
        else if (grounded && Input.GetButtonDown("Jump"))
        {
            direction = Vector2.up * jumpStrength;
        }
        else 
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;
        direction.y = Mathf.Max(direction.y, -3f);

        // if (grounded) {
        //     direction.y = Mathf.Max(direction.y, -3f);
        // }

        if (direction.x > 0f) 
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0f) 
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }

    private void AnimateSprite()
    {
        if (climbing && !grounded)
        {
            spriteRenderer.sprite = climbSprite;
        }
        else if (direction.x != 0f)
        {
            spriteIndex++;

            if (spriteIndex >= runSprites.Length)
            {
                spriteIndex = 0;
            }

            spriteRenderer.sprite = runSprites[spriteIndex];
        }
        else if (direction.x == 0f)
        {
            spriteIndex = 0;
            spriteRenderer.sprite = runSprites[spriteIndex];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            FindObjectOfType<AK_GameManager>().LevelFailed();
        }
        else if (collision.gameObject.CompareTag("Objective"))
        {
            enabled = false;
            FindObjectOfType<AK_GameManager>().LevelComplete();
        }
    }
}


