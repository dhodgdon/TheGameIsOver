using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConquerorDeath : MonoBehaviour
{

    public Sprite[] explosionFrames;

    public float animationTime;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animationFrame = 0;
    }

    void Start() {
        InvokeRepeating(nameof(Animate), animationTime, animationTime);
    }

    private void Animate() {
        _spriteRenderer.sprite = this.explosionFrames[_animationFrame];
        _animationFrame++;
        if (_animationFrame == this.explosionFrames.Length) {
            Destroy(this.gameObject);
        }
    }
}
