using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK_Barrel : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed = 2f;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rigidbody.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("DestroyBarrelBarrier"))
        {
            Destroy(this.gameObject);
        }
    }
}
