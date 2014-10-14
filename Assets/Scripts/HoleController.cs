using UnityEngine;
using System.Collections;
using Lib;

public class HoleController : MonoBehaviour {

	public string nextLevel;
	public float parTime;
	public string holeName;

	private float timeStarted;

	// Use this for initialization
	public void Start () 
	{
		PlayerRound.CurrentRound.playHole( holeName, parTime );
		Messenger.AddListener( "OutOfBounds", OnOutOfBounds );
		Messenger.AddListener( "Goal", OnGoal );
		timeStarted = Time.time;
	}
	
	public void OnOutOfBounds()
	{
		PlayerRound.CurrentRound.finishHole( holeName, Time.time - timeStarted );
		Application.LoadLevel( Application.loadedLevel );
	}

	public void OnGoal()
	{
		PlayerRound.CurrentRound.finishHole( holeName, Time.time - timeStarted );
		Application.LoadLevel( nextLevel );
	}
}
