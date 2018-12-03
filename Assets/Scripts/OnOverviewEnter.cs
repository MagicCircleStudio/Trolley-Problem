using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OnOverviewEnter : MonoBehaviour {

	public CinemachineVirtualCamera overviewCam;
	public TrainController train;

	private void OnEnable() {
		// Debug.Log("Enter Overview");
		overviewCam.Priority = 11;
		train.enableInput = true;
	}
}