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
	public void OnClicked()
	{
		if (done)
			return;

		done = true;
		Events.OnFruitNinjaClickedBubble (type);
		Destroy (this.gameObject);
	}
}
