using UnityEngine;
using System.Collections;
using System;

namespace TipGolf
{
public class MainMenuController : MonoBehaviour
{

	public GUIText timeText;
	private string[,] currentCourse = new string[,] { 
	      { "TileHole", "StAndrews/Andrews1", "Zig-Zag Box" }
	//, { "TileHole", "StAndrews/Right-Corner", "Right Corner" }
	    , { "TileHole", "StAndrews/Inchworm", "Inchworm" }
	    , { "TileHole", "StAndrews/Long-Run", "Long Run" }
	    , { "TileHole", "StAndrews/J", "J" }
	    , { "TileHole", "StAndrews/Bonk-Line", "Bonk/Line" }
	    , { "TileHole", "StAndrews/Gambit", "Gambit" }
	    , { "TileHole", "StAndrews/Maze", "Maze" }
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