using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuy : MonoBehaviour
{
    public GameObject prefab;
    public float minTime = 2f;
    public float maxTime = 4f;

    private SpriteRenderer spriteRenderer;
    public Sprite throwSprite;
    public Sprite normalSprite;

    private bool throwingObject;

    private void Start()
    {
        Spawn();
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void AnimateSprite()
    {
        if (throwingObject)
        {
            spriteRenderer.sprite = throwSprite;
            throwingObject = false;
            Invoke(nameof(AnimateSprite), 1f);
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }    

    private void Spawn()
    {
        throwingObject = true;
        Invoke(nameof(AnimateSprite), 0f);

        Instantiate(prefab, transform.position - new Vector3(-2.1f, 1.15f, 0), Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
