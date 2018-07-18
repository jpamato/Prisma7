using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGeneric : Fruit {
	

	void Update()
	{
		Vector2 pos = transform.localPosition;
		speedV -= 10;
		pos.y += speedV * Time.deltaTime;
		pos.x += speedH * Time.deltaTime;
		transform.localPosition = pos;
		if (pos.y < 0)
			Destroy (this.gameObject);
	}
}
