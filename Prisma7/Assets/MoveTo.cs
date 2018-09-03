using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {

	Vector3 destination;
	public float speed;
	bool isOn;

	public void Init(Vector3 _destination) {
		this.destination = _destination;
		isOn = true;
	}

	void Update () {
		if (!isOn)
			return;

		destination.y = transform.localPosition.y;

		float step = speed * Time.deltaTime;

		transform.position = Vector3.MoveTowards(transform.position, destination, step);

		if (Vector3.Distance (transform.position , destination) < 0.3f) {
			Events.OnCharacterStopWalking ();
			isOn = false;
		}

	}
}
