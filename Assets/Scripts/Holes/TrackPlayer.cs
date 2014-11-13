using UnityEngine;
using System.Collections;

namespace TipGolf
{
public class TrackPlayer : MonoBehaviour
{

	public GameObject player;
	private Vector3 offset;

	void Start()
	{
		offset = transform.localPosition - player.transform.localPosition;
	}
	
	void LateUpdate()
	{
		transform.localPosition = player.transform.localPosition + offset;
	}
}
}