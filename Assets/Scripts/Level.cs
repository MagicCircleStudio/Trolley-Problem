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

	public string aRouteDialog;
	public string bRouteDialog;
	public string ironyDialog;

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