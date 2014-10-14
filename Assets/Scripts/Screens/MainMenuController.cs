using UnityEngine;
using System.Collections;
using System;

namespace TipGolf
{
public class MainMenuController : MonoBehaviour
{

	public GUIText timeText;

	void Start()
	{
		var timeTaken = PlayerRound.CurrentRound.getCoursePlayerTime();
		if(timeTaken > 0.0f) {
			timeText.text = String.Format("Score: {0:0.00}s", timeTaken);
		}
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(60, 140, 300, 60), "New Game")) {
			var round = new PlayerRound("St. Andrews");
			PlayerRound.CurrentRound = round;
			Application.LoadLevel("Andrews1");
		}
	}
}
}