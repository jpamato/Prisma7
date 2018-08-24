using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {

	public Animation anim;
	public AudioClip walkSfx;

	public states state;

	AudioSource asource;

	public enum states
	{
		IDLE,
		WALK,
		ENTER
	}
	void Start()
	{
		asource = GetComponent<AudioSource> ();
		Idle ();
	}
	public void Idle () {
		//audio
		asource.Stop ();

		anim.Play ("idle2");
		state = states.IDLE;
	}
	public void Walk () {
		//audio
		asource.Stop ();
		asource.clip = walkSfx;
		asource.loop = true;
		asource.Play ();

		anim.Play ("walk");
		state = states.WALK;
	}
	public void Enter () {
		//audio
		asource.Stop ();

		anim.Play ("enter");
		state = states.ENTER;
	}
	public void Cheer () {
		//audio
		asource.Stop ();

		anim.Play ("festejo");
	}

}
