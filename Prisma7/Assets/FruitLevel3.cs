using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLevel3 : Fruit {
	
	public float speedX;
	public float speedY;

	public override void Init(Vector2 speed, int direction) {
		//base.Init (speed*10, direction);
		speedY = Random.Range(100, 500);
		speedX = Random.Range(400, 900);

		Vector2 pos = transform.localPosition;
		pos.y = speedY;
		pos.x = -600;
		transform.localPosition = pos;

	}
	void Update()
	{
		Vector2 pos = transform.localPosition;
		pos.y = speedY;
		pos.x += speedX * Time.deltaTime;
		transform.localPosition = pos;
		if (pos.x > 600)
			Destroy (this.gameObject);
	}
}