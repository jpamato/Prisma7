using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLevel2 : Fruit {

	void Update()
	{
		Vector2 pos = transform.localPosition;
		speedV -= (speed.y) /3;

        float deltaTime = Time.deltaTime;

        pos.y += speedV * deltaTime;
		pos.x += speedH * deltaTime;

		transform.localPosition = pos;

		if (pos.y < 0)
			Destroy (this.gameObject);
	}
}
