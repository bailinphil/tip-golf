using UnityEngine;
using System.Collections;
using Lib;

namespace TipGolf
{
public class Goal : MonoBehaviour
{

	public float spinRate = 2.5f;

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * spinRate);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player") {
			Messenger.Broadcast("Goal");
		}
	}
}
}