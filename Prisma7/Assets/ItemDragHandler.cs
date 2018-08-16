using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	public static GameObject ItemBeingDragged;
	private Vector2 _startPosition;
	private Transform _startParent;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnBeginDrag(PointerEventData eventData){
		ItemBeingDragged = gameObject;
		_startPosition = transform.position;
		_startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = Input.mousePosition;
		//Debug.Log ("aca");
	}

	public void OnEndDrag(PointerEventData eventData){
		ItemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		transform.position = _startPosition;
	}
}