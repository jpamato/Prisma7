using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour {

	public Camera cam;

	void Update()
	{
		if (Game.Instance.mode == Game.modes.FRUIT_NINJA)
			return;
		
		Vector3 mousePos = Input.mousePosition;
		Ray mouseRay = cam.ScreenPointToRay(mousePos);
		RaycastHit hit = new RaycastHit();

		//If Right mouse button is pressed.
		if (Input.GetMouseButtonDown(0)){
			if(Physics.Raycast(mouseRay, out hit, 100))
			{
				InteractiveObject io = hit.transform.gameObject.GetComponent<InteractiveObject> ();
				if (io!= null) {
					Events.OnCharacterHitInteractiveObject (io);
					return;
				}
				else
				if (hit.transform.gameObject.name == "floor") {
					Ray testRay = new Ray (hit.point, Vector3.up);
					Events.OnFloorClicked (hit.point);
				}
			}
		}
	}
}
