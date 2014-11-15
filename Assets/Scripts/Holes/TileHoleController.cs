using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.IO;

namespace TipGolf
{
public class TileHoleController : HoleController
{

	public GameObject goal;
	public GameObject flat2x2;
	public GameObject flat4x4;
	public GameObject sidePlusX;
	public GameObject sideMinusX;
	public GameObject sidePlusZ;
	public GameObject sideMinusZ;
	public GameObject cornerPlusXPlusZ;
	public GameObject cornerPlusXMinusZ;
	public GameObject cornerMinusXPlusZ;
	public GameObject cornerMinusXMinusZ;
	public Texture lowTileFloorTexture;
	private GameObject courseRoot;
	

	void Start()
	{
		base.Start();
		var resourceName = TipGameController.instance.GetCurrentHoleConfigResource();
		var configStr = Resources.Load(resourceName).ToString();

		courseRoot = GameObject.FindWithTag("CourseBase");
		using(XmlReader reader = XmlReader.Create(new StringReader(configStr))) {
			if(reader.ReadToFollowing("hole")) {
				holeName = reader.GetAttribute("name");
				loadTiles(reader);
			} else {
				throw new ArgumentException("Can't find any parts in " + resourceName);
			}
		}
	}
	
	private void loadTiles(XmlReader reader)
	{
		var tileCounter = 0;
		while(reader.ReadToFollowing("part")) {
			tileCounter += 1;
			var tileType = reader.GetAttribute("type");
			makeHolePart(tileType, reader);
		}
	}

	private void makeHolePart(string part, XmlReader reader)
	{
		float x = 0.0f, y = 0.0f, z = 0.0f;
		readXYZAttributes(reader, out x, out y, out z);
		var newTile = (GameObject)Instantiate(getTileForType(part), new Vector3(x, y, z), Quaternion.identity);
		if(y < 0) {
			// tiles that are "low" in the configuration should have a distinct texture 
			// find the "floor" object and give it a yellow texture
			foreach(Transform child in newTile.transform) {
				if(child.tag == "Floor") {
					child.gameObject.renderer.material.mainTexture = lowTileFloorTexture;
				}
			}
		}
		newTile.transform.parent = courseRoot.transform;
	}

	private void readXYZAttributes(XmlReader reader, out float x, out float y, out float z)
	{
		var xStr = reader.GetAttribute("x");
		var yStr = reader.GetAttribute("y");
		var zStr = reader.GetAttribute("z");
		x = 0.0f;
		y = 0.0f;
		z = 0.0f;
		// we always need an X and a Z coordinate, but can assume 0 for y most of the time.
		if(!float.TryParse(xStr, out x) || !float.TryParse(zStr, out z)) {
			throw new ArgumentException("can't parse X or Z in element");
		}
		if(!float.TryParse(yStr, out y)) {
			y = 0.0f;
		}
	}
									
	private GameObject getTileForType(string tileType)
	{
		switch(tileType) {
		case "goal":
			return goal;
		case "flat2x2":
			return flat2x2;
		case "flat4x4":
			return flat4x4;
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