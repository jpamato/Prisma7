using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropable : MonoBehaviour {

	int dropN;
	GameObject lastChild;
	
	public void OnDrop(GameObject dragged){
		if (dragged.transform.parent != transform) {
			GameObject go = Instantiate (dragged);
			go.transform.SetParent (transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			go.name = go.name + dropN;
			dropN++;
			GemaItem gi = go.GetComponent<GemaItem> ();
			gi.image.raycastTarget = true;
			gi.text.enabled = false;
			go.GetComponent<Draggable> ().dropable = true;
			Events.DroppedUI (dragged);
		} else {
			dragged.transform.SetAsFirstSibling ();
		}
	}

}
