using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get current position of star
        Vector2 position = transform.position;
        //compute star's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update star's position
        transform.position = position;
        //Bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //if star goes outside of the screen on the bottom, 
        // then position the star on the top edge of the screen
        // and randomly between the left and right side of the screen
        if(transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
