using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Conqueror : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject EnemyBulletPrefab; // Prefab of the enemy bullet
    public Transform FirePoint; // Position where the bullet is instantiated
    bool isDestroyed = false;
    public Sprite[] sprites;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    PolygonCollider2D polygon;
    float speed = 5f;
    float fireRate = 0.1f; // Delay between shots
    float nextFireTime; // Time when the next shot can be fired

    GameObject player; // Reference to the player GameObject or its position

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        polygon = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has the "Player" tag
    }

    void Update()
    {
        if (player != null && Time.time > nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void fire(float theMass, Vector2 direction)
    {
        sprite.sprite = sprites[Random.Range(0, sprites.Length)];
        List<Vector2> path = new List<Vector2>();
        sprite.sprite.GetPhysicsShape(0, path);
        polygon.SetPath(0, path.ToArray());

        rigid.mass = theMass;
        rigid.velocity = direction.normalized * speed;
        rigid.AddTorque(Random.Range(-4f, 4f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet2") && !isDestroyed)
        {
            Destroy(other.gameObject); // Destroy the asteroid
            isDestroyed = true; // Set the flag to indicate the conqueror is destroyed

            if (rigid.mass > 0.7f)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                
            }

            Destroy(gameObject); // Destroy the conqueror
        }
    }

    void Split()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        C_Conqueror small = Instantiate(this, position, transform.rotation);
        Vector2 direction = Random.insideUnitCircle;
        float mass = rigid.mass / 2;
        small.fire(mass, direction);
    }

    void ShootAtPlayer()
    {
        if (EnemyBulletPrefab != null && FirePoint != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            GameObject bullet = Instantiate(EnemyBulletPrefab, FirePoint.position, Quaternion.identity);
            C_EnemyBullet bulletScript = bullet.GetComponent<C_EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.fire(direction);
            }
        }
    }
}

