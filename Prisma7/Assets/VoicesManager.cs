using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicesManager : MonoBehaviour {

	public AudioSource audioSource;
	TipsManager tipsManager;

	void Awake () {
		Events.OnVoice += OnVoice;
		Events.OnPetSay += OnPetSay;
		tipsManager = GetComponent<TipsManager> ();
	}
	void OnDestroy () {
		Events.OnVoice -= OnVoice;
		Events.OnPetSay -= OnPetSay;
	}
	void OnPetSay(string fileName)
	{
		string n = tipsManager.GetNameByValue (fileName);
		OnVoice ("Tips/" + n);
	}
	void OnVoice (string fileName) {
		string file = "voices/" + fileName;
		//Debug.Log ("OnVoice: " + file);
		AudioClip ac = Resources.Load<AudioClip> (file);
		audioSource.clip = ac;
		audioSource.Play ();
	}
}
