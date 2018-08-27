using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitNinja : MonoBehaviour {

	public FruitsManager fruitsManager;
	public ProgressBar progressBar;
	public ProgressBar timerProgressBar;
	InteractiveObject interactiveObject;
	public int totalPoints;
	int points;
	bool playing;
	float timer = 0;
	public float duration;
	public AudioClip ninjaOK, ninjaWrong,ninjaDone;

	AudioSource clockSource;

	void Start () {
		fruitsManager.gameObject.SetActive (false);
		clockSource = timerProgressBar.gameObject.GetComponent<AudioSource> ();
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
		timer = 0;
		playing = true;
		Events.OnDragger (true);
		this.interactiveObject = _interactiveObject;
		points = 0;
		progressBar.SetValue (1);
		Game.Instance.ChangeMode (Game.modes.FRUIT_NINJA);
		fruitsManager.gameObject.SetActive (true);
		fruitsManager.Init ();
		clockSource.Play ();
	}
	void OnFruitNinjaClickedBubble(Fruit.types type)
	{		
		switch (type) {
		case Fruit.types.GENERIC:
			fruitsManager.audioSource.PlayOneShot (ninjaOK);
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
		progressBar.SetValue (1-(float)points/(float)totalPoints);
	}
	void Done(bool win)
	{
		fruitsManager.audioSource.PlayOneShot (ninjaDone);
		playing = false;
		Events.OnDragger (false);
		fruitsManager.Reset ();
		fruitsManager.gameObject.SetActive (false);
		//Invoke ("CloseManager", 2);
		Game.Instance.ChangeMode (Game.modes.WORLD);
		Events.CloseFruitNinja (win);
		clockSource.Stop ();
		if(win)
			interactiveObject.GetComponent<Door> ().OnOpen ();
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
		timerProgressBar.SetValue (timer/duration);
	}

}
