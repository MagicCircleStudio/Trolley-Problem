using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDetector : MonoBehaviour {

	public LevelManager levelManager;
	public int choiceType = -1;

	private void Start() {
		levelManager = FindObjectOfType<LevelManager>();
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Choice Make: " + choiceType);
		levelManager.GenerateGauges(transform.position, transform.forward, transform.rotation);
	}

}
