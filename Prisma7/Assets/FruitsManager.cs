using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsManager : MonoBehaviour {

	public Vector2 speed;

	public Fruit genericFruit;
	public Fruit badFruit;

	public Fruit genericFruit2;
	public Fruit badFruit2;

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
		if (Data.Instance.levelsData.actualDiamondLevel == 1 && Random.Range (0, 10) < 5)
			fruit = genericFruit2;
		AddFruit (fruit);
	}
	void AddBad()
	{
		Fruit fruit = badFruit;
		if (Data.Instance.levelsData.actualDiamondLevel == 1 && Random.Range (0, 10) < 5)
			fruit = badFruit2;
		AddFruit (fruit);
	}
	void AddFruit(Fruit newFruit)
	{
		Fruit fruit = Instantiate (newFruit);
		fruit.transform.SetParent (target);
		fruit.transform.localScale = new Vector2 (0.5f, 0.5f);

		int dir = 1;
		if (Random.Range (0, 10) < 5)
			dir = -1;
		fruit.Init (speed, dir);

		fruit.transform.localPosition = new Vector3(Random.Range(0,300*-dir),0,0);
	}
}
