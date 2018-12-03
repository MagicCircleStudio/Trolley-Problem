using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OnOverviewExit : MonoBehaviour {

	public CinemachineVirtualCamera overviewCam;
	// public TrainController train;

	private void OnEnable() {
		// Debug.Log("Exit Overview");
		overviewCam.Priority = 9;

		// train.enableInput = false;
	}
}