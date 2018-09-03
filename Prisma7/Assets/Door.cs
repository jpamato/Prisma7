using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject {

	public int id;
	public states state;
	public enum states
	{
		CLOSED,
		OPENED
	}
	public GameObject closed;
	public GameObject opened;

	void Start()
	{
		
	}
	public void SetState(states state)
	{
		this.state = state;
		switch (state) {
		case states.CLOSED:
			closed.SetActive (true);
			opened.SetActive (false);
			break;
		case states.OPENED:
			closed.SetActive (false);
			opened.SetActive (true);
			break;
		}
	}
}
