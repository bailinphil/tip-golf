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
		lowSource.AddComponent<AudioSource>();
		lowSource.GetComponent<AudioSource>().clip = lowRoll;
		lowSource.GetComponent<AudioSource>().loop = true;
		lowSource.GetComponent<AudioSource>().volume = 0.0f;
		lowSource.GetComponent<AudioSource>().Play();
		medSource = new GameObject();
		medSource.AddComponent<AudioSource>();
		medSource.GetComponent<AudioSource>().clip = medRoll;
		medSource.GetComponent<AudioSource>().loop = true;
		medSource.GetComponent<AudioSource>().volume = 0.0f;
		medSource.GetComponent<AudioSource>().Play();
		highSource = new GameObject();
		highSource.AddComponent<AudioSource>();
		highSource.GetComponent<AudioSource>().clip = highRoll;
		lowSource.GetComponent<AudioSource>().volume = 0.0f;
		highSource.GetComponent<AudioSource>().loop = true;
		highSource.GetComponent<AudioSource>().volume = 0.0f;
		highSource.GetComponent<AudioSource>().Play();
		
		
	}
	
	// Update is called once per frame
	void Update()
	{
		var playerSpeed = GetComponent<Rigidbody>().velocity.magnitude;
		if(!isCurrentlyOnFloor() || playerSpeed < 0.1f) {
			lowSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			medSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			highSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
		} else if(playerSpeed < 1.0f) {
			lowSource.GetComponent<AudioSource>().volume = playerSpeed * rollVolumeFactor;
			medSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			highSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
		} else if(playerSpeed < 3.0f) {
			lowSource.GetComponent<AudioSource>().volume = 1.0f * rollVolumeFactor;
			medSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			highSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
		} else if(playerSpeed < 5.0f) {
			lowSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			medSource.GetComponent<AudioSource>().volume = 1.0f * rollVolumeFactor;
			highSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
		} else {
			lowSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			medSource.GetComponent<AudioSource>().volume = 0.0f * rollVolumeFactor;
			highSource.GetComponent<AudioSource>().volume = 1.0f * rollVolumeFactor;
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
				GetComponent<AudioSource>().clip = bonk1;
			} else {
				GetComponent<AudioSource>().clip = bonk2;
			}
			GetComponent<AudioSource>().volume = bonkVolumeFactor;
			GetComponent<AudioSource>().Play();
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
