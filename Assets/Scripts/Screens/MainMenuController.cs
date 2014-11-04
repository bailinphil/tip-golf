using UnityEngine;
using System.Collections;
using System;

namespace TipGolf
{
public class MainMenuController : MonoBehaviour
{
	public GUIText timeText;
	public Texture logoTexture;
	public Texture backgroundTexture;
	public GUIStyle buttonStyle;
	public ScreenRegion logoRegion;
	public ScreenRegion playButtonRegion;
	private string[,] currentCourse = new string[,] 
	    { { "TileHole", "StAndrews/Andrews1", "Zig-Zag Box" }
	    , { "TileHole", "StAndrews/Right-Corner", "Right Corner" }
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
		if(!backgroundTexture) {
			Debug.LogError("Assign a background texture in the editor");
		}
		if(logoRegion == null) {
			Debug.LogError("Assign logo region in the editor");
		}
		if(playButtonRegion == null) {
			Debug.LogError("Assign play button region in the editor");
		}
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture, ScaleMode.StretchToFill, false);
		
		GUI.DrawTexture(logoRegion.getRect(), logoTexture, ScaleMode.ScaleToFit, true);
		
		if(GUI.Button(playButtonRegion.getRect(), "New Game", buttonStyle)) {
			var round = new PlayerRound("St. Andrews", currentCourse);
			PlayerRound.CurrentRound = round;
			Application.LoadLevel(currentCourse[0, 0]);
		}
	}
}
}