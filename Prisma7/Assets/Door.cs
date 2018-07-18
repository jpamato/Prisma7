using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject {

	public states state;
	public enum states
	{
		CLOSED,
		OPENED
	}
	public GameObject closed;
	public GameObject opened;

	public void OnOpen() {
		
		state = states.OPENED;
		closed.SetActive (false);
		opened.SetActive (true);
	}
}
