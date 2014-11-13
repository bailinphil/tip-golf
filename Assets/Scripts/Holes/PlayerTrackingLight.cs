using UnityEngine;
using System.Collections;

namespace TipGolf
{
public class PlayerTrackingLight : MonoBehaviour
{

	public GameObject player;
	private Vector3 offset;

	void Start()
	{
		offset = transform.position;
	}

	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
	}
}
}