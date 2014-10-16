using UnityEngine;
using System.Collections;
using Lib;

namespace TipGolf
{
public class HoleController : MonoBehaviour
{

	public string nextLevel;
	public float parTime;
	public string holeName;
	private float timeStarted;

	public void Start()
	{
		Debug.Log("starting hole " + PlayerRound.CurrentRound.getCurrentHoleTitle());
		PlayerRound.CurrentRound.startHole(holeName, parTime);
		Messenger.AddListener("OutOfBounds", OnOutOfBounds);
		Messenger.AddListener("Goal", OnGoal);
		timeStarted = Time.time;
	}
	
	public void OnOutOfBounds()
	{
		PlayerRound.CurrentRound.logTimeTaken(holeName, Time.time - timeStarted);
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnGoal()
	{
		PlayerRound.CurrentRound.logTimeTaken(holeName, Time.time - timeStarted);
		PlayerRound.CurrentRound.advanceToNextHole();
		Application.LoadLevel(PlayerRound.CurrentRound.getCurrentHoleSceneName());
	}
}
}