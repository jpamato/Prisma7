using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

	public float smooth;
	public Vector3 offset;
	public Character character;

	void Update () {
		Vector3 newPos = character.transform.localPosition;
		transform.localPosition =Vector3.Lerp (transform.localPosition, newPos + offset, smooth);
	}
}
