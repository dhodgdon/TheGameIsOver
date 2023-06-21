using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;
    public bool isMoving;

    Vector2 min;
    Vector2 max;
    // Start is called before the first frame update
   void Awake()
    {
        isMoving = false;

        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //add the planet sprite half height to max y
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        //subtract planet sprite half height to get min y
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;
        Vector2 position = transform.position;
        // compute planet's current position 
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        //update planet's position
        transform.position = position;
        //if the planet gets to minimum y position. then stop moving planet
        if(transform.position.y < min.y)
        {
            isMoving = false;
        }
    }
    public void ResetPosition()
    {
        //reset position of planet to random x, and max y
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
