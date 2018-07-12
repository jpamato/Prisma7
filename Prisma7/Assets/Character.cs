using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public GameObject target;

	MoveTo moveTo;
	CharacterAnimations anim;

	InteractiveObject selectedInteractiveObject;

	void Start () {
		
		moveTo = GetComponent<MoveTo> ();
		anim = GetComponent<CharacterAnimations> ();

		Events.OnFloorClicked += OnFloorClicked;
		Events.OnCharacterStopWalking += OnCharacterStopWalking;
		Events.OnCharacterHitInteractiveObject += OnCharacterHitInteractiveObject;
	}
	void OnCharacterHitInteractiveObject(InteractiveObject io)
	{
		selectedInteractiveObject = io;
		Vector3 newPos = io.transform.localPosition;
		newPos.z -= 2;
		OnFloorClicked (newPos);
	}
	void OnFloorClicked (Vector3 pos) {
		target.transform.position = pos;
		LookAtTarget (target);
		Vector3 rot = transform.localEulerAngles;
		rot.x = rot.z = 0;
		transform.localEulerAngles = rot;
		moveTo.Init (pos);
		anim.Walk ();
	}
	void LookAtTarget(GameObject lookAtTarget)
	{
		Vector3 pos = lookAtTarget.transform.localPosition;
		pos.y = transform.localPosition.y;
		transform.LookAt (pos);
	}
	void OnCharacterStopWalking()
	{
		if (selectedInteractiveObject != null) {
			LookAtTarget (selectedInteractiveObject.transform.gameObject);
			Events.OpenFruitNinja ();
		}

		selectedInteractiveObject = null;
		anim.Idle ();
	}
}
