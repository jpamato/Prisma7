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
		Events.OnTimeOver = TimeOver;
		Init ();
	}

	void Init(){
		doneSign.SetActive (false);
		loseSign.SetActive (false);
		enabledButtons = new Dictionary<string,bool> ();
		//figura = Data.Instance.figurasData.figuras.Find (x=>x.go.name == figuraGO.name);
		SetFigura();
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
		state = states.PLAYING;
	}

	void OnDestroy(){
		Events.OnMouseCollide -= FigureSelect;
		Events.FiguraComplete += FiguraComplete;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FigureSelect(GameObject hit){		
		if (hit.tag == "Runa" && state == states.PLAYING) {
			if (enabledButtons[hit.name]) {
				bool done = figura.CheckRuna (hit.name);
				Transform t = figuraGO.transform.Find (hit.name);
				if (t != null) {
					t.GetComponent<Renderer> ().material.color = Color.red;
				} else {
					TimePenalty ();
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
		foreach (GameObject go in runasButtons)
			Destroy (go);
		runasButtons.Clear ();
		Destroy (figuraGO);
		Invoke ("Init", 3);
	}

	void SetFigura(){
		for (int i = 0; i < Data.Instance.figurasData.figuras.Count; i++) {			
			if (!Data.Instance.figurasData.figuras [i].done) {
				figura = Data.Instance.figurasData.figuras [i];
				figuraGO = Instantiate (figura.go);
				figuraGO.transform.SetParent(gameObject.transform.Find("Figura"));
				figuraGO.transform.localPosition = Vector3.zero;
				figuraGO.transform.localRotation = Quaternion.identity;
				i = Data.Instance.figurasData.figuras.Count;
			}
		}
		SetButtons ();
	}

	void SetButtons(){
		for (int i = 0; i < 5; i++) {
			if (i < figura.runas.Count) {
				runasButtons.Add (figura.runas [i].go);
			} else {
				FigurasData.Runa r = Data.Instance.figurasData.GetRandomRuna ();
				if (runasButtons.Contains (r.go)) {
					i--;
				} else {
					runasButtons.Add (r.go);
				}

			}				
		}
		Utils.Shuffle (runasButtons);

		for(int i=0;i<runasButtons.Count;i++){
			string name = runasButtons [i].name;
			runasButtons[i] = Instantiate (runasButtons[i]);
			runasButtons [i].name = name;
			runasButtons[i].transform.SetParent(gameObject.transform.Find("Runas"));
			runasButtons[i].transform.localPosition = new Vector3(2*i,0,0);
		}
	}

	void TimeOver(){
		state = states.ENDED;
		loseSign.SetActive (true);
		foreach (GameObject go in runasButtons)
			Destroy (go);
		runasButtons.Clear ();
		Destroy (figuraGO);
		figura.ClearRunas ();
		Invoke ("Init", 3);
	}
}
