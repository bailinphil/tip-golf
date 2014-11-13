using System;
using UnityEngine;

namespace TipGolf
{
public class CourseTile
{
	public GameObject tile;
	public Vector3 location;

	public CourseTile(GameObject obj, float x, float z)
	{
		tile = obj;
		location = new Vector3(x, 0.0f, z);
	}

	public CourseTile(GameObject obj, float x, float y, float z)
	{
		tile = obj;
		location = new Vector3(x, y, z);
	}

}
}
