using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGeneric : Fruit {
	
	void FixedUpdate()
	{
		Vector2 pos = transform.localPosition;
		speedV -= (speed.y) /3;
		pos.y += speedV * Time.deltaTime;
		pos.x += speedH * Time.deltaTime;
		transform.localPosition = pos;
		if (pos.y < 0)
			Destroy (this.gameObject);
	}
}
