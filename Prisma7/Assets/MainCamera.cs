using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject target;

	void Start () {
		
	}

	void Update () {
		Vector3 pos = target.transform.localPosition;
		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 0.1f);
	}
}
