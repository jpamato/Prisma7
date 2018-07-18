using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsManager : MonoBehaviour {

	public Fruit genericFruit;
	public Fruit badFruit;

	public Transform target;

	public void Init() {
		Loop ();
	}
	public void Reset() {
		CancelInvoke ();
		Invoke ("Delayed", 0.1f);
	}
	void Delayed()
	{
		Utils.RemoveAllChildsIn (target);
	}
	void Loop () {
		Invoke ("Loop", 0.25f);

		Fruit fruit = genericFruit;
		if (Random.Range (0, 10) < 3)
			fruit = badFruit;
		
		AddFruit (fruit);
	}
	void AddFruit(Fruit newFruit)
	{
		Fruit fruit = Instantiate (newFruit);
		fruit.transform.SetParent (target);
		fruit.transform.localScale = new Vector2 (0.5f, 0.5f);
		fruit.transform.localPosition = Vector3.zero;
	}
}
