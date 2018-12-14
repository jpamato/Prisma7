using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour {

	public GameObject intro;
	public GameObject outro;

	void Start () {
		Data.Instance.ui.SetStatus (false);
		intro.SetActive (true);
		outro.SetActive (false);
	}
	float timer;
	void Update () {
		timer += Time.deltaTime;
		if (timer < 1)
			return;
		if (Input.GetMouseButtonDown (0)) {
			Data.Instance.LoadScene ("CharacterSelector");
		}
	}
}
