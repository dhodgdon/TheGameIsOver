using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int bulletDamage = 1;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target)
            return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // other.GameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        FindObjectOfType<Health>().TakeDamage(bulletDamage);
        Destroy(GameObject);
    }
}
