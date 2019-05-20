using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsManager : MonoBehaviour {

	public Vector2 speed;

	public Fruit genericFruit;
	public Fruit badFruit;

	public Fruit genericFruit2;
	public Fruit badFruit2;

	public Fruit genericFruit3;
	public Fruit badFruit3;

	public Fruit genericFruit4;
	public Fruit badFruit4;

	public Fruit genericFruit5;
	public Fruit badFruit5;

	public Transform target;

	[HideInInspector]
	public AudioSource audioSource;

	public void Init() {
		audioSource = transform.parent.GetComponent<AudioSource> ();
		Loop ();
	}
	public void Reset() {
		CancelInvoke ();
		Invoke ("Delayed", 0.2f);
	}
	void Delayed()
	{
		Utils.RemoveAllChildsIn (target);
	}
	void Loop () {
		Invoke ("Loop", 0.5f);

		if (Random.Range (0, 10) < 3)
			return;
		
		Fruit fruit = genericFruit2;
		if (Random.Range (0, 10) < 3)
			AddBad();
		else
			AddGood ();
	}
	void AddGood()
	{
		Fruit fruit = genericFruit;
		if (Data.Instance.levelsData.actualDiamondLevel > 3 && Random.Range (0, 10) < 5) 
			fruit = genericFruit5;
		else if (Data.Instance.levelsData.actualDiamondLevel > 2 && Random.Range (0, 10) < 5) 
			fruit = genericFruit4;
		else if (Data.Instance.levelsData.actualDiamondLevel > 1 && Random.Range (0, 10) < 5) 
			fruit = genericFruit3;
		else if (Data.Instance.levelsData.actualDiamondLevel > 0 && Random.Range (0, 10) < 5) 
			fruit = genericFruit2;
		AddFruit (fruit);
	}
	void AddBad()
	{
		Fruit fruit = badFruit;
		if (Data.Instance.levelsData.actualDiamondLevel > 3 && Random.Range (0, 10) < 5) 
			fruit = badFruit5;
		else if (Data.Instance.levelsData.actualDiamondLevel > 2 && Random.Range (0, 10) < 5) 
			fruit = badFruit4;
		else if (Data.Instance.levelsData.actualDiamondLevel > 1 && Random.Range (0, 10) < 5) 
			fruit = badFruit3;
		else if (Data.Instance.levelsData.actualDiamondLevel > 0 && Random.Range (0, 10) < 5) 
			fruit = badFruit2;

		AddFruit (fruit);
	}
	void AddFruit(Fruit newFruit)
	{
		Fruit fruit = Instantiate (newFruit);
		fruit.transform.SetParent (target);
		fruit.transform.localScale = new Vector2 (0.75f, 0.75f);

		int dir = 1;
		if (Random.Range (0, 10) < 5)
			dir = -1;
		fruit.Init (speed, dir);
	}
}
