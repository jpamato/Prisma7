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
		timer = 0;
		playing = true;
		Events.OnDragger (true);
		this.interactiveObject = _interactiveObject;
		points = 0;
		progressBar.SetValue (1);
		Game.Instance.ChangeMode (Game.modes.FRUIT_NINJA);
		fruitsManager.gameObject.SetActive (true);
		fruitsManager.Init ();
	}
	void OnFruitNinjaClickedBubble(Fruit.types type)
	{		
		switch (type) {
		case Fruit.types.GENERIC:
			points += 1;
			break;
		case Fruit.types.BAD:
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
		playing = false;
		Events.OnDragger (false);
		fruitsManager.Reset ();
		fruitsManager.gameObject.SetActive (false);
		Game.Instance.ChangeMode (Game.modes.WORLD);
		Events.CloseFruitNinja ();

		if(win)
			interactiveObject.GetComponent<Door> ().OnOpen ();
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
