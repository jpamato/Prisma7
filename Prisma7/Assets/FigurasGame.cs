using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigurasGame : MateGame {

	public GameObject figuraGO;
	public List<GameObject> runasButtons;

	Dictionary<string, bool> enabledButtons;

	public FigurasData.Figura figura;

	// Use this for initialization
	void Start () {
		Events.OnMouseCollide += FigureSelect;
		Events.FiguraComplete += FiguraComplete;
		enabledButtons = new Dictionary<string,bool> ();
		figura = Data.Instance.figurasData.figuras.Find (x=>x.go.name == figuraGO.name);
		foreach (GameObject go in runasButtons) {
			bool enabled = Data.Instance.figurasData.runas.Find (x=>x.go.name ==go.name).enabled;
			if (!enabled) {
				go.GetComponent<Renderer> ().material.color = new Color (0.5F, 0.5F, 0.5F, 0.5f);
				enabledButtons.Add (go.name, false);
			} else {
				enabledButtons.Add (go.name, true);
			}
		}
		SetBarColor ();
		InitTimer ();
	}

	void OnDestroy(){
		Events.OnMouseCollide -= FigureSelect;
		Events.FiguraComplete += FiguraComplete;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FigureSelect(GameObject hit){		
		if (hit.tag == "Runa") {
			if (enabledButtons[hit.name]) {
				bool done = figura.CheckRuna (hit.name);
				Transform t = figuraGO.transform.Find (hit.name);
				if (t != null) {
					t.GetComponent<Renderer> ().material.color = Color.red;
				}

				if (done)
					Events.FiguraComplete (figura.go.name);
			}
		}
	}

	void FiguraComplete(string name){
		state = states.ENDED;
		colorBar.value += 0.1f;
		doneSign.SetActive (true);
	}

}
