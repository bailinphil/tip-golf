using UnityEngine;
using System.Collections;
using Lib;

public class BoundsTrigger : MonoBehaviour {

	void OnTriggerExit( Collider other ){
		Messenger.Broadcast( "OutOfBounds" );
		Application.LoadLevel( Application.loadedLevel );
	}
}
