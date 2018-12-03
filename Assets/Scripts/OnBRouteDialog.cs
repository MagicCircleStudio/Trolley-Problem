using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OnBRouteDialog : MonoBehaviour {

	public CinemachineVirtualCamera overviewCam;
	public MenuController menu;
	public LevelManager levelManager;

	private void OnEnable() {
		Debug.Log("Enable a dialog");
		
		menu.ShowDialog(levelManager.level.bRouteDialog);
	}

	private void OnDisable() {
		Debug.Log("Disable a dialog");
		menu.CloseDialog();
	}
}