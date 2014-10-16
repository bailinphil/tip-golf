using UnityEngine;
using System.Collections;
using System;

namespace TipGolf
{
public class MainMenuController : MonoBehaviour
{

	public GUIText timeText;
	private string[,] currentCourse = new string[4, 3] { 
	      { "TileHole", "StAndrews/Andrews3", "Zig-Zag Box" }
			, { "TileHole", "StAndrews/Andrews3", "Right Bend" }
			, { "TileHole", "StAndrews/Andrews3", "Alley Hook" }
			, { "TileHole", "StAndrews/Andrews3", "Final Hole" }
			};

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
			var round = new PlayerRound("St. Andrews", currentCourse);
			PlayerRound.CurrentRound = round;
			Application.LoadLevel(currentCourse[0, 0]);
		}
	}
}
}