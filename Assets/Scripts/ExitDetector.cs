using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDetector : MonoBehaviour {

	public LevelManager levelManager;
	public int choiceType = -1;

	public MenuController menu;
	public string hintBoardStr;

	public Level levelToAssign;

	private void Start() {
		if (!levelManager)
			levelManager = FindObjectOfType<LevelManager>();
		if (!menu)
			menu = FindObjectOfType<MenuController>();
	}

	private void OnTriggerEnter(Collider other) {
		// Debug.Log("Choice Make: " + choiceType);

		if (hintBoardStr != "") {
			menu.ShowHintBoard(hintBoardStr);
		}

		if (choiceType == 0) {
			levelManager.trainController.totalKills += levelManager.level.aRouteKills;
			// Debug.Log("total kills: " + levelManager.trainController.totalKills);

			// Debug.Log(Network.queryPage("update_level", levelManager.currentLevelIndex.ToString(), "0"));
			// 
			levelManager.GenerateGauges(transform.position, transform.forward, transform.rotation);

		} else if (choiceType == 1) {
			levelManager.trainController.totalKills += levelManager.level.bRouteKills;
			// Debug.Log("total kills: " + levelManager.trainController.totalKills);

			// Debug.Log(Network.queryPage("update_level", levelManager.currentLevelIndex.ToString(), "0"));
			// menu.ShowHintBoard("60% people make same choices with you");
			levelManager.GenerateGauges(transform.position, transform.forward, transform.rotation);

		} else if (choiceType == -2) {
			levelManager.trainController.totalKills += levelManager.level.bRouteKills;
			// Debug.Log("total kills: " + levelManager.trainController.totalKills);

			// Debug.Log(Network.queryPage("update_level", levelManager.currentLevelIndex.ToString(), "0"));
			// menu.ShowHintBoard("60% people make same choices with you");

			levelManager.AssignLevel(levelToAssign);
			// levelManager.GenerateGauges(transform.position, transform.forward, transform.rotation);
		}

	}

}