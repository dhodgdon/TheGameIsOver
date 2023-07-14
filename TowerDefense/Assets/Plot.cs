using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start() {
        startColor = sr.color;        
    }

    private void OnMouseEnter() {
        sr.Color = hoverColor;    
    }

    private void OnMouseExit() {
        sr.Color = startColor;
    }

    private void OnMouseDown() {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
    }
}