using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject BossBullet;
    public GameObject laserPrefab;
    public Transform bulletSpawnPoint;
    public Transform laserSpawnPoint;
    public int hitPoints = 3;
    public int maxHits = 20;
    public float fireRate = 0.1f;
    public float laserDuration = 3f;
    public bool isFiringLaser = false;
    public float moveDuration = 2f;
    private bool isVisible = true;
    private bool canFireLaser = true;

    private void Start()
    {
        // Start the boss movement
        StartCoroutine(MoveBoss());
        // Start the bullet firing
        StartCoroutine(FireBullets());
    }

    private IEnumerator MoveBoss()
    {
        // Set the initial and target positions for left-to-right movement
        Vector3 initialPosition = new Vector3(0.21f, 3.26f, 0f);
        Vector3 targetPosition = new Vector3(8f, 3.26f, 0f);

        // Move up to the initial position
        Vector3 upTarget = new Vector3(initialPosition.x, 10f, 0f);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration; // Adjust moveDuration to control the movement speed
            transform.position = Vector3.Lerp(upTarget, initialPosition, t);
            yield return null;
        }

        // Move left to right repeatedly
        while (true)
        {
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / moveDuration; // Adjust moveDuration to control the movement speed
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / moveDuration; // Adjust moveDuration to control the movement speed
                transform.position = Vector3.Lerp(targetPosition, initialPosition, t);
                yield return null;
            }
        }
    }

    private IEnumerator FireBullets()
    {
        while (true)
        {
            if (isVisible)
            {
                // Instantiate a boss bullet
                GameObject bullet = Instantiate(BossBullet, bulletSpawnPoint.position, Quaternion.identity);

                // Ignore collision with the player (if applicable)
                Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
                if (playerCollider != null)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), playerCollider);
                }
            }

            yield return new WaitForSeconds(5f); // Wait for 5 seconds before firing again
        }
    }

    private IEnumerator FireLaser()
    {
        isFiringLaser = true;

        // Instantiate a laser
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);

        isFiringLaser = false;
        canFireLaser = false;
        yield return new WaitForSeconds(fireRate);
        canFireLaser = true;
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            // Boss is destroyed
            DestroyBoss();
        }
        else
        {
            // Boss is hit but not destroyed
            isVisible = !isVisible;

            if (!isVisible)
            {
                // Boss is hit and disappears temporarily
                StopCoroutine("MoveBoss"); // Stop only the movement coroutine
                StartCoroutine(RespawnBoss());
            }
            else if (canFireLaser)
            {
                // Boss is hit and fires a laser
                StartCoroutine(FireLaser());
            }
        }
    }

    private IEnumerator RespawnBoss()
    {
        yield return new WaitForSeconds(fireRate);
        isVisible = true;
        StartCoroutine(MoveBoss()); // Start the movement coroutine again
        StartCoroutine(FireBullets()); // Start the bullet firing coroutine again
    }

    private void DestroyBoss()
    {
        StopAllCoroutines();
        // Instantiate a destruction effect or perform other actions

        // Destroy the boss
        Destroy(gameObject);
    }
}
