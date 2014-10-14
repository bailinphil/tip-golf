using UnityEngine;
using System.Collections;

public class BrickHoleController : HoleController {

	public GameObject flat2x2;
	public GameObject sidePlusX;
	public GameObject sideMinusX;
	public GameObject sidePlusZ;
	public GameObject sideMinusZ;
	public GameObject cornerPlusXPlusZ;
	public GameObject cornerPlusXMinusZ;
	public GameObject cornerMinusXPlusZ;
	public GameObject cornerMinusXMinusZ;


	private CourseBrick[] course; 

	void Start(){
		base.Start();

		course = new CourseBrick[]  { new CourseBrick( flat2x2, 0, 0 )
			, new CourseBrick( sidePlusX, 2, 0)
			, new CourseBrick( sidePlusZ, 0, 2)
			, new CourseBrick( sideMinusX, -2, 0)
			, new CourseBrick( sideMinusZ, 0, -2)
			, new CourseBrick( cornerPlusXPlusZ, 2, 2)
			, new CourseBrick( cornerPlusXMinusZ, 2, -2)
			, new CourseBrick( cornerMinusXPlusZ,-2, 2)
			, new CourseBrick( sideMinusX, -2, -2)
			, new CourseBrick( sideMinusX, -2, -4)
			, new CourseBrick( flat2x2, 0, -4 )
			, new CourseBrick( sidePlusX, 2, -4 )
			, new CourseBrick( sideMinusX, -2, -6)
			, new CourseBrick( flat2x2, 0, -6 )
			, new CourseBrick( sidePlusX, 2, -6 )
			, new CourseBrick( cornerMinusXMinusZ, -2, -8 )
			, new CourseBrick( sideMinusZ, 0, -8 )
			, new CourseBrick( sidePlusX, 2, -8 )
		};
		foreach( var placement in course ){
			var t = (GameObject) Instantiate( placement.brick, placement.location, Quaternion.identity );
			var courseRoot = GameObject.FindWithTag( "CourseBase" );
			t.transform.parent = courseRoot.transform;
		}

	}
}
