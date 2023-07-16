using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Plot : MonoBehaviour {
    
	[Header("References")]
	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private Color hoverColor;

	public GameObject towerObj;
	public TD_Turret turret;
	private Color startColor;

	private void Start() {
		startColor = sr.color;
	}

	private void OnMouseEnter() {
		sr.color = hoverColor;
	}

	private void OnMouseExit() {
		sr.color = startColor;
	}

	private void OnMouseDown() {
		if (UIManager.main.IsHoveringUI()) return;

		if (towerObj != null) {
			turret.OpenUpgradeUI();
			return;
		}

		TD_Tower towerToBuild = TD_BuildManager.main.GetSelectedTower();

		if (towerToBuild.cost > TD_LevelManager.main.currency) {
			Debug.Log("You can't afford this tower");
			return;
		}

		TD_LevelManager.main.SpendCurrency(towerToBuild.cost);

		towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
		turret = towerObj.GetComponent<TD_Turret>();
	}

}
