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
	}

	public void LogTimeTaken(float playerTime)
	{
		performances[currentHoleIndex].logTime(playerTime);
	}

	public float GetCoursePlayerTime()
	{
		var timeAccumulator = 0.0f;
		foreach(PlayerPerformance perf in performances) {
			timeAccumulator += perf.getTotalTime();
		}
		return timeAccumulator;	
	}
	
	public void AdvanceToNextHole()
	{
		currentHoleIndex += 1;
	}
	
	public string GetCurrentHoleSceneName()
	{
		if(currentHoleIndex <= courseHoles.GetUpperBound(0)) {
			return courseHoles[currentHoleIndex, 0];
		}
		return "MainMenu";
	}
	
	public string GetCurrentHoleConfigResource()
	{
		if(currentHoleIndex <= courseHoles.GetUpperBound(0)) {
			return courseHoles[currentHoleIndex, 1];
		}
		throw new Exception("Don't know current hole configuration");
	}
	
	public string GetCurrentHoleTitle()
	{
		if(currentHoleIndex <= courseHoles.GetUpperBound(0)) {
			return courseHoles[currentHoleIndex, 2];
		}
		throw new Exception("Don't know current hole configuration");
	}
}
}