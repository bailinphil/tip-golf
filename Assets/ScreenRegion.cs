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
using UnityEngine;
namespace TipGolf
{

[System.Serializable]
public class ScreenRegion
{
	public float xFraction;
	public float yFraction;
	public float widthFraction;
	public float heightFraction;
		
	public ScreenRegion(float _xFraction,
	                   float _yFraction,
	                   float _widthFraction,
	                   float _heightFraction)
	{
		xFraction = _xFraction;
		yFraction = _yFraction;
		widthFraction = _widthFraction;
		heightFraction = _heightFraction;
	}

	/**
	 * I want to scale things to the screen by fraction, i.e. allocate 30% of the height of
	 * the screen to a logo, then 10% to a button, etc.
	 * 
	 * This function will generate Rects which represent these regions, based off of Screen.width, etc.
	 */
	public Rect getRect()
	{
		// should this instead be initialized in the constructor?
		return new Rect(Screen.width * xFraction,
			              Screen.height * yFraction,
			              Screen.width * widthFraction,
			              Screen.height * heightFraction);
	}
}
}

