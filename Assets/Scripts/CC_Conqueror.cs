using UnityEngine;

public class CC_Conqueror : MonoBehaviour {
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame;

    public System.Action killed;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}


