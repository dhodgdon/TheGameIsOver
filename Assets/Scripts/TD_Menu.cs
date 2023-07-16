using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TD_Menu : MonoBehaviour {
    
	[Header("References")]
	[SerializeField] TextMeshProUGUI currencyUI;
	[SerializeField] Animator anim;

	private bool isMenuOpen = true;

	public void ToggleMenu() {
		isMenuOpen = !isMenuOpen;
		anim.SetBool("MenuOpen", isMenuOpen);	
	}

	private void OnGUI() {
		currencyUI.text = TD_LevelManager.main.currency.ToString();
	}

}
