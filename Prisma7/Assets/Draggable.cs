using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour{

	public bool dropable;
	public Vector2 _startPosition;
	private Transform _startParent;

	bool dragging;
	Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {		
		if (dragging) {
			if (Input.GetMouseButtonUp (0))
				OnEndDrag ();
			else				
				OnDrag ();
		}

	}

	public void OnBeginDrag(){
		dragging = true;
		transform.SetAsLastSibling();
		_startPosition = transform.position;
		_startParent = transform.parent;
		image.raycastTarget = false;
	}

	void OnDrag(){
		transform.position = Input.mousePosition;
	}

	void OnEndDrag(){
		dragging = false;
		image.raycastTarget = true;
		transform.position = _startPosition;
	}

	public void OnDropingOut(){
		if (dropable) {	
			Events.OnDropingOut();
			Destroy (gameObject);
		}
	}
}