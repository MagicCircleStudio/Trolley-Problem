using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OnOverviewEnter : MonoBehaviour {

	public CinemachineVirtualCamera overviewCam;

	private void OnEnable() {
		Debug.Log("Enter Overview");
		overviewCam.Priority = 11;
	}
}