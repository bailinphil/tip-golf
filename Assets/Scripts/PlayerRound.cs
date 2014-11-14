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
	public static PlayerRound CurrentRound = new PlayerRound("one hole test", new string[,]{ {
		Application.loadedLevelName,
		"StAndrews/Leap-Faith",
		"one scene test hole"
	} });
	
	private int currentHoleIndex { get; set; }
	private string[,] courseHoles;
	private IList<PlayerPerformance> performances;

	public PlayerRound(string course, string[,] holes)
	{
		courseName = course;
		courseHoles = holes;
		performances = new List<PlayerPerformance>();
		for(var i = 0; i <= holes.GetUpperBound(0); ++i) {
			performances.Add(new PlayerPerformance());
		}
			
		currentHoleIndex = 0;
		holeNamesToListOfPlayerTimes = new Dictionary<string, IList>();
		holeNamesToParTimes = new Dictionary<string, float>();
	}

	public void StartHole(string name, float parTime)
	{
		/*
		if(name == null || name.Trim().Length == 0) {
			throw new ArgumentException(String.Format("Hole for scene {0} needs a name", Application.loadedLevelName));
		}
		*/
	}

	public void logTimeTaken(string name, float playerTime)
	{
		performances[currentHoleIndex].logTime(playerTime);
	}

	public float getCoursePlayerTime()
	{
		var timeAccumulator = 0.0f;
		foreach(PlayerPerformance perf in performances) {
			timeAccumulator += perf.getTotalTime();
		}
		return timeAccumulator;	
	}
	
	public void advanceToNextHole()
	{
		currentHoleIndex += 1;
	}
	
	public string getCurrentHoleSceneName()
	{
		Debug.Log("current index " + currentHoleIndex + " out of " + courseHoles.GetUpperBound(0));
		if(currentHoleIndex <= courseHoles.GetUpperBound(0)) {
			return courseHoles[currentHoleIndex, 0];
		}
		return "MainMenu";
	}
	
	public string getCurrentHoleConfigResource()
	{
		if(currentHoleIndex <= courseHoles.GetUpperBound(0)) {
			return courseHoles[currentHoleIndex, 1];
		}
		throw new Exception("Don't know current hole configuration");
	}
	
	public string getCurrentHoleTitle()
	{
		if(currentHoleIndex <= courseHoles.GetUpperBound(0)) {
			return courseHoles[currentHoleIndex, 2];
		}
		throw new Exception("Don't know current hole configuration");
	}
}
}