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
		Messenger.AddListener("OutOfBounds", OnOutOfBounds);
		Messenger.AddListener("Goal", OnGoal);
		
		
		timeStarted = Time.time;
	}
	
	public void OnOutOfBounds()
	{
		TipGameController.GetInstance().OnOutOfBounds();
	}
		
	public void OnGoal()
	{
		TipGameController.GetInstance().OnHoleComplete();
	}
}
}