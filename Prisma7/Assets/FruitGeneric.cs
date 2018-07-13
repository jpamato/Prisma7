using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGeneric : Fruit {

	public float speedV;
	public float speedH;

	void Start () {
		speedV = Random.Range (200, 400)/20;
		speedH = Random.Range (-200, 200);
	}
	void Update()
	{
		Vector2 pos = transform.localPosition;
		speedV -= Time.deltaTime*10;
		pos.y += speedV;
		pos.x += speedH*Time.deltaTime;
		transform.localPosition = pos;
		if (pos.y < 0)
			Destroy (this.gameObject);
	}
}
