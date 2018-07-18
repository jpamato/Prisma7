using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public states state;
	public enum states{
		PLAYING,
		OPENING_FRUIT_NINJA,
		ENTERING_DOOR
	}
	public GameObject target;

	MoveTo moveTo;
	CharacterAnimations anim;

	InteractiveObject selectedInteractiveObject;

	void Start () {
		state = states.PLAYING;
		moveTo = GetComponent<MoveTo> ();
		anim = GetComponent<CharacterAnimations> ();

		Events.OnFloorClicked += OnFloorClicked;
		Events.CloseFruitNinja += CloseFruitNinja;
		Events.OnCharacterStopWalking += OnCharacterStopWalking;
		Events.OnCharacterHitInteractiveObject += OnCharacterHitInteractiveObject;
	}
	void OnDestroy () {
		Events.OnFloorClicked -= OnFloorClicked;
		Events.CloseFruitNinja -= CloseFruitNinja;
		Events.OnCharacterStopWalking -= OnCharacterStopWalking;
		Events.OnCharacterHitInteractiveObject -= OnCharacterHitInteractiveObject;
	}
	void CloseFruitNinja()
	{
		state = states.PLAYING;
	}
	void OnCharacterHitInteractiveObject(InteractiveObject io)
	{
		if (state != states.PLAYING)
			return;
		Door door = io.GetComponent<Door> ();
		if (door == null)
			return;
		if (door.state == Door.states.CLOSED) {
			state = states.OPENING_FRUIT_NINJA;
			selectedInteractiveObject = io;
			Vector3 newPos = io.transform.localPosition;
			newPos.z -= 2;
			OnFloorClicked (newPos);
		} else {
			state = states.ENTERING_DOOR;
			selectedInteractiveObject = io;
			Vector3 newPos = io.transform.localPosition;
			newPos.z -= 0.5f;
			OnFloorClicked (newPos);
			Invoke ("EnterMinigame", 1);
		}
	}
	void EnterMinigame()
	{
		if (state == states.ENTERING_DOOR) {
			Data.Instance.LoadScene ("Figuras");
		}
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
			if (state == states.OPENING_FRUIT_NINJA) {
				LookAtTarget (selectedInteractiveObject.transform.gameObject);
				Events.OpenFruitNinja (selectedInteractiveObject);
			}
		}

		selectedInteractiveObject = null;
		anim.Idle ();
	}
}
