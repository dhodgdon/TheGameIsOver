using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConquerorSpawn : MonoBehaviour
{
    public Conqueror Conqueror;
    float spawnRate = 2.0f;
    float spawnDistance = 14f;
    float maxSpawnRateInSeconds = 5f;
    float spawnHeight = 10f; // Adjust this value to set the height at which Conquerors spawn

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnRate);
        Invoke("ScheduleNextSpawn", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    void Spawn()
    {
        float spawnX = Random.Range(-spawnDistance, spawnDistance);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0f); // Set the spawn position at the top of the screen
        float angle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        Conqueror theConqueror = Instantiate(Conqueror, spawnPosition, rotation);
        Vector2 direction = rotation * Vector2.down; // Conquerors will move downwards
        float mass = Random.Range(0.8f, 1.4f);
        theConqueror.fire(mass, direction);
    }

    public void ScheduleNextSpawn()
    {
         maxSpawnRateInSeconds = 5f;
        float spawnRateInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnRateInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnRateInSeconds = 1f;
        }
        Invoke("Spawn", spawnRateInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }
        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }
}

