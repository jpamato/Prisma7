using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour {

	public List<Door> doors;

	void Start () {
		Events.OnChangeWorld += OnChangeWorld;
		int diamondLevel = Data.Instance.levelsData.actualDiamondLevel;
		foreach (Door door in doors) {
			if(diamondLevel>=door.diamondLevel)
				door.SetState (Door.states.CLOSED);
			else
				door.SetState (Door.states.UNAVAILABLE);
		}
		foreach (int doorID in Data.Instance.userData.doorsPlayed) {
			GetDoorByID (doorID).SetState (Door.states.OPENED);				
		}
	}
	void OnDestroy () {
		Events.OnChangeWorld -= OnChangeWorld;
	}
	void OnChangeWorld(int a)
	{
		Data.Instance.userData.ResetDoors ();
	}
	Door GetDoorByID(int id)
	{
		foreach (Door door in doors)
			if (door.id == id)
				return door;
		return null;
	}

}
