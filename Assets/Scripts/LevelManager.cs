using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public Transform train;
	public TrainController trainController;
	public GameObject gaugePrefab;
	public float gaugeLength = 40;

	public Vector3 lastGaugePosition;

	public GameObject[] levels;
	public int currentLevel = -1;

	public GameObject overviewCM;
	public List<GameObject> lastLevelPool = new List<GameObject>();
	public List<GameObject> levelPool = new List<GameObject>();

	public void GenerateGauges(Vector3 position, Vector3 forward, Quaternion rotation) {
		var gauge = Instantiate(gaugePrefab, position + forward * gaugeLength, rotation);
		levelPool.Clear();
		levelPool.Add(gauge);
	}

	public void GenerateNextLevel(Vector3 position, Vector3 forward, Quaternion rotation) {
		currentLevel++;
		var levelObj = Instantiate(levels[currentLevel], position + forward * gaugeLength, rotation);
		levelPool.Add(levelObj);
		var level = levelObj.GetComponent<Level>();
		overviewCM.transform.position = level.cMAnchor.position;
		overviewCM.transform.rotation = level.cMAnchor.rotation;
		trainController.UpdateRoutePoints(level.baseRoute, level.aRoute, level.bRoute);
		trainController.enteringNextLevel = false;

		for (int i = lastLevelPool.Count - 1; i >= 0; i--) {
			var obj = lastLevelPool[i];
			lastLevelPool.RemoveAt(i);
			Destroy(obj);
		}
		lastLevelPool.Clear();
		lastLevelPool.AddRange(levelPool);

	}

	private void Awake() {
		// GenerateGauges();
		// GenerateGauges(gaugeLength);
	}

}