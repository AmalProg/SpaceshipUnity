using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {

	public GameObject bestScoreObj;
	public GameObject finalScoreTextObj;
	private Text finalScoreText;

	// Use this for initialization
	void Start () {
		finalScoreText = finalScoreTextObj.GetComponent<Text> ();
		finalScoreText.text = GlobalData.finalScore.ToString();

		if (GlobalData.finalScore > GlobalData.bestScore) {
			GlobalData.bestScore = GlobalData.finalScore;

			bestScoreObj.SetActive (true);
		}			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReturnGameMenu() {
		SceneLoader.LoadScene ("GameMenuScene");
	}
}
