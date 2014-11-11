using UnityEngine;
using System.Collections;
using System;

namespace TipGolf
{
public class MainMenuController : MonoBehaviour
{
	public Texture logoTexture;
	public Texture backgroundTexture;
	public GUISkin skin;
	public ScreenRegion logoRegion;
	public ScreenRegion playButtonRegion;
	public ScreenRegion timeTakenRegion;
	private float timeTaken;
	public Font timeFont;
	public Material whiteFontMaterial;
	private string[,] currentCourse = new string[,] 
	    { { "TileHole", "StAndrews/Andrews1", "Zig-Zag Box" }
	    , { "TileHole", "StAndrews/Right-Corner", "Right Corner" }
	/*, { "TileHole", "StAndrews/Inchworm", "Inchworm" }
	    , { "TileHole", "StAndrews/Long-Run", "Long Run" }
	    , { "TileHole", "StAndrews/J", "J" }
	    , { "TileHole", "StAndrews/Bonk-Line", "Bonk/Line" }
	    , { "TileHole", "StAndrews/Gambit", "Gambit" }
	    , { "TileHole", "StAndrews/Maze", "Maze" } */
			};

	void Start()
	{
		
		timeTaken = PlayerRound.CurrentRound.getCoursePlayerTime();
		
		if(!backgroundTexture) {
			Debug.LogError("Assign a background texture in the editor");
		}
		if(logoRegion == null) {
			Debug.LogError("Assign logo region in the editor");
		}
		if(playButtonRegion == null) {
			Debug.LogError("Assign play button region in the editor");
		}
		if(timeTakenRegion == null) {
			Debug.LogError("Assign time taken button region in the editor");
		}
	}

	void OnGUI()
	{
		GUI.skin = skin;
	
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture, ScaleMode.StretchToFill, false);
		
		GUI.DrawTexture(logoRegion.getRect(), logoTexture, ScaleMode.ScaleToFit, true);
		
		if(GUI.Button(playButtonRegion.getRect(), "New Game")) {
			var round = new PlayerRound("St. Andrews", currentCourse);
			PlayerRound.CurrentRound = round;
			Application.LoadLevel(currentCourse[0, 0]);
		}
		
		if(timeTaken > 0.0f) {
			GUI.Label(timeTakenRegion.getRect(), String.Format("That took {0:0.00}s.\nGood job!", timeTaken));
		}
	}
}
}