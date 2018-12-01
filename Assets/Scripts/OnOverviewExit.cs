using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OnOverviewExit : MonoBehaviour {

	public CinemachineVirtualCamera overviewCam;

	private void OnEnable() {
		Debug.Log("Exit Overview");
		overviewCam.Priority = 9;
	}
}