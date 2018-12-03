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
	public Level level;

	public GameObject overviewCM;
	public GameObject aRouteCM;
	public GameObject bRouteCM;
	public List<GameObject> lastLevelPool = new List<GameObject>();
	public List<GameObject> levelPool = new List<GameObject>();

	public TimelineAsset defalutOverview;
	public TimelineAsset abRouteView;

	public void GenerateGauges(Vector3 position, Vector3 forward, Quaternion rotation) {
		var gauge = Instantiate(gaugePrefab, position + forward * gaugeLength, rotation);
		levelPool.Clear();
		levelPool.Add(gauge);
	}

	public void AssignLevel(Level level) {
		// Debug.Log("Assign Level");
		// Debug.Log(level.gameObject.transform.position);
		this.level = level;
		if (level && level.enableBoost) {
			if (!trainController.pressToBoost)
				trainController.StopBoost();
		}

		trainController.thridToOverview = defalutOverview;
		overviewCM.transform.position = level.overviewCMAnchor.position;
		overviewCM.transform.rotation = level.overviewCMAnchor.rotation;
		if (level.aRouteCMAnchor && level.bRouteCMAnchor) {
			aRouteCM.transform.position = level.aRouteCMAnchor.position;
			aRouteCM.transform.rotation = level.aRouteCMAnchor.rotation;
			bRouteCM.transform.position = level.bRouteCMAnchor.position;
			bRouteCM.transform.rotation = level.bRouteCMAnchor.rotation;
			trainController.thridToOverview = abRouteView;
		}

		trainController.UpdateRoutePoints(level.baseRoute, level.aRoute, level.bRoute);
		trainController.enteringNextLevel = false;

		for (int i = lastLevelPool.Count - 1; i >= 0; i--) {
			var obj = lastLevelPool[i];
			lastLevelPool.RemoveAt(i);
			Destroy(obj);
		}
		lastLevelPool.Clear();
		levelPool.Clear();
		levelPool.Add(level.gameObject);
		lastLevelPool.AddRange(levelPool);
	}

	public Level GenerateLevelOnly(int levelIndex, Vector3 position, Vector3 forward, Quaternion rotation) {
		var levelObj = Instantiate(levels[levelIndex], position + forward * gaugeLength, rotation);
		if (level && level.enableBoost) {
			if (!trainController.pressToBoost)
				trainController.StopBoost();
		}

		return levelObj.GetComponent<Level>();
	}

	public void GenerateNextLevel(Vector3 position, Vector3 forward, Quaternion rotation) {
		currentLevelIndex++;
		if (currentLevelIndex >= levels.Length)
			currentLevelIndex = 0;
		var levelObj = Instantiate(levels[currentLevelIndex], position + forward * gaugeLength, rotation);
		levelPool.Add(levelObj);
		if (level && level.enableBoost) {
			if (!trainController.pressToBoost)
				trainController.StopBoost();
		}
		level = levelObj.GetComponent<Level>();

		trainController.thridToOverview = defalutOverview;
		overviewCM.transform.position = level.overviewCMAnchor.position;
		overviewCM.transform.rotation = level.overviewCMAnchor.rotation;
		if (level.aRouteCMAnchor && level.bRouteCMAnchor) {
			aRouteCM.transform.position = level.aRouteCMAnchor.position;
			aRouteCM.transform.rotation = level.aRouteCMAnchor.rotation;
			bRouteCM.transform.position = level.bRouteCMAnchor.position;
			bRouteCM.transform.rotation = level.bRouteCMAnchor.rotation;
			trainController.thridToOverview = abRouteView;
		}

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

	private void Update() {
		if (trainController.enableInput) {
			if (Input.GetKeyDown(KeyCode.A)) {
				trainController.routeSelected = 0;
				trainController.enableMove = true;
				if (level.enableBoost) {
					trainController.StartBoost();
					trainController.enablePressToBoost = true;
				}

				if (level.GetIronyDialog(0) != "") {
					trainController.director.Play(trainController.overviewToThridWithIrony);
				} else {
					trainController.director.Play(trainController.overviewToThrid);
				}

				trainController.enableInput = false;
			}
			if (Input.GetKeyDown(KeyCode.B)) {

				trainController.enableMove = true;
				if (level.switchBar) {
					level.SwitchAnimation();
					trainController.routeSelected = 1;
				} else {
					trainController.routeSelected = 0;
				}

				if (level.GetIronyDialog(0) != "") {
					trainController.director.Play(trainController.overviewToThridWithIrony);
				} else {
					trainController.director.Play(trainController.overviewToThrid);
				}

				trainController.enableInput = false;
			}
		}
	}

	private void Awake() {
		// GenerateGauges();
		// GenerateGauges(gaugeLength);
	}

}