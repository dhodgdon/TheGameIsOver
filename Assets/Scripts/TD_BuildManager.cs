using UnityEngine;

public class TD_BuildManager : MonoBehaviour {
	public static TD_BuildManager main;

	[Header("References")]
	[SerializeField] private TD_Tower[] towers;

	private int selectedTower = 0;

	private void Awake() {
		main = this;		
	}

	public TD_Tower GetSelectedTower() {
		return towers[selectedTower];
	}

	public void SetSelectedTower(int _selectedTower) {
		selectedTower = _selectedTower;
	}
}
