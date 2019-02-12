using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour {

	public states state;
	public enum states{
		PLAYING,
		OPENING_FRUIT_NINJA,
		ENTERING_DOOR,
		CHANGING_LEVEL
	}
	public GameObject target;

	MoveTo moveTo;
	CharacterAnimations anim;

	InteractiveObject selectedInteractiveObject;

	string nextScene;

	void Start () {
		state = states.PLAYING;
		moveTo = GetComponent<MoveTo> ();
		anim = GetComponent<CharacterAnimations> ();

		Events.OnFloorClicked += OnFloorClicked;
		Events.CloseFruitNinja += CloseFruitNinja;
		Events.OnCharacterStopWalking += OnCharacterStopWalking;
		Events.OnCharacterHitInteractiveObject += OnCharacterHitInteractiveObject;
		//Events.OnMinigameDone += OnMinigameDone;
		transform.localPosition = Data.Instance.userData.lastPosition;

		if(Data.Instance.lastLevel == "Pociones" || 
			Data.Instance.lastLevel == "Combinatorias" || 
			Data.Instance.lastLevel == "Grilla" || 
			Data.Instance.lastLevel == "Figuras" )
		OnMinigameDone ();
	}
	void OnDestroy () {
		Events.OnFloorClicked -= OnFloorClicked;
		Events.CloseFruitNinja -= CloseFruitNinja;
		Events.OnCharacterStopWalking -= OnCharacterStopWalking;
		Events.OnCharacterHitInteractiveObject -= OnCharacterHitInteractiveObject;
		//Events.OnMinigameDone -= OnMinigameDone;
	}
	void CloseFruitNinja(bool win)
	{
		state = states.PLAYING;
		if (win)
			anim.Cheer ();			
	}
	void OnMinigameDone()
	{
		Vector3 pos = transform.localPosition;
		pos.z -= 1f;
		transform.localPosition = pos;

		Vector3 rot = transform.localEulerAngles;
		rot.y = 180;
		transform.localEulerAngles = rot;
	}

	void OnCharacterHitInteractiveObject(InteractiveObject io)
	{
		if (state != states.PLAYING || state == states.CHANGING_LEVEL)
			return;

		PortalLevel portalLevel = io.GetComponent<PortalLevel> ();
		if (portalLevel != null) {
			if (portalLevel.ChekToCross ()) {
				selectedInteractiveObject = io;
				anim.Idle ();
				state = states.CHANGING_LEVEL;
				return;
			}
		}
		
		Door door = io.GetComponent<Door> ();




		if (door == null) 
			return;
		if (door.state == Door.states.UNAVAILABLE) {
			Events.PortalUnavailable ();
			return;
		}
		
		if (door.state == Door.states.CLOSED) {			
			selectedInteractiveObject = io;
			Vector3 newPos = io.transform.localPosition;
			newPos.z -= 1.5f;
			OnFloorClicked (newPos);
			state = states.OPENING_FRUIT_NINJA;
		} else {			
			selectedInteractiveObject = io;
			Door d = selectedInteractiveObject as Door;

			int mgCount = Enum.GetNames (typeof(Data.minigamesScenes)).Length;

			int random = UnityEngine.Random.Range (0, mgCount);

			nextScene = ((Data.minigamesScenes)random).ToString ();
			Vector3 newPos = io.transform.localPosition;
			newPos.z -= 0.35f;
			OnFloorClicked (newPos);
			state = states.ENTERING_DOOR;
		}
	}
	IEnumerator EnterMinigame()
	{
		anim.Enter ();
		yield return new WaitForSeconds (1.25f);
		Data.Instance.musicManager.SetIngameMusic ();
		Data.Instance.LoadScene (nextScene);
	}
	void OnFloorClicked (Vector3 pos) {

		if ( state == states.CHANGING_LEVEL)
			return;
		if (state == states.ENTERING_DOOR)
			return;
		else if (state == states.OPENING_FRUIT_NINJA)
			state = states.PLAYING;
		
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
		if ( state == states.CHANGING_LEVEL)
			return;
		if (selectedInteractiveObject != null) {
			if (state == states.OPENING_FRUIT_NINJA) {
				LookAtTarget (selectedInteractiveObject.transform.gameObject);
				Data.Instance.userData.SaveLastPosition ();
				Events.OpenFruitNinja (selectedInteractiveObject);
			}
		}

		selectedInteractiveObject = null;
		if (state == states.ENTERING_DOOR) {
			Data.Instance.userData.SaveLastPosition ();
			StartCoroutine (EnterMinigame ());
			return;
		}		

		anim.Idle ();
	}
}
