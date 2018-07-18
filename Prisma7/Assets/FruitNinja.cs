using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitNinja : MonoBehaviour {

	public FruitsManager fruitsManager;
	public ProgressBar progressBar;
	InteractiveObject interactiveObject;
	public int totalPoints;
	int points;

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
			Done ();
		}			
		progressBar.SetValue (1-(float)points/(float)totalPoints);
	}
	void Done()
	{
		fruitsManager.Reset ();
		fruitsManager.gameObject.SetActive (false);
		Game.Instance.ChangeMode (Game.modes.WORLD);
		Events.CloseFruitNinja ();
		interactiveObject.GetComponent<Door> ().OnOpen ();
	}

}
