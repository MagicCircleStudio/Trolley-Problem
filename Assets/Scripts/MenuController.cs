﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject menuCorner;
	public GameObject sideMenu;
	public GameObject dialog;
	public Text dialogText;

	public void OnMenuButtonPressed() {
		// Debug.Log("Menu");
		menuCorner.SetActive(false);
		sideMenu.SetActive(true);

		Time.timeScale = 0;
		// var rect = menuCorner.GetComponent<RectTransform>();
	}

	public void OnSideMenuResumePressed() {
		menuCorner.SetActive(true);
		sideMenu.SetActive(false);

		Time.timeScale = 1;
	}

	public void ShowDialog(string str) {
		if (str != "") {
			dialogText.text = str;
			dialog.SetActive(true);
		}
	}

	public void CloseDialog() {
		dialog.SetActive(false);
	}

	private void Awake() {
		menuCorner.SetActive(true);
		sideMenu.SetActive(false);
		// dialogText.text = "君の名は？你的名字是？What's your name?";
		dialog.SetActive(false);
	}

	private void Update() {

	}

}