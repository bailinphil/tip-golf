using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float rollVolumeFactor = 0.0f;
	public float bonkVolumeFactor = 1.0f;
	public AudioClip lowRoll;
	public AudioClip medRoll;
	public AudioClip highRoll;
	public AudioClip bonk1;
	public AudioClip bonk2;
	protected GameObject lowSource;
	protected GameObject medSource;
	protected GameObject highSource;
	protected float timeOfLastFloorCollision = 0.0f;
	public float silenceDelay = 0.1f;

	// Use this for initialization
	void Start()
	{
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
		if(!isCurrentlyOnFloor() || playerSpeed < 0.1f) {
			lowSource.audio.volume = 0.0f * rollVolumeFactor;
			medSource.audio.volume = 0.0f * rollVolumeFactor;
			highSource.audio.volume = 0.0f * rollVolumeFactor;
		} else if(playerSpeed < 1.0f) {
			lowSource.audio.volume = playerSpeed * rollVolumeFactor;
			medSource.audio.volume = 0.0f * rollVolumeFactor;
			highSource.audio.volume = 0.0f * rollVolumeFactor;
		} else if(playerSpeed < 3.0f) {
			lowSource.audio.volume = 1.0f * rollVolumeFactor;
			medSource.audio.volume = 0.0f * rollVolumeFactor;
			highSource.audio.volume = 0.0f * rollVolumeFactor;
		} else if(playerSpeed < 5.0f) {
			lowSource.audio.volume = 0.0f * rollVolumeFactor;
			medSource.audio.volume = 1.0f * rollVolumeFactor;
			highSource.audio.volume = 0.0f * rollVolumeFactor;
		} else {
			lowSource.audio.volume = 0.0f * rollVolumeFactor;
			medSource.audio.volume = 0.0f * rollVolumeFactor;
			highSource.audio.volume = 1.0f * rollVolumeFactor;
		}
	}
	
	public bool isCurrentlyOnFloor()
	{
		var z = transform.localPosition.z;
		return ((z < -0.95 && z > -1.05) || (z > 2.95 && z < 3.05));
	}
	
	public void OnCollisionEnter(Collision other)
	{
		Debug.Log(string.Format("enter with {0}", other.gameObject.tag));
		if(other.gameObject.tag == "Floor") {
			timeOfLastFloorCollision = Time.time;
		}
		if(other.gameObject.tag == "Wall") {
			if(Random.value > 0.5) {
				audio.clip = bonk1;
			} else {
				audio.clip = bonk2;
			}
			audio.volume = bonkVolumeFactor;
			audio.Play();
		}
	}
	
	public void OnCollisionExit(Collision other)
	{
		Debug.Log(string.Format("exit with {0}", other.gameObject.tag));
		if(other.gameObject.tag == "Floor") {
			timeOfLastFloorCollision = Time.time;
		}
	}
}
