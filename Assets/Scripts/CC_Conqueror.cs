using UnityEngine;

public class CC_Conqueror : MonoBehaviour {
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame;

    public System.Action killed;

    public ConquerorDeath explosion;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

//logic for making explosions occur when the conqueror is shot by the missle.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("CC_missile")) {  

            Instantiate(this.explosion, transform.position, Quaternion.identity);
            
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}


