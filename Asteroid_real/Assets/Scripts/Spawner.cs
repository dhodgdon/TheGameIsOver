using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Asteroid Asteroid;
    float spawnRate = 2.0f;
    float spawnDistance = 14f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 0f, spawnRate);
    }

    // Update is called once per frame
    void spawn()
        //get spawn point
    {
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;
        //get rotation
        float angle = Random.Range(-15f, 15f);
       // a rotation which rotates angle degrees around z-axis
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        //create new asteroid
        Asteroid theAsteroid = Instantiate(Asteroid, spawnPoint, rotation);
        // direction and size
        Vector2 direction = rotation * -spawnPoint;//Quaternion * Vector / Quaternion
        float mass = Random.Range(0.8f, 1.4f);
        theAsteroid.kick(mass, direction);
    }
    
    void Update()
    {
        
    }
}
