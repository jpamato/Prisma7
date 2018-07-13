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
		colorBar.value = Data.Instance.levelsData.actualLevelPercent * colorBar.maxValue;
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
				Renderer r = go.GetComponent<Renderer> ();
				if(r==null)
					r = go.GetComponentInChildren<Renderer> ();
				r.material.color = new Color (0.25F, 0.25F, 0.25F, 0.25f);
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
		Events.FiguraComplete -= FiguraComplete;
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
					Renderer r = t.GetComponent<Renderer> ();
					if(r==null)
						r = t.GetComponentInChildren<Renderer> ();
					r.material.color = Color.red;
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
		colorBar.value += 1;
		Data.Instance.levelsData.actualLevelPercent = 1f*colorBar.value / colorBar.maxValue;
		if (colorBar.value == colorBar.maxValue)
			colorDoneSign.SetActive (true);
		else
			doneSign.SetActive (true);
		foreach (GameObject go in runasButtons)
			Destroy (go);
		runasButtons.Clear ();
		Destroy (figuraGO);
		//Invoke ("Init", 3);
		Invoke ("BackToWorld", 3);
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
		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
