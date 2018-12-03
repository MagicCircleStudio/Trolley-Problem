using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PostProcessing;
using UnityEngine.Timeline;

public class TrainController : MonoBehaviour {

	public bool enableMove = false;
	public bool enableInput = false;
	public bool enteringNextLevel = false;

	public float speed = 3.0f;
	public float RotateSpeed = 300f;
	public int routeSelected = -1;
	public int towardPointIndex = 0;
	public float distGap = 0.5f;

	public int totalKills {
		get { return _totalKills; }
		set {
			_totalKills = value;
			var vSettings = ppProfile.vignette.settings;
			float v = (float) _totalKills / 50.0f;
			// Debug.Log(_totalKills);
			vSettings.color = new Color(Mathf.Min(v, 1f), 0f, 0f);
			// Debug.Log(vSettings.color);
			ppProfile.vignette.settings = vSettings;
		}
	}
	private int _totalKills = 0;

	[HideInInspector] public List<Vector3> baseRoutePoints = new List<Vector3>();
	[HideInInspector] public List<Vector3> aRoutePoints = new List<Vector3>();
	[HideInInspector] public List<Vector3> bRoutePoints = new List<Vector3>();

	[Header("Timeline")]
	public PlayableDirector director;
	public TimelineAsset thridToOverview;
	// public TimelineAsset overviewTodetail;
	public TimelineAsset overviewToThrid;
	public TimelineAsset overviewToThridWithIrony;
	// public TimelineAsset overviewToFirst;
	// public TimelineAsset thridToFirst;
	// public TimelineAsset firstToOverview;
	// public TimelineAsset firstToThrid;

	[Header("Effects")]
	public ParticleSystem boostingFire;
	public bool enablePressToBoost = false;
	public bool pressToBoost = false;
	public PostProcessingBehaviour ppBahaviour;
	public PostProcessingProfile ppProfile;

	public void UpdateRoutePoints(RoutePoints baseRoute, RoutePoints aRoute, RoutePoints bRoute) {
		baseRoutePoints.Clear();
		foreach (var item in baseRoute.points) {
			baseRoutePoints.Add(item.position);
		}

		aRoutePoints.Clear();
		foreach (var item in aRoute.points) {
			aRoutePoints.Add(item.position);
		}

		bRoutePoints.Clear();
		foreach (var item in bRoute.points) {
			bRoutePoints.Add(item.position);
		}
	}

	public void StartBoost() {
		speed = 25;
		boostingFire.Play();
	}

	public void StopBoost() {
		speed = 15;
		boostingFire.Stop();
	}

	private void Start() {
		boostingFire.Stop();
		ppProfile = ppBahaviour.profile;
		var vSettings = ppProfile.vignette.settings;
		vSettings.color = new Color(0f, 0f, 0f);
		ppProfile.vignette.settings = vSettings;
	}

	private void Update() {
		if (enableMove) {
			Vector3 dist = Vector3.zero;
			if (enteringNextLevel) {
				dist = transform.position + transform.forward;
			} else if (routeSelected == -1) {
				if ((baseRoutePoints[towardPointIndex] - transform.position).magnitude < distGap) {
					towardPointIndex++;
					if (towardPointIndex >= baseRoutePoints.Count) {
						dist = transform.position + transform.forward;
						towardPointIndex = 0;
						routeSelected = 0;
						enableMove = false;

						// Debug.Log("Play thrid to overview");
						director.Play(thridToOverview);
					} else {
						dist = baseRoutePoints[towardPointIndex];
					}
				} else {
					dist = baseRoutePoints[towardPointIndex];
				}

			} else if (routeSelected == 0) {
				if ((aRoutePoints[towardPointIndex] - transform.position).magnitude < distGap) {
					towardPointIndex++;
					if (towardPointIndex >= aRoutePoints.Count) {
						dist = transform.position + transform.forward;
						towardPointIndex = 0;
						routeSelected = -1;
						enteringNextLevel = true;
					} else {
						dist = aRoutePoints[towardPointIndex];
					}
				} else {
					dist = aRoutePoints[towardPointIndex];
				}
			} else if (routeSelected == 1) {
				if ((bRoutePoints[towardPointIndex] - transform.position).magnitude < distGap) {
					towardPointIndex++;
					if (towardPointIndex >= bRoutePoints.Count) {
						dist = transform.position + transform.forward;
						towardPointIndex = 0;
						routeSelected = -1;
						enteringNextLevel = true;
					} else {
						dist = bRoutePoints[towardPointIndex];
					}
				} else {
					dist = bRoutePoints[towardPointIndex];
				}
			}

			// Vector3 rotateVector = dist - transform.position;
			// Quaternion newRotation = Quaternion.LookRotation(rotateVector);
			// if (newRotation.eulerAngles.y - 160 > 0) {
			// 	newRotation = Quaternion.Euler(-90 - 2 * (90 + newRotation.eulerAngles.x), newRotation.eulerAngles.y - 180, newRotation.eulerAngles.z);
			// }
			// transform.rotation = newRotation;
			// transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, RotateSpeed * Time.deltaTime);

			transform.LookAt(dist);

			// if (transform.eulerAngles.y - 160 > 0) {
			// 	transform.rotation = Quaternion.Euler(-90 - 2 * (90 + transform.eulerAngles.x), transform.eulerAngles.y - 180, transform.eulerAngles.z);
			// }

			transform.position += transform.forward * speed * Time.deltaTime;
		} else {

		}
		if (enablePressToBoost)
			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				pressToBoost = !pressToBoost;
				// Debug.Log("Switch Boost");
				if (pressToBoost) StartBoost();
				else StopBoost();
			}

	}

}