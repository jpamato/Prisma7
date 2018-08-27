using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsManager : MonoBehaviour {

	public Vector2 speed;
	public Fruit genericFruit;
	public Fruit badFruit;

	public Transform target;

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
		fruit.Init (speed);
	}
}
