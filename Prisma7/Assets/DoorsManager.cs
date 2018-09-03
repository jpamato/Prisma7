using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour {

	public List<Door> doors;

	void Start () {
		foreach (Door door in doors)
			door.SetState (Door.states.CLOSED);
		foreach (int doorID in Data.Instance.userData.doorsPlayed) {
			GetDoorByID (doorID).SetState (Door.states.OPENED);				
		}
	}
	Door GetDoorByID(int id)
	{
		foreach (Door door in doors)
			if (door.id == id)
				return door;
		return null;
	}

}
