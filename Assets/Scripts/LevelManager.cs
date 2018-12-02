using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LevelManager : MonoBehaviour {

	public Transform train;
	public TrainController trainController;
	public GameObject gaugePrefab;
	public float gaugeLength = 40;

	public Vector3 lastGaugePosition;

	public GameObject[] levels;
	public int currentLevelIndex = -1;
	public Level currentLevel;

	public GameObject overviewCM;
	public GameObject aRouteCM;
	public GameObject bRouteCM;
	public List<GameObject> lastLevelPool = new List<GameObject>();
	public List<GameObject> levelPool = new List<GameObject>();

	public TimelineAsset defaultOverview;
	public TimelineAsset abRouteView;

	public void GenerateGauges(Vector3 position, Vector3 forward, Quaternion rotation) {
		var gauge = Instantiate(gaugePrefab, position + forward * gaugeLength, rotation);
		levelPool.Clear();
		levelPool.Add(gauge);
	}

	public void GenerateNextLevel(Vector3 position, Vector3 forward, Quaternion rotation) {
		currentLevelIndex++;
		var levelObj = Instantiate(levels[currentLevelIndex], position + forward * gaugeLength, rotation);
		levelPool.Add(levelObj);
		if (currentLevel && currentLevel.enableBoost) {
			if (!trainController.pressToBoost)
				trainController.StopBoost();
		}
		currentLevel = levelObj.GetComponent<Level>();

		trainController.thridToOverview = defaultOverview;
		overviewCM.transform.position = currentLevel.overviewCMAnchor.position;
		overviewCM.transform.rotation = currentLevel.overviewCMAnchor.rotation;
		if (currentLevel.aRouteCMAnchor && currentLevel.bRouteCMAnchor) {
			aRouteCM.transform.position = currentLevel.aRouteCMAnchor.position;
			aRouteCM.transform.rotation = currentLevel.aRouteCMAnchor.rotation;
			bRouteCM.transform.position = currentLevel.bRouteCMAnchor.position;
			bRouteCM.transform.rotation = currentLevel.bRouteCMAnchor.rotation;
			trainController.thridToOverview = abRouteView;
		}

		trainController.UpdateRoutePoints(currentLevel.baseRoute, currentLevel.aRoute, currentLevel.bRoute);
		trainController.enteringNextLevel = false;

		for (int i = lastLevelPool.Count - 1; i >= 0; i--) {
			var obj = lastLevelPool[i];
			lastLevelPool.RemoveAt(i);
			Destroy(obj);
		}
		lastLevelPool.Clear();
		lastLevelPool.AddRange(levelPool);

	}

	private void Update() {
		if (trainController.enableInput) {
			if (Input.GetKeyDown(KeyCode.A)) {
				trainController.routeSelected = 0;
				trainController.enableMove = true;
				if (currentLevel.enableBoost) {
					trainController.StartBoost();
					trainController.enablePressToBoost = true;
				}
				trainController.director.Play(trainController.overviewToThrid);
				trainController.enableInput = false;
			}
			if (Input.GetKeyDown(KeyCode.B)) {
				trainController.routeSelected = 1;
				trainController.enableMove = true;
				currentLevel.Switch();
				trainController.director.Play(trainController.overviewToThrid);
				trainController.enableInput = false;
			}
		}
	}

	private void Awake() {
		// GenerateGauges();
		// GenerateGauges(gaugeLength);
	}

}