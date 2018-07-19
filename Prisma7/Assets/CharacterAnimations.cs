using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {

	public Animation anim;

	public states state;

	public enum states
	{
		IDLE,
		WALK,
		ENTER
	}
	void Start()
	{
		Idle ();
	}
	public void Idle () {
		anim.Play ("idle2");
		state = states.IDLE;
	}
	public void Walk () {
		anim.Play ("walk");
		state = states.WALK;
	}
	public void Enter () {
		anim.Play ("enter");
		state = states.ENTER;
	}
	public void Cheer () {
		anim.Play ("festejo");
	}

}
