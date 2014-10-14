using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.IO;

namespace TipGolf
{
public class TileHoleController : HoleController
{

	public string holeURL;
	public GameObject flat2x2;
	public GameObject sidePlusX;
	public GameObject sideMinusX;
	public GameObject sidePlusZ;
	public GameObject sideMinusZ;
	public GameObject cornerPlusXPlusZ;
	public GameObject cornerPlusXMinusZ;
	public GameObject cornerMinusXPlusZ;
	public GameObject cornerMinusXMinusZ;

	IEnumerator Start()
	{
		base.Start();
		var www = new WWW(holeURL);
		yield return www;
		
		var courseRoot = GameObject.FindWithTag("CourseBase");
		using(XmlReader reader = XmlReader.Create(new StringReader(www.text))) {
			var tileCounter = 0;
			while(reader.ReadToFollowing("tile")) {
				tileCounter += 1;
				var tileType = reader.GetAttribute("type");
				var tileX = reader.GetAttribute("x");
				var tileY = reader.GetAttribute("y");
				var tileZ = reader.GetAttribute("z");
				float x = 0.0f;
				float y = 0.0f;
				float z = 0.0f;
				if(tileType == null || !float.TryParse(tileX, out x) || !float.TryParse(tileZ, out z)) {
					throw new ArgumentException("can't parse the tile element, #" + tileCounter);
				} else {
					var tile = getTileForType(tileType);
					var tilePlacement = new CourseTile(tile, x, z);
					if(float.TryParse(tileY, out y)) {
						tilePlacement = new CourseTile(tile, x, y, z);
					}
					
					var newTile = (GameObject)Instantiate(tilePlacement.tile, tilePlacement.location, Quaternion.identity);
					newTile.transform.parent = courseRoot.transform;
				}
			}
		}
	}
	
	public void Update()
	{
		//print(Time.time);
	}
		
	
	private GameObject getTileForType(string tileType)
	{
		switch(tileType) {
		case "flat2x2":
			return flat2x2;
		case "sidePlusX":
			return sidePlusX;
		case "sideMinusX":
			return sideMinusX;
		case "sidePlusZ":
			return sidePlusZ;
		case "sideMinusZ":
			return sideMinusZ;
		case "cornerPlusXPlusZ":
			return cornerPlusXPlusZ;
		case "cornerPlusXMinusZ":
			return cornerPlusXMinusZ;
		case "cornerMinusXPlusZ":
			return cornerMinusXPlusZ;
		case "cornerMinusXMinusZ":
			return cornerMinusXMinusZ;
		default:
			throw new ArgumentException(String.Format("Tile type {0} not recognized", tileType));
		}
	}

}
}