using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_Projectile : MonoBehaviour
{
    public Vector3 direction;

    public float speed;

    public System.Action destroyed;

    private void Update() {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }
//logic for when collision is detected system action is activated
    private void OnTriggerEnter2D(Collider2D other) {
        if (this.destroyed != null) {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
