using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

	public Animation anim;
	public types type;
	bool done;
	public enum types
	{
		GENERIC,
		BAD
	}
	public float speedV;
	public float speedH;
	public Vector2 speed;

	public void Init(Vector2 speed) {
		this.speed = speed;
		speedV = (Random.Range (200, 400)/20) * speed.y;
		speedH = Random.Range (-100, 100) * speed.x;
	}

	public void OnClicked()
	{
		if (done)
			return;

		done = true;
		Events.OnFruitNinjaClickedBubble (type);
		Destroy (this.gameObject);
	}
}
