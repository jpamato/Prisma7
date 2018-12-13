using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour {

	public GameObject[] bubbles;
	public GameObject bomb;
	public int id = 0;

	public void SetOn (Fruit.types fruitType, Vector3 pos) {
		
		GameObject go = null;
		switch (fruitType) {
		case Fruit.types.BAD:
			id = 0;
			go = bomb;
			break;
		case Fruit.types.GENERIC:			
			if (id > 3)
				id = 3;
			go = bubbles[id];
			id++;
			break;
		}

		if (go == null)
			return;
		
		GameObject newGO = Instantiate (go);
		newGO.transform.SetParent (transform);
		newGO.transform.position = pos;
	}
}
