using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_LevelManager : MonoBehaviour {
    
	public static TD_LevelManager main;

	public Transform startPoint;
	public Transform[] path;

	public int currency;

	[SerializeField] private int startingCurrency = 100;

	private void Awake() {
		main = this;
	}

	private void Start() {
		currency = startingCurrency;
	}

	public void IncreaseCurrency(int amount) {
		currency += amount;
	}

	public void DecreaseCurrency(int amount) {
		currency -= amount;
	}

	public bool SpendCurrency(int amount) {
		if (amount == 1000 && currency >= 1000) {
			FindObjectOfType<AK_GameManager>().LevelComplete();
		}
		if (amount <= currency){
			currency -= amount;
			return true;
		} else {
			Debug.Log("You do not have enough to purchase this item");
			return false;
		}
	}

}
