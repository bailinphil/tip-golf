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
	public AudioClip lowRoll;
	public AudioClip medRoll;
	public AudioClip highRoll;
	protected GameObject lowSource;
	protected GameObject medSource;
	protected GameObject highSource;

	public void Start()
	{
		Messenger.AddListener("OutOfBounds", OnOutOfBounds);
		Messenger.AddListener("Goal", OnGoal);
		
		lowSource = new GameObject();
		lowSource.AddComponent("AudioSource");
		lowSource.audio.clip = lowRoll;
		lowSource.audio.loop = true;
		lowSource.audio.volume = 0.0f;
		lowSource.audio.Play();
		medSource = new GameObject();
		medSource.AddComponent("AudioSource");
		medSource.audio.clip = medRoll;
		medSource.audio.loop = true;
		medSource.audio.volume = 0.0f;
		medSource.audio.Play();
		highSource = new GameObject();
		highSource.AddComponent("AudioSource");
		highSource.audio.clip = highRoll;
		lowSource.audio.volume = 0.0f;
		highSource.audio.loop = true;
		highSource.audio.volume = 0.0f;
		highSource.audio.Play();
		
		timeStarted = Time.time;
	}
	
	public void Update()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var playerSpeed = player.rigidbody.velocity.magnitude;
		if(playerSpeed < 0.1f) {
			lowSource.audio.volume = 0.0f;
			medSource.audio.volume = 0.0f;
			highSource.audio.volume = 0.0f;
		} else if(playerSpeed < 1.0f) {
			lowSource.audio.volume = playerSpeed;
			medSource.audio.volume = 0.0f;
			highSource.audio.volume = 0.0f;
		} else if(playerSpeed < 3.0f) {
			lowSource.audio.volume = 1.0f;
			medSource.audio.volume = 0.0f;
			highSource.audio.volume = 0.0f;
		} else if(playerSpeed < 5.0f) {
			lowSource.audio.volume = 0.0f;
			medSource.audio.volume = 1.0f;
			highSource.audio.volume = 0.0f;
		} else {
			lowSource.audio.volume = 0.0f;
			medSource.audio.volume = 0.0f;
			highSource.audio.volume = 1.0f;
				
		}
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