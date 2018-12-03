using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class FlyAway : MonoBehaviour {
	private float forcePower = 200f;
	public AudioClip[] audios;
	[HideInInspector] public AudioSource source;
	[HideInInspector] public Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		source = GetComponent<AudioSource>();
	}

	private void OnCollisionStay(Collision other) {
		if (other.gameObject.name == "Train") {
			// Debug.Log("Hit");
			rb.AddForce(forcePower * Random.rotation.eulerAngles.normalized);

			if (audios.Length > 0) {
				var clip = audios[Random.Range(0, audios.Length)];
				source.clip = clip;
				source.Play();
			}

		}
	}
}