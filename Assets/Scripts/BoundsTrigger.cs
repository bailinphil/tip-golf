using UnityEngine;
using System.Collections;
using Lib;

namespace TipGolf
{
public class BoundsTrigger : MonoBehaviour
{

	void OnTriggerExit(Collider other)
	{
		Messenger.Broadcast("OutOfBounds");
		Application.LoadLevel(Application.loadedLevel);
	}
}
}
