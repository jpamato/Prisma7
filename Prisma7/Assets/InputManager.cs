using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	public bool raycastWorld, raycastUI, dragging;

	public List<RaycastResult> UIhits;
	Draggable draggable;

	// Use this for initialization
	void Start () {
		UIhits = new List<RaycastResult> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.Instance != null && Game.Instance.mode == Game.modes.FRUIT_NINJA || Data.Instance.captureScreen.isVisible)
			return;
		if (Input.GetMouseButtonDown (0)) {
			if (raycastWorld) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit)) {				
					Events.OnMouseCollide (hit.transform.gameObject);
					//Debug.Log (hit.transform.name);
				}
			}

			if (raycastUI) {
				GetUIUnderMouse ();
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if(dragging){
				GetUIUnderMouse ();
				dragging = false;
			}
		}
	}
	
	private void GetUIUnderMouse(){
		var pointer = new PointerEventData (EventSystem.current);
		pointer.position = Input.mousePosition;

		EventSystem.current.RaycastAll (pointer, UIhits);

		if (UIhits.Count < 1)
			return;

		GameObject clickedGO = null;
		foreach (RaycastResult r in UIhits) {
			if (!dragging) {
				if (r.gameObject.tag == "draggable_ui") {
					clickedGO = r.gameObject;
					//print (r.gameObject.name);
					break;
				}
			} else {
				if (r.gameObject.tag == "dropable_ui") {
					clickedGO = r.gameObject;
					//print (r.gameObject.name);
					break;
				}
			}
		}

		if (clickedGO != null) {
			if (!dragging) {
				draggable = clickedGO.GetComponent<Draggable> ();
				if (draggable != null) {
					dragging = true;
					draggable.OnBeginDrag ();
				} else
					print (clickedGO.name+": falta clase Draggable en gameobject");
			} else {
				Dropable dropable = clickedGO.GetComponent<Dropable> ();
				if (dropable != null) {				
					dropable.OnDrop (draggable.gameObject);
					draggable = null;
				} else
					print ("falta clase Dropable en gameobject");
			}
		} else {
			if (dragging)
				draggable.OnDropingOut ();
		}
		
	}
}
