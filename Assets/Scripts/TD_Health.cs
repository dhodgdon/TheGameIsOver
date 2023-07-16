using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Health : MonoBehaviour {
    
	[Header("Attributes")]
	[SerializeField] private int hitPoints = 2;
	[SerializeField] private int currencyWorth = 50;

	private bool isDestroyed = false;

	public void TakeDamage(int dmg) {
		hitPoints -= dmg;

		if (hitPoints <= 0 && !isDestroyed) {
			TD_EnemySpawner.onEnemyDestroy.Invoke();
			TD_LevelManager.main.IncreaseCurrency(currencyWorth);
			isDestroyed = true;
			Destroy(gameObject);
		}
	}

}
