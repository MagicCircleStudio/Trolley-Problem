using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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

	public int totalKills = 0;

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

						Debug.Log("Play thrid to overview");
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
			// transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, RotateSpeed * Time.deltaTime);

			transform.LookAt(dist);
			// if (transform.eulerAngles.y - 179 > 0) {
			// transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
			// }

			transform.position += transform.forward * speed * Time.deltaTime;
		} else {

		}
		if (enablePressToBoost)
			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				pressToBoost = !pressToBoost;
				Debug.Log("Switch Boost");
				if (pressToBoost) StartBoost();
				else StopBoost();
			}

	}

}