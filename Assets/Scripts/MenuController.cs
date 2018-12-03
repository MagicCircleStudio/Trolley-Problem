using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject menuCorner;
	public GameObject sideMenu;
	public GameObject dialog;
	public Text dialogText;
	public GameObject hintBoard;
	public Text hintBoardText;

	public int languageChoice = 0; //EN:0 CN:1 JP:2
	public Image languageIcon;
	public Sprite enIcon;
	public Sprite cnIcon;
	public Sprite jpIcon;

	public void OnSwitchLanguage() {
		languageChoice = (languageChoice + 1) % 3;
		switch (languageChoice) {
			case 0:
				languageIcon.sprite = enIcon;
				break;
			case 1:
				languageIcon.sprite = cnIcon;
				break;
			case 2:
				languageIcon.sprite = jpIcon;
				break;
			default:
				languageIcon.sprite = enIcon;
				break;
		}
	}

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

	public void CloseHintBoard() {
		hintBoard.SetActive(false);
	}

	public void ShowHintBoard(string str) {
		if (str != "") {
			hintBoardText.text = str;
			hintBoard.SetActive(true);
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
		hintBoard.SetActive(false);
	}

	private void Update() {

	}

}