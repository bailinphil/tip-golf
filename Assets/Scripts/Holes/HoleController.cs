using UnityEngine;
using System.Collections;
using Lib;

namespace TipGolf
{
public class HoleController : MonoBehaviour
{

	public float parTime;
	public string holeName;
	private float timeStarted;
	public float boundsPenaltyTime;

	public void Start()
	{
		Debug.Log("starting hole " + PlayerRound.CurrentRound.getCurrentHoleTitle());
		PlayerRound.CurrentRound.StartHole(holeName, parTime);
		Messenger.AddListener("OutOfBounds", OnOutOfBounds);
		Messenger.AddListener("Goal", OnGoal);
		timeStarted = Time.time;
	}
		
	public void OnOutOfBounds()
	{
		PlayerRound.CurrentRound.logTimeTaken(PlayerRound.CurrentRound.getCurrentHoleTitle(), boundsPenaltyTime);
		Application.LoadLevel(Application.loadedLevel);
	}
		
	public void OnGoal()
	{
		PlayerRound.CurrentRound.logTimeTaken(holeName, Time.time - timeStarted);
		TipGameController.instance.OnHoleComplete();
	}
}
}