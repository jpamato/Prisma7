using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip mainMusic, ingameMusic;

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
