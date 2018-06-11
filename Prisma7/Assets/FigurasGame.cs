using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurasGame : MonoBehaviour {

	public GameObject figura;
	public List<GameObject> runasButtons;

	// Use this for initialization
	void Start () {
		Events.OnMouseCollide += FigureSelect;
		foreach (GameObject go in runasButtons) {
			bool enabled = Data.Instance.figurasManager.runas.Find (x=>x.name ==go.name).enabled;
			if (!enabled)
				go.GetComponent<Renderer> ().material.color = new Color (0.5F, 0.5F, 0.5F, 0.5f);
		}
	}

	void OnDestroy(){
		Events.OnMouseCollide -= FigureSelect;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FigureSelect(GameObject hit){		
		if (hit.tag == "Runa") {
			FigurasManager.Runa runa = Data.Instance.figurasManager.runas.Find (x=>x.name ==hit.name);
			if (runa.enabled) {
				Transform t = figura.transform.Find (runa.name);
				if (t != null) {
					t.GetComponent<Renderer> ().material.color = Color.red;
				}
			}
		}
	}
}
