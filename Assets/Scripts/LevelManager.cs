using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public Transform train;
	public TrainController trainController;
	public GameObject gaugePrefab;
	public float gaugeLength = 40;

	public Vector3 lastGaugePosition;

	public void EnterNextLevel() {
		GenerateGauges(gaugeLength / 2);
	}

	public void GenerateGauges(float dist = 0) {
		lastGaugePosition = train.position + train.forward * dist;
		Instantiate(gaugePrefab, lastGaugePosition, train.rotation);
	}
	public void GenerateGauges(Vector3 position, Vector3 forward, Quaternion rotation) {
		Instantiate(gaugePrefab, position + forward * gaugeLength, rotation);
	}

	private void Awake() {
		// GenerateGauges();
		// GenerateGauges(gaugeLength);
	}

}