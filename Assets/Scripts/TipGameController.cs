using UnityEngine;
using System.Collections;
using Lib;

namespace TipGolf
{

public class TipGameController : MonoBehaviour
{

	public static TipGameController instance;

	private string[,] currentCourse = new string[,] 
		{ { "TileHole", "StAndrews/Andrews1", "Zig-Zag Box" }
		, { "TileHole", "StAndrews/Right-Corner", "Right Corner" }
		, { "TileHole", "StAndrews/Inchworm", "Inchworm" }
		, { "TileHole", "StAndrews/Long-Run", "Long Run" }
		, { "TileHole", "StAndrews/J", "J" }
		, { "TileHole", "StAndrews/Net", "Safety Net" }
		, { "TileHole", "StAndrews/Bonk-Line", "Bonk/Line" }
		, { "TileHole", "StAndrews/Gambit", "Gambit" }
		, { "TileHole", "StAndrews/Mini-Jump", "Mini Jump" }
		, { "TileHole", "StAndrews/Slip", "Slip" }
		, { "TileHole", "StAndrews/Diminish", "Diminish" }
		, { "TileHole", "StAndrews/Pond", "Pond" }
		, { "TileHole", "StAndrews/Curls", "Curls" }
		, { "TileHole", "StAndrews/Left-Leap", "Left Leap" }
		, { "TileHole", "StAndrews/Knot", "Knot" }
		, { "TileHole", "StAndrews/Side-Ride", "Sider" }
		, { "TileHole", "StAndrews/Bender", "Bender" }
		, { "TileHole", "StAndrews/Leap-Faith", "Leap of Faith" }
		};
		
	private float timeStarted = 0.0f;
	public float boundsPenaltyTime = 0.5f;

	void Awake()
	{
		instance = this;
		// the main game controller (this object) needs to persist in order to orchestrate the
		// whole game's scene flow.
		DontDestroyOnLoad(transform.gameObject);
		Debug.Log("awake");
	}
	
	// Use this for initialization
	void Start()
	{
		Application.LoadLevel("MainMenu");
	}
	
	public void NewGame()
	{
		Debug.Log("game controller start game");
		var round = new PlayerRound("St. Andrews", currentCourse);
		PlayerRound.CurrentRound = round;
		UnloadCurrentLevel();
		Application.LoadLevel(currentCourse[0, 0]);
	}
	
	public void OnHoleComplete()
	{
		PlayerRound.CurrentRound.advanceToNextHole();
		Application.LoadLevel(PlayerRound.CurrentRound.getCurrentHoleSceneName());
	}
	
	private void UnloadCurrentLevel()
	{
		var sceneRootObjects = GameObject.FindGameObjectsWithTag("SceneRoot");
		Debug.Log(string.Format("found {0} objects to destroy", sceneRootObjects.Length));
		foreach(var obj in sceneRootObjects) {
			Debug.Log(string.Format("destroy {0}", obj));
			Destroy(obj);
		}
	}
	
}
}