using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePoints : MonoBehaviour {
	public Transform[] points;

	public Color pointColor = Color.green;

	private void OnDrawGizmos() {
		Gizmos.color = pointColor;
		foreach (var item in points) {
			Gizmos.DrawSphere(item.position, 0.2f);
		}

	}
}