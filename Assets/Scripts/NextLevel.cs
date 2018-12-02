using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

	public LevelManager levelManager;
	// public int level = -1;

	private void Start() {
		levelManager = FindObjectOfType<LevelManager>();
	}

	private void OnTriggerEnter(Collider other) {
		
		levelManager.GenerateNextLevel(transform.position, transform.forward, transform.rotation);
	}

}