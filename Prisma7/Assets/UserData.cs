using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour {

	public List<int> doorsPlayed;
	public Vector3 lastPosition;

	public void OnDoorPlayed(int doorId)
	{
		doorsPlayed.Add (doorId);
	}
	public void SaveLastPosition()
	{
		Vector3 pos = Game.Instance.character.transform.localPosition;
		pos.z -= 1.5f;
		lastPosition = pos;
	}
}

