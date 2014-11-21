using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public AudioClip lowRoll;
	public AudioClip medRoll;
	public AudioClip highRoll;
	protected GameObject lowSource;
	protected GameObject medSource;
	protected GameObject highSource;

	protected bool touchingFloor;	

	// Use this for initialization
	void Start()
	{
		touchingFloor = true;
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
		
	}
	
	// Update is called once per frame
	void Update()
	{
		var playerSpeed = rigidbody.velocity.magnitude;
		if(!touchingFloor || playerSpeed < 0.1f) {
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
	
	public void OnCollisionEnter(Collision other)
	{
		Debug.Log(string.Format("enter with {0}", other.gameObject.tag));
		if(other.gameObject.tag == "Floor") {
			touchingFloor = true;
		}
	}
	
	public void OnCollisionExit(Collision other)
	{
		Debug.Log(string.Format("exit with {0}", other.gameObject.tag));
		if(other.gameObject.tag == "Floor") {
			touchingFloor = false;
		}
	}
}
