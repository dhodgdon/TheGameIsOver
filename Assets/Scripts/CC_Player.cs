using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CC_Player : MonoBehaviour
{
//connects missile prefab to player
    public CC_Projectile missilePrefab;
    public float speed = 2.0f;

    private bool _missileActive;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    public bool controlsEnabled;



    private void Update() {

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (controlsEnabled) {
            
            // move player to the left of the screen
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                if(this.transform.position.x > leftEdge.x) {
                this.transform.position += Vector3.left  * this.speed * Time.deltaTime;
                }
            //move player to the right of the screen
            } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                if (this.transform.position.x < rightEdge.x) {
                this.transform.position += Vector3.right * this.speed * Time.deltaTime;
                }
            }
            //key input for player to shoot
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                Shoot();
            }
        }
    }

//logic for player to shoot
    private void Shoot() {
        if (!_missileActive){
            CC_Projectile projectile = Instantiate(this.missilePrefab, this.transform.position + 0.1f * Vector3.up, Quaternion.identity);
            projectile.destroyed += MissileDestroyed;
            _missileActive = true;
        }
    }
//missile not active, missile will not fire if missile is not visible
    private void MissileDestroyed() {
        _missileActive = false;
    }
//if player is destroyed, Cosmic Conquerors restarts.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("CC_conqueror") ||
            other.gameObject.layer == LayerMask.NameToLayer("CC_laser")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }

}
