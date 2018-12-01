using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TrainController : MonoBehaviour {

	public bool enableMove = false;
	public bool enteringNextLevel = false;

	public float speed = 3.0f;
	public int routeSelected = -1;
	public int towardPointIndex = 0;
	public float distGap = 0.1f;

	public RoutePoints baseRoute;
	public RoutePoints aRoute;
	public RoutePoints bRoute;

	public List<Vector3> baseRoutePoints = new List<Vector3>();
	public List<Vector3> aRoutePoints = new List<Vector3>();
	public List<Vector3> bRoutePoints = new List<Vector3>();

	[Header("Timeline")]
	public PlayableDirector director;
	public TimelineAsset thridToOverview;
	public TimelineAsset overviewToThrid;
	public TimelineAsset overviewToFirst;
	public TimelineAsset thridToFirst;
	public TimelineAsset firstToOverview;
	public TimelineAsset firstToThrid;

	void UpdateRoutePoints() {
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

	private void Start() {
		UpdateRoutePoints();

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
						routeSelected = 1;
						enableMove = false;
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

			transform.LookAt(dist);
			transform.position += transform.forward * speed * Time.deltaTime;
		} else {
			if (Input.GetKeyDown(KeyCode.A)) {
				routeSelected = 0;
				enableMove = true;
				director.Play(overviewToThrid);
			}
			if (Input.GetKeyDown(KeyCode.B)) {
				routeSelected = 1;
				enableMove = true;
				director.Play(overviewToThrid);
			}
		}

	}

}