// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace TipGolf
{
public class PlayerPerformance
{
	public IList<float> timesTaken;
	public PlayerPerformance()
	{
	
		timesTaken = new List<float>();
	}
	
	public void logTime(float seconds)
	{
		timesTaken.Add(seconds);
	}
	
	public float getTotalTime()
	{
		float result = 0.0f;
		foreach(float time in timesTaken) {
			result += time;
		}
		return result;
	}
	
	public int getNumTries()
	{
		return timesTaken.Count;
	}
}
}

