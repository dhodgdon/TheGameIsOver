using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitpoints = 2;
    [SerializeField] private int curencyWorth = 50;
    private bool isDestroyed = false;

    public void TakeDamage(int dmg) {
        hitpoints -= dmg;

        if (hitpoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(curencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}

