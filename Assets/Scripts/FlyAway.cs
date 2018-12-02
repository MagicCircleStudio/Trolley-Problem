using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour {
	private float forcePower = 200f;
	[HideInInspector] public Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionStay(Collision other) {
		if (other.gameObject.name == "Train") {
			// Debug.Log("Hit");
			rb.AddForce(forcePower * Random.rotation.eulerAngles.normalized);
		}
	}
}