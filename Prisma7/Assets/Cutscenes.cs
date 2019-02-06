using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour {

	public GameObject intro;
	public GameObject outro;

	void Start () {
		Data.Instance.ui.SetStatus (false);

		if (Data.Instance.levelsData.showOutro) {
			Events.OnVoice ("Outro/all");
			intro.SetActive (false);
			outro.SetActive (true);
		} else {			
			Events.OnVoice ("Intro/all");
			intro.SetActive (true);
			outro.SetActive (false);
		}
	}
	void OnDestroy()
	{
		Events.OnVoice ("");
	}
	float timer;
	void Update () {
		timer += Time.deltaTime;

		int totalTime = 1;
		if (Data.Instance.levelsData.showOutro)
			totalTime = 10;
		
		if (timer < 1)
			return;
		if (Input.GetMouseButtonDown (0)) {
			if (Data.Instance.levelsData.showOutro) {
				Data.Instance.LoadScene ("World3");
			} else {
				Data.Instance.LoadScene ("CharacterSelector");
			}
		}
	}
}
