using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLevel : InteractiveObject {

	public Animator anim;

	public int id;
	public int gotoLevel;
	public Vector3 destinationCoords;

	public states state;
	public enum states
	{
		UNAVAILABLE,
		OPENED
	}


	void Start () {
		if (Data.Instance.levelsData.actualDiamondLevel >= 2 && Data.Instance.userData.GetPortalIDOpened() >= id) {
			state = states.OPENED;
			anim.Play ("portal_unlocked");
		}
	}
	public bool ChekToCross()
	{
		if (Data.Instance.levelsData.actualDiamondLevel <= id) {
			Events.PortalFinalUnavailable ();
			return false;
		}
		Game.Instance.mode = Game.modes.FREEZED;
		if (state != states.OPENED) {
			Data.Instance.userData.OpenPortal (id);
			state = states.OPENED;
			anim.Play ("portal_open");
			Invoke ("ChangeLevel", 5);
		} else {
			ChangeLevel ();
		}
		return true;
	}
	void ChangeLevel()
	{
		Data.Instance.userData.SaveSpecificLastPosition (destinationCoords);
		switch (gotoLevel) {
		case 1:
			Data.Instance.LoadScene ("World");
			break;
		case 2:
			Data.Instance.LoadScene ("World2");
			break;
		case 3:
			Data.Instance.LoadScene ("World3");
			break;
		}

	}
}