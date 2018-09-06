using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathGameBG_SFX : MonoBehaviour {

	public AudioClip trueno;
	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(string s){
		string[] commands = s.Split (',');

		if (commands[0] == "trueno") {
			//source.clip = trueno;
			source.pitch = Random.Range (0.5f, 2f);
			if (commands.Length > 0)
				source.panStereo = float.Parse(commands [1]);
			
			source.PlayOneShot (trueno);
		}
	}
}
