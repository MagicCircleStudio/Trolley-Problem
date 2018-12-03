using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

	public LevelManager levelManager;
	public MenuController menu;

	public int levelSelected = -1;
	public Transform levelEndPosition;

	private void Start() {
		if (!levelManager)
			levelManager = FindObjectOfType<LevelManager>();
		if (!menu)
			menu = FindObjectOfType<MenuController>();
	}

	private void OnTriggerEnter(Collider other) {
		if (levelSelected == -1) {
			levelManager.GenerateNextLevel(transform.position, transform.forward, transform.rotation);
			menu.CloseHintBoard();
		} else {
			var level = levelManager.GenerateLevelOnly(levelManager.currentLevelIndex, levelEndPosition.position, levelEndPosition.forward, levelEndPosition.rotation);
			levelEndPosition.GetComponent<ExitDetector>().levelToAssign = level;
			menu.CloseHintBoard();
		}

	}

}