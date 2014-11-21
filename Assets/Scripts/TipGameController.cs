using UnityEngine;
using System.Collections;
using Lib;

namespace TipGolf
{

/*
 * This class is the main manager of the Tip-Golf game. It is responsible for:
 *  1. Loading all scenes, whether menu screens or holes to play
 *  2. Maintaining a record of how well the player is doing
 *  3. Playing music or other tasks that should continue continuously
 *  4. Handling configuration
 */
public class TipGameController : MonoBehaviour
{

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
	private float timeHoleStarted = 0.0f;
	public float boundsPenaltyTime = 0.5f;
	
	// This variable will be initialized when we start a new game
	private PlayerRound currentRound = null;
	
	// It's a little tricky to pass references to the controllers of scenes, menus, and 
	// the like. Therefore, I'm using this master controller as a singleton.
	private static TipGameController instance = null;

	public static TipGameController GetInstance()
	{
		if(instance == null) {
			Debug.Log("invoking constructor because null");
			instance = new TipGameController();
		}
		return instance;
	}

	void Awake()
	{
		instance = this;
		
		// the main game controller (this object) needs to persist in order to orchestrate the
		// whole game's scene flow. When LoadLevel is called, all objects are fair game for 
		// garbage collection. But this GameObject should remain, to manage the transitions
		// between screens and holes.
		DontDestroyOnLoad(transform.gameObject);
		
		Application.targetFrameRate = 60;
	}
	
	void Start()
	{
		Application.LoadLevel("MainMenu");
	}
	
	/*
	 * "Handler" methods: originally I wanted to use the Messenger class to pass events
	 * back to this TipGameController, but on calls to LoadLevel, the message listener 
	 * entries in Messenger's eventTable class were being destroyed. Using 
	 * LoadLevelAdditive prevented this, but I couldn't figure out how to properly 
	 * destroy the old scenes.
	 *
	 * The (inelegant) solutions I'm using is to make a pseudo-singleton of this 
	 * class and ditch message passing in favor of direct invocation. The methods
	 * below are how the various holes and menus screen(s) can indicate that something
	 * interesting has happened and it's time to change state.
	 */
	 
	// invoked by MainMenu.
	public void OnNewGame()
	{
		currentRound = new PlayerRound("St. Andrews", currentCourse);
		Application.LoadLevel(currentCourse[0, 0]);
		timeHoleStarted = Time.time;
	}
	
	// invoked by any Hole
	public void OnOutOfBounds()
	{
		currentRound.LogTimeTaken(boundsPenaltyTime);
		Application.LoadLevel(Application.loadedLevel);
	}
	
	// invoked by any Hole
	public void OnHoleComplete()
	{
		var now = Time.time;
		currentRound.LogTimeTaken(now - timeHoleStarted);
		currentRound.AdvanceToNextHole();
		Application.LoadLevel(currentRound.GetCurrentHoleSceneName());
		timeHoleStarted = now;
	}

	// TileHole instances need to be configured after their construction. This
	// accessor is used to fetch that configuration.
	public string GetCurrentHoleConfigResource()
	{
		if(currentRound == null) {
			throw new UnityException("can't get config resource because a game has not started yet.");
		} else {
			return currentRound.GetCurrentHoleConfigResource();
		}
	}
	
	public float GetCoursePlayerTime()
	{
		if(currentRound == null) {
			return 0.0f;
		} else {
			return currentRound.GetCoursePlayerTime();
		}
	}
}
}