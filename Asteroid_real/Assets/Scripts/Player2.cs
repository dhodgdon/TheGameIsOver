using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Bullet2 Bullet2;
    public float speed;
void Start() 
    {

    }
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet2 theBullet2 = Instantiate(Bullet2, transform.position, Quaternion.identity);
            theBullet2.fire(transform.up);
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }
    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;
        //calculate new position
        pos += direction * speed * Time.deltaTime;
        //make sure the new position isn't outside of the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        //update player's position
        transform.position = pos;
    }
}
