using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject[] Planets; // Array of planet prefabs

    // Queue to hold the planets
    Queue<GameObject> availablePlanets = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Enqueue planets by array index
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        // Call MovePlanetDown function every 20 seconds
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any necessary logic here
    }

    // Function to dequeue the planets
    void MovePlanetDown()
    {
        // Enqueue planets that are below the screen
        EnqueuePlanets();

        // If queue is empty, return
        if (availablePlanets.Count == 0)
            return;

        // Get planet from the queue
        GameObject aPlanet = availablePlanets.Dequeue();

        // Set the planet's isMoving property to true
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    // Function to enqueue planets that are below the screen
    void EnqueuePlanets()
    {
        foreach (GameObject planet in Planets)
        {
            // If planet is below the screen and the planet is not moving
            if (planet.transform.position.y < 0 || (!planet.GetComponent<Planet>().isMoving))
            {
                planet.GetComponent<Planet>().ResetPosition();
                // Enqueue available planets
                availablePlanets.Enqueue(planet);
            }
        }
    }
}
