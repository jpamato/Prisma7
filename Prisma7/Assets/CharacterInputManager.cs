using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour {

	public Camera cam;

	void Update()
	{
		if (Game.Instance.mode == Game.modes.FRUIT_NINJA)
			return;
		
		if (Input.GetMouseButtonDown(0)){
			
			Vector3 mousePos = Input.mousePosition;
			Ray mouseRay = cam.ScreenPointToRay(mousePos);

			RaycastHit[] hits = Physics.RaycastAll(mouseRay);

			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit hit = hits[i];
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
