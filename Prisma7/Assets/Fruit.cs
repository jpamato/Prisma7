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

	public virtual void Init(Vector2 speed, int direction) {
		this.speed = speed/3.2f;
		speedV = (Random.Range (200, 400)/20) * speed.y;
		speedH = Random.Range (0, 150*direction) * speed.x;
		transform.localPosition = new Vector3(Random.Range(0,300*-direction),0,0);
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
