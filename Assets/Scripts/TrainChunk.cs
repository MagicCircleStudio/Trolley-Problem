using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainChunk : MonoBehaviour {

	public TrainController trainHead;
	private Vector3 trainHeadGap;

	public int routeSelected = -1;
	public int towardPointIndex = 0;

	private void Awake() {
		trainHeadGap = transform.position - trainHead.transform.position;
	}

	private void Update() {
		if (trainHead.enableMove) {
			Vector3 dist = Vector3.zero;
			if (trainHead.enteringNextLevel) {
				dist = transform.position + transform.forward;
			} else if (routeSelected == -1) {
				if ((trainHead.baseRoutePoints[towardPointIndex] - transform.position).magnitude < trainHead.distGap) {
					towardPointIndex++;
					if (towardPointIndex >= trainHead.baseRoutePoints.Count) {
						dist = transform.position + transform.forward;
						towardPointIndex = 0;
						routeSelected = trainHead.routeSelected;
					} else {
						dist = trainHead.baseRoutePoints[towardPointIndex];
					}
				} else {
					dist = trainHead.baseRoutePoints[towardPointIndex];
				}

			} else if (routeSelected == 0) {
				if ((trainHead.aRoutePoints[towardPointIndex] - transform.position).magnitude < trainHead.distGap) {
					towardPointIndex++;
					if (towardPointIndex >= trainHead.aRoutePoints.Count) {
						dist = transform.position + transform.forward;
						towardPointIndex = 0;
						routeSelected = trainHead.routeSelected;
					} else {
						dist = trainHead.aRoutePoints[towardPointIndex];
					}
				} else {
					dist = trainHead.aRoutePoints[towardPointIndex];
				}
			} else if (routeSelected == 1) {
				if ((trainHead.bRoutePoints[towardPointIndex] - transform.position).magnitude < trainHead.distGap) {
					towardPointIndex++;
					if (towardPointIndex >= trainHead.bRoutePoints.Count) {
						dist = transform.position + transform.forward;
						towardPointIndex = 0;
						routeSelected = trainHead.routeSelected;
					} else {
						dist = trainHead.bRoutePoints[towardPointIndex];
					}
				} else {
					dist = trainHead.bRoutePoints[towardPointIndex];
				}
			}

			transform.LookAt(dist);
			transform.position += transform.forward * trainHead.speed * Time.deltaTime;
		} else {
			transform.position = trainHead.transform.position + trainHeadGap;
		}

	}
}