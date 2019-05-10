using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLevel4 : Fruit {

	public float speedX;
	public float speedY;
	int dir = 1;

	public override void Init(Vector2 speed, int direction) {
		//base.Init (speed*10, direction);
		speedY = Random.Range(100, 500);
		speedX = Random.Range(400, 900);

		Vector2 pos = transform.localPosition;
		pos.y = speedY;
		if (Random.Range (0, 10) < 5)
			dir = -1;
		pos.x = 600 * -dir;
		transform.localPosition = pos;
	}
	void Update()
	{
		Vector2 pos = transform.localPosition;
		pos.y = speedY;
        pos.x += speedX * Time.deltaTime * dir;
		transform.localPosition = pos;
		if (pos.x > 805 || pos.x < -805)
			Destroy (this.gameObject);
	}
}