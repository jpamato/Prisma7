﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLevel : InteractiveObject {

	public bool isLastPortal;
	public Animator anim;

	public int id;
	public int gotoLevel;
	public Vector3 destinationCoords;

	AudioSource audiosource;

	public states state;
	public enum states
	{
		UNAVAILABLE,
		OPENED
	}


	void Start () {
		audiosource = GetComponent<AudioSource> ();
		if (Data.Instance.levelsData.actualDiamondLevel >= 2 && Data.Instance.userData.GetPortalIDOpened() >= id) {
			state = states.OPENED;
			anim.Play ("portal_unlocked");
		}
	}
	public bool ChekToCross()
	{
		if (isLastPortal)
		{
			if (Data.Instance.levelsData.actualDiamondLevel == 6 && !Data.Instance.levelsData.showOutro) {
				Data.Instance.levelsData.showOutro = true;
				Data.Instance.LoadScene ("cutscenes");
			}
			return false;
		}
		if (Data.Instance.levelsData.actualDiamondLevel <= id) {
			Events.PortalFinalUnavailable ();
			return false;
		}
		Game.Instance.mode = Game.modes.FREEZED;
		if (state != states.OPENED) {
			Data.Instance.userData.OpenPortal (id);
			state = states.OPENED;
			anim.Play ("portal_open");
			audiosource.Play ();
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
			Events.OnChangeWorld (1);
			Data.Instance.LoadScene ("World");
			break;
		case 2:
			Events.OnChangeWorld (2);
			Data.Instance.LoadScene ("World2");
			break;
		case 3:
			Events.OnChangeWorld (3);
			Data.Instance.LoadScene ("World3");
			break;
		}

	}
}