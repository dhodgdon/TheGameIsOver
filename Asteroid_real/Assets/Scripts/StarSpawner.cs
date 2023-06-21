using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject Star;//star prefab
    public int MaxStars;//maximum number of stars

    // array of colors
    Color[] starColors =
    {
        new Color(0.5f,0.5f, 1f),
        new Color(0, 1f, 1f),
        new Color(1f,1f,0),
        new Color(1f,0,0),
    };
    // Start is called before the first frame update
    void Start()
    {
        //bottom left of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top right of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        for(int count = 0; count < MaxStars; count++)
        {
            GameObject star = (GameObject)Instantiate(Star);
            //set star color
            star.GetComponent<SpriteRenderer>().color = starColors[count % starColors.Length];
            //set position of the star (random x and random y)
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            // set random speed for star
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);
            // make the star a child of the starSpawwner
            star.transform.parent = transform;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
