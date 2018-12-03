using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class OnIronyDialog : MonoBehaviour {

	public CinemachineVirtualCamera overviewCam;
	public MenuController menu;
	public LevelManager levelManager;

	private void OnEnable() {
		Debug.Log("Enable a Irony dialog");
		
		menu.ShowDialog(levelManager.level.ironyDialog);
	}

	private void OnDisable() {
		Debug.Log("Disable a Irony dialog");
		menu.CloseDialog();
	}
}
