using UnityEngine;
using System.Collections;
using System;
using Lib;

namespace TipGolf
{
public class MainMenuController : MonoBehaviour
{
	public Texture logoTexture;
	public Texture backgroundTexture;
	public GUISkin skin;
	public ScreenRegion logoRegion;
	public ScreenRegion playButtonRegion;
	public ScreenRegion timeTakenRegion;
	private float timeTaken;
	public Font timeFont;
	public Material whiteFontMaterial;

	void Start()
	{
		
		timeTaken = TipGameController.GetInstance().GetCoursePlayerTime();
		
		if(!backgroundTexture) {
			Debug.LogError("Assign a background texture in the editor");
		}
		if(logoRegion == null) {
			Debug.LogError("Assign logo region in the editor");
		}
		if(playButtonRegion == null) {
			Debug.LogError("Assign play button region in the editor");
		}
		if(timeTakenRegion == null) {
			Debug.LogError("Assign time taken button region in the editor");
		}
		Debug.Log(string.Format("logo texture should be {0}x{1}", logoRegion.getRect().width, logoRegion.getRect().height));
	}

	void OnGUI()
	{
		GUI.skin = skin;
	
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture, ScaleMode.StretchToFill, false);
		
		GUI.DrawTexture(logoRegion.getRect(), logoTexture, ScaleMode.ScaleToFit, true);
		
		if(GUI.Button(playButtonRegion.getRect(), "New Game")) {
			TipGameController.GetInstance().OnNewGame();
		}
		
		if(timeTaken > 0.0f) {
			GUI.Label(timeTakenRegion.getRect(), String.Format("That took {0:0.00}s.\nGood job!", timeTaken));
		}
	}
}
}