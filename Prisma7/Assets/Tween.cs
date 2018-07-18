using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour {

	public float speed;
	bool doTween;
	Vector3 origin, target;
	float tweenFactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (doTween) {
			tweenFactor += speed;
			transform.localPosition = Vector3.Lerp (origin, target, tweenFactor);
			if (tweenFactor >= 1f)
				doTween = false;
		}
		
		
	}

	public void SetTween(Vector3 targetPos, float nSpeed){
		speed = nSpeed;
		SetTween (targetPos);
	}

	public void SetTween(Vector3 targetPos){
		target = targetPos;
		origin = transform.localPosition;
		doTween = true;
	}
}
