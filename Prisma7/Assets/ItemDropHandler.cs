using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour,IDropHandler {

	int dropN;
	
	public void OnDrop(PointerEventData eventData){
		//Debug.Log ("drop");
		//ItemDragHandler.ItemBeingDragged.transform.SetParent (transform);

		GameObject go = Instantiate(ItemDragHandler.ItemBeingDragged);
		go.transform.position = ItemDragHandler.ItemBeingDragged.transform.position;
		go.transform.SetParent (transform);
		go.transform.localScale = Vector3.one;
		go.name = go.name + dropN;
		dropN++;
	}
}
