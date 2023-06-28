using UnityEngine;

public class Conqueror : MonoBehaviour {
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame;

    public System.Action killed;

    public ConquerorDeath explosion;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {  

            Instantiate(this.explosion, transform.position, Quaternion.identity);
            
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}


