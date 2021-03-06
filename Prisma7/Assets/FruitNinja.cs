﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitNinja : MonoBehaviour {

	public FruitsManager fruitsManager;
	InteractiveObject interactiveObject;
	int totalPoints;
	int points;
	bool playing;
	float timer = 0;
	public float duration;
	public AudioClip ninjaOK, ninjaWrong,ninjaDone, rompehielo, glass;

	void Start () {
		fruitsManager.gameObject.SetActive (false);
		Events.OpenFruitNinja += OpenFruitNinja;
		Events.OnFruitNinjaClickedBubble += OnFruitNinjaClickedBubble;
	}
	void OnDestroy()
	{
		Events.OnFruitNinjaClickedBubble -= OnFruitNinjaClickedBubble;
		Events.OpenFruitNinja -= Events.OpenFruitNinja;
	}
	void OpenFruitNinja(InteractiveObject _interactiveObject)
	{
		totalPoints = 3 + (Data.Instance.levelsData.actualDiamondLevel * 2);
		timer = 0;
		Data.Instance.ui.StartTimer ();
		playing = true;
		Events.OnDragger (true);
		this.interactiveObject = _interactiveObject;
		points = 0;
		Data.Instance.ui.progressBar.SetValue (1);
		Game.Instance.ChangeMode (Game.modes.FRUIT_NINJA);
		fruitsManager.gameObject.SetActive (true);
		fruitsManager.Init ();
		interactiveObject.GetComponent<Door> ().Reset ();
		interactiveObject.GetComponent<Door> ().SetProgress (1);
	}

	void OnFruitNinjaClickedBubble(Fruit.types type)
	{		
		switch (type) {
		case Fruit.types.GENERIC:
			fruitsManager.audioSource.PlayOneShot (ninjaOK);
			fruitsManager.audioSource.PlayOneShot (rompehielo);
			points += 1;
			break;
		case Fruit.types.BAD:
			fruitsManager.audioSource.PlayOneShot (ninjaWrong);
			points -= 1;
			break;
		}
		if (points < 0)
			points = 0;
		else if (points >= totalPoints) {
			points = totalPoints;
			Done (true);
		}			
		float v = 1 - (float)points / (float)totalPoints;
		Data.Instance.ui.progressBar.SetValue (v);
		interactiveObject.GetComponent<Door> ().SetProgress (v);
	}
	void Done(bool win)
	{
		Data.Instance.ui.HideTimer ();
		fruitsManager.audioSource.PlayOneShot (ninjaDone);
		playing = false;
		Events.OnDragger (false);
		fruitsManager.Reset ();
		fruitsManager.gameObject.SetActive (false);
		//Invoke ("CloseManager", 2);
		Game.Instance.ChangeMode (Game.modes.WORLD);
		Events.CloseFruitNinja (win);

		if (win) {
			interactiveObject.GetComponent<Door> ().SetState (Door.states.OPENING);
			AchievementsEvents.NewPointToAchievement (Achievement.types.FRUIT_NINJA_WIN);
		}

		fruitsManager.audioSource.PlayOneShot (glass);
	}

	void CloseManager(){
		fruitsManager.gameObject.SetActive (false);
	}

	void Update()
	{
		if (!playing)
			return;
		timer += Time.deltaTime;
		if (timer >= duration) {
			Done (false);
		}
		Data.Instance.ui.timer.SetValue (1-(timer/duration));
	}

}
