using UnityEngine;
using System.Collections;
using Lib;

public class Goal : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

	void OnTriggerEnter ( Collider other){
		if( other.tag == "Player" ){
			Messenger.Broadcast( "Goal" );
		}
	}
}
