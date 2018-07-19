using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour {

	public Camera cam;
	bool isOn;
	public GameObject panel;
	public GameObject trailObject;
	public ParticlesManager particles;

	public void Start()
	{
		isOn = false;
		panel.SetActive (false);
		Events.OnDragger += OnDragger;
		Events.OnFruitNinjaClickedBubble += OnFruitNinjaClickedBubble;
	}
	void OnDestroy()
	{
		Events.OnDragger -= OnDragger;
		Events.OnFruitNinjaClickedBubble -= OnFruitNinjaClickedBubble;
	}
	bool addParticle;
	Fruit.types fruitType;
	void OnFruitNinjaClickedBubble(Fruit.types type)
	{
		addParticle = true;
		this.fruitType = type;
	}
	void Update()
	{
		if (!isOn)
			return;

		if (Input.GetMouseButton (0)) {
			panel.SetActive (true);
			Vector3 mousePos = Input.mousePosition;
			Ray mouseRay = cam.ScreenPointToRay (mousePos);

			RaycastHit[] hits = Physics.RaycastAll (mouseRay);

			for (int i = 0; i < hits.Length; i++) {
				RaycastHit hit = hits [i];
				if (hit.transform.gameObject.name == "draggerPlane") {
					trailObject.transform.position = hit.point;
					if (addParticle) {
						particles.SetOn (fruitType, hit.point);
						addParticle = false;
					}
				}
			}
		} else if (Input.GetMouseButtonUp (0)) {
			panel.SetActive (false);
		}
	}
	void OnDragger(bool _isOn)
	{
		isOn = _isOn;
		panel.SetActive (_isOn);
	}
}
