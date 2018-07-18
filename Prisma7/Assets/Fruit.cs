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

	public void Init(Vector2 speed) {
		speedV = (Random.Range (200, 400)/20) * speed.y;
		speedH = Random.Range (-200, 200) * speed.x;
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
