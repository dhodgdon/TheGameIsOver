using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public float respawnTime = 10f;

    private void Start()
    {
        // Start spawning the boss
        StartCoroutine(SpawnBoss());
    }

    private IEnumerator SpawnBoss()
    {
        while (true)
        {
            // Instantiate the boss
            GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

            // Wait for the boss to be destroyed
            yield return new WaitForSeconds(respawnTime);

            // Check if the boss is still active
            if (boss != null)
            {
                // Boss is still active, destroy it
                Destroy(boss);
            }
        }
    }
}
