using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public Projectile laserPrefab;
    public float speed = 2.0f;

    private bool _laserActive;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    public bool controlsEnabled;



    private void Update() {
        if (controlsEnabled) {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                if(this.transform.position.x > -3.85f) {
                this.transform.position += Vector3.left  * this.speed * Time.deltaTime;
                }
            } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                if (this.transform.position.x < 3.85f) {
                this.transform.position += Vector3.right * this.speed * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                Shoot();
            }
        }
    }


    private void Shoot() {
        if (!_laserActive){
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position + 0.1f * Vector3.up, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed() {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
            other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }

}
