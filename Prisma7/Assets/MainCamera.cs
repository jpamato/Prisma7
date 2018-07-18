using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public Vector3 fruitNinja_Offset;
	public GameObject target;
	public states state;
	public enum states
	{
		FOLLOWING_CHARACTER,
		FRUIT_NINJA
	}

	void Start () {
		state = states.FOLLOWING_CHARACTER;
		Events.OpenFruitNinja += OpenFruitNinja;
		Events.CloseFruitNinja += CloseFruitNinja;
	}
	void OnDestroy()
	{
		Events.OpenFruitNinja -= OpenFruitNinja;
		Events.CloseFruitNinja -= CloseFruitNinja;
	}
	void Update () {
		if (state == states.FRUIT_NINJA)
			FruitNinjaUpdate ();
		else
			FollowingUpdate ();
	}
	void CloseFruitNinja()
	{
		state = states.FOLLOWING_CHARACTER;
	}
	Transform fruitNinjaTarget;
	public void OpenFruitNinja(InteractiveObject io)
	{
		state = states.FRUIT_NINJA;
		this.fruitNinjaTarget = io.transform;
	}
	void FollowingUpdate()
	{
		Vector3 pos = target.transform.localPosition;
		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 0.1f);
	}
	void FruitNinjaUpdate()
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition, fruitNinjaTarget.transform.localPosition + fruitNinja_Offset, 0.3f);
	}
}
