using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip mainMusic, ingameMusic;

	public AudioSource source;

	public void SetIngameMusic(){
		source.clip = ingameMusic;
		source.volume = 0.65f;
		source.Play ();
	}

	public void SetMainMusic(){
		source.clip = mainMusic;
		source.volume = 1f;
		source.Play ();
	}

	public void MusicStop(){
		source.Stop ();
	}
}
