using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void OnGUI(){
		if (GUI.Button(new Rect(60, 140, 300, 60), "New Game"))
			Application.LoadLevel ( "Andrews1" );
	}
}
