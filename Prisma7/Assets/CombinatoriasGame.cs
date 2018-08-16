using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinatoriasGame : MateGame {

	public List<GameObject> gemasButtons;
	public List<Material> gemasOKMaterials;
	public GameObject gemasOKPS1,gemasOKPS2;
	public GameObject gemasWrongPS1,gemasWrongPS2;

	public int partidaGames;
	int gamesPlayeds;

	// Use this for initialization
	void Start () {		
		levelBarStep = 1f / times2FullBar;
		Events.OnTimeOver += TimeOver;
		Invoke ("Init", 5);
	}

	void Init(){
		doneSign.SetActive (false);
		loseSign.SetActive (false);



		SetBarColor ();
		InitTimer ();

		state = states.PLAYING;
	}

	void OnDestroy(){
		Events.OnTimeOver -= TimeOver;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StopFigurWrongPS(){
		gemasWrongPS1.SetActive (false);
		gemasWrongPS2.SetActive (false);
	}

	void StopFiguraOKPS(){
		gemasOKPS1.SetActive (false);
		gemasOKPS2.SetActive (false);
	}

	void FiguraComplete(){
		//Events.FiguraComplete (figura.go.name);
		Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		foreach (GameObject go in gemasButtons)
			Destroy (go);
		gemasButtons.Clear ();
		//Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			Data.Instance.figurasData.ResetFiguresDone ();
			Invoke ("BackToWorld", 3);
		} else {
			doneSign.SetActive (true);
			gamesPlayeds++;
			if (gamesPlayeds >= partidaGames) 
				Invoke ("BackToWorld", 3);
			else
				Invoke ("Init", 3);
		}		
	}

	void TimeOver(){
		state = states.ENDED;
		loseSign.SetActive (true);
		foreach (GameObject go in gemasButtons)
			Destroy (go);
		gemasButtons.Clear ();

		/*Destroy (figuraGO);
		figura.ClearRunas ();*/

		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
