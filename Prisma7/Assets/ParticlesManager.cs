using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour {

	public GameObject bubbles;
	public GameObject bomb;

	public void SetOn (Fruit.types fruitType, Vector3 pos) {
		
		GameObject go = null;
		switch (fruitType) {
		case Fruit.types.BAD:
			go = bomb;
			break;
		case Fruit.types.GENERIC:
			go = bubbles;
			break;
		}

		if (go == null)
			return;
		
		GameObject newGO = Instantiate (go);
		newGO.transform.SetParent (transform);
		newGO.transform.position = pos;
	}
}
