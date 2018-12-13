using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSFX : MonoBehaviour {

	public AudioClip clickSfx;
	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		Events.ClickSfx += ClickSfxPlay;
	}

	void OnDestroy(){
		Events.ClickSfx -= ClickSfxPlay;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void ClickSfxPlay(){
		source.PlayOneShot (clickSfx);
	}

}
