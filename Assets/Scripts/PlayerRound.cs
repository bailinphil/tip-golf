using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace TipGolf
{
public class PlayerRound
{
	private string courseName { get; set; }

	private IList holeNamesInOrderPlayed { get; set; }

	private IDictionary<string, IList> holeNamesToListOfPlayerTimes { get; set; }

	private IDictionary<string, float> holeNamesToParTimes { get; set; }

	// this is an unusual use of singleton; I'm letting the main menu
	// instantiate the PlayerRound every time a new one starts--but 
	// each of the holes will be able to access it through this static
	// variable without trying to find a way to pass the global object
	// from scene to scene. A proper factory object is probably the right
	// way to handle this, but I'm just going to go with this because in
	// all likelihood it's not going to cause any problem.
	public static PlayerRound CurrentRound = new PlayerRound("one hole test");

	public PlayerRound(string course)
	{
		courseName = course;
		holeNamesInOrderPlayed = new ArrayList();
		holeNamesToListOfPlayerTimes = new Dictionary<string, IList>();
		holeNamesToParTimes = new Dictionary<string, float>();
	}

	public void playHole(string name, float parTime)
	{
		if(name == null || name.Trim().Length == 0) {
			throw new ArgumentException(String.Format("Hole for scene {0} needs a name", Application.loadedLevelName));
		}
		holeNamesInOrderPlayed.Add(name);
		holeNamesToParTimes[name] = parTime;
		holeNamesToListOfPlayerTimes[name] = new ArrayList();
	}

	public void finishHole(string name, float playerTime)
	{
		holeNamesToListOfPlayerTimes[name].Add(playerTime);
	}

	public float getCourseParTime()
	{
		var timeAccumulator = 0.0f;
		foreach(string name in holeNamesInOrderPlayed) {
			timeAccumulator += holeNamesToParTimes[name];
		}
		return timeAccumulator;
	}

	public float getCoursePlayerTime()
	{
		var timeAccumulator = 0.0f;
		foreach(string name in holeNamesInOrderPlayed) {
			foreach(float time in holeNamesToListOfPlayerTimes[name]) {
				timeAccumulator += time;
			}
		}
		return timeAccumulator;	
	}
}
}