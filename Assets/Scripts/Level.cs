using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Level : MonoBehaviour {

	public RoutePoints baseRoute;
	public RoutePoints aRoute;
	public RoutePoints bRoute;

	public int aRouteKills = 0;
	public int bRouteKills = 0;

	// public int languangeChoice = 0; // 0:EN 1:CN 2:JP

	public string GetARouteDialog(int languangeChoice) {
		switch (languangeChoice) {
			case 1:
				return aRouteDialogCN;
			case 2:
				return aRouteDialogJP;
			default:
				return aRouteDialogEN;
		}

	}
	public string GetBRouteDialog(int languangeChoice) {
		switch (languangeChoice) {
			case 1:
				return bRouteDialogCN;
			case 2:
				return bRouteDialogJP;
			default:
				return bRouteDialogEN;
		}

	}

	public string GetIronyDialog(int languangeChoice) {
		switch (languangeChoice) {
			case 1:
				return ironyDialogCN;
			case 2:
				return ironyDialogJP;
			default:
				return ironyDialogEN;
		}

	}

	// public string aRouteDialog {
	// 	get {
	// 		switch (languangeChoice) {
	// 			case 1:
	// 				return aRouteDialogCN;
	// 			case 2:
	// 				return aRouteDialogJP;
	// 			default:
	// 				return aRouteDialogEN;
	// 		}
	// 	}
	// }
	// public string bRouteDialog {
	// 	get {
	// 		switch (languangeChoice) {
	// 			case 1:
	// 				return bRouteDialogCN;
	// 			case 2:
	// 				return bRouteDialogJP;
	// 			default:
	// 				return bRouteDialogEN;
	// 		}
	// 	}
	// }
	// public string ironyDialog {
	// 	get {
	// 		switch (languangeChoice) {
	// 			case 1:
	// 				return ironyDialogCN;
	// 			case 2:
	// 				return ironyDialogJP;
	// 			default:
	// 				return ironyDialogEN;
	// 		}
	// 	}
	// }

	public string aRouteDialogEN;
	public string bRouteDialogEN;
	public string ironyDialogEN;

	public string aRouteDialogCN;
	public string bRouteDialogCN;
	public string ironyDialogCN;

	public string aRouteDialogJP;
	public string bRouteDialogJP;
	public string ironyDialogJP;

	public Transform overviewCMAnchor;
	public Transform aRouteCMAnchor;
	public Transform bRouteCMAnchor;

	public Transform switchBar;

	public bool enableBoost = false;
	private bool enableSwitch = false;

	public void SwitchAnimation() {
		enableSwitch = true;
	}

	private void Update() {
		if (enableSwitch) {
			switchBar.Rotate(0f, 0f, -72f * 3 * Time.deltaTime);
			// Debug.Log(switchBar.localEulerAngles.z);

			if (switchBar.localEulerAngles.z < 360f - 36f && switchBar.localEulerAngles.z > 50f) {
				enableSwitch = false;
			}

		}
	}

}