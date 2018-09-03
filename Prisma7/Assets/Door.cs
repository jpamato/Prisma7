using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject {

	public int id;
	public states state;
	public enum states
	{
		CLOSED,
		OPENED,
		OPENING
	}
	public GameObject closed;
	public GameObject opened;
	public GameObject opening;

	public void SetState(states state)
	{
		closed.SetActive (false);
		opened.SetActive (false);
		opening.SetActive (false);

		this.state = state;
		switch (state) {
		case states.CLOSED:
			closed.SetActive (true);
			break;
		case states.OPENING:
			opening.SetActive (true);
			opened.SetActive (true);
			Data.Instance.userData.OnDoorPlayed (id);
			break;
		case states.OPENED:
			opened.SetActive (true);
			break;
		}
	}
}
