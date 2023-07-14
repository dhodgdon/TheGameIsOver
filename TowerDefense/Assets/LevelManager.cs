using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    
    public Transform[] startingpoint;
    public Transform[] path;

    public int currency;

    private void Start() {
        currency = 100;
    }

    public void IncreaseCurrency (int amount) {
        currency += amount;
    }
    
    public bool SpendCurrency(int ammount) {
        if (ammount <= currency) {
            currency -= ammount;
            return true;
        } else {
            Debug.Log("You don't have neough money");
            
            return false;
        }
    }
    private void Awake() {
        main = this;
    }

}
