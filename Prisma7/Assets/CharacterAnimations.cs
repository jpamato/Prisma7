using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {

	public Animation anim_he;
	public Animation anim_she;

	Animation anim;

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
		if (Data.Instance.userData.sex == UserData.sexs.HE) {
			anim = anim_he;
			anim_she.gameObject.SetActive (false);
			anim_he.gameObject.SetActive (true);
		} else {
			anim = anim_she;
			anim_he.gameObject.SetActive (false);
			anim_she.gameObject.SetActive (true);
		}
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
