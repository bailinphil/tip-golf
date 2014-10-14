using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;


public class TileHoleController : HoleController
{

		public GameObject flat2x2;
		public GameObject sidePlusX;
		public GameObject sideMinusX;
		public GameObject sidePlusZ;
		public GameObject sideMinusZ;
		public GameObject cornerPlusXPlusZ;
		public GameObject cornerPlusXMinusZ;
		public GameObject cornerMinusXPlusZ;
		public GameObject cornerMinusXMinusZ;

		void Start ()
		{
			base.Start ();
/*			var www = new WWW ("Andrews3.xml");
			yield return www;
			using (XmlReader reader = XmlReader.Create(new StringReader(www.text))) {
					reader.ReadToFollowing ("tile");
				
			}
*/
			var course = new CourseTile[]  { new CourseTile (flat2x2, 0, 0)
			       , new CourseTile (sidePlusX, 2, 0)
			       , new CourseTile (sidePlusZ, 0, 2)
			       , new CourseTile (sideMinusX, -2, 0)
			       , new CourseTile (sideMinusZ, 0, -2)
			       , new CourseTile (cornerPlusXPlusZ, 2, 2)
			       , new CourseTile (cornerPlusXMinusZ, 2, -2)
			, new CourseTile (cornerMinusXPlusZ, -2, 2)
			, new CourseTile (sideMinusX, -2, -2)
			, new CourseTile (sideMinusX, -2, -4)
			, new CourseTile (flat2x2, 0, -4)
			, new CourseTile (sidePlusX, 2, -4)
			, new CourseTile (sideMinusX, -2, -6)
			, new CourseTile (flat2x2, 0, -6)
			, new CourseTile (sidePlusX, 2, -6)
			, new CourseTile (cornerMinusXMinusZ, -2, -8)
			, new CourseTile (sideMinusZ, 0, -8)
			, new CourseTile (sidePlusX, 2, -8)
		};
				foreach (var placement in course) {
						var t = (GameObject)Instantiate (placement.tile, placement.location, Quaternion.identity);
						var courseRoot = GameObject.FindWithTag ("CourseBase");
						t.transform.parent = courseRoot.transform;
				}

		}
}
