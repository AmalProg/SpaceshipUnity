using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public GameObject lifeTextObj;
	private Text lifeText;
	private string baseLifeText;
	public GameObject pointsTextObj;
	private Text pointsText;
	private string basePointsText;

	void Start ()
	{
		lifeText = lifeTextObj.GetComponent<Text> ();
		pointsText = pointsTextObj.GetComponent<Text> ();

		baseLifeText = lifeText.text;
		basePointsText = pointsText.text;
	}

	public void UpdatePoints(float points) {
		pointsText.text = basePointsText.Replace (",", points.ToString());
	}

	public void UpdateLife(int life) {
		lifeText.text = baseLifeText.Replace (",", life.ToString());
	}
}
