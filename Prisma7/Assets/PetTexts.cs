using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetTexts : MonoBehaviour {

	public Text field;
	public GameObject panel;


	void Start () {
		Reset ();
		Events.OnPetSay += OnPetSay;
	}
	void OnDestroy () {
		Events.OnPetSay -= OnPetSay;
	}

	void OnPetSay (string text)
	{
		CancelInvoke ();
		panel.SetActive (true);
		field.text = text;
		float length = 2 + ((float)text.Length/20);
		Invoke ("Reset", length);
	}
	void Reset()
	{
		panel.SetActive (false);
	}
}
