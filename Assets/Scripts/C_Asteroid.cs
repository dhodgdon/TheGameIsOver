using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Asteroid : MonoBehaviour
{
    public GameObject Explosion;
    bool isDestroyed = false;
    public Sprite[] sprites;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    PolygonCollider2D polygon;
    float speed = 2f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    public void kick(float theMass, Vector2 direction)
    {
        sprite.sprite = sprites[Random.Range(0, sprites.Length)];
        List<Vector2> path = new List<Vector2>();
        sprite.sprite.GetPhysicsShape(0, path);
        polygon.SetPath(0, path.ToArray());

        rigid.mass = theMass;
        float width = Random.Range(7.5f, 13.3f);
        float height = width;
        transform.localScale = new Vector2(width, height); // Remove scaling based on mass
        rigid.velocity = direction.normalized * speed;
        rigid.AddTorque(Random.Range(-4f, 4f));

        // Reset the size of the collider
        polygon.transform.localScale = Vector3.one;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !isDestroyed)
        {
            Destroy(other.gameObject); // Destroy the bullet
            isDestroyed = true; // Set the flag to indicate the asteroid is destroyed

            if (rigid.mass > 0.7f)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                split();
                split();
            }

            Destroy(this.gameObject); // Destroy the asteroid
            FindObjectOfType<AK_GameManager>().CC_LevelComplete();
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(this.gameObject);
        }
    }

    void split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        C_Asteroid small = Instantiate(this, position, this.transform.rotation);
        Vector2 direction = Random.insideUnitCircle;
        float mass = rigid.mass / 2;
        small.kick(mass, direction);
    }
}