using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigurasGame : MateGame {

	public GameObject figuraGO;
	public List<GameObject> runasButtons;
	public Color figurOKColor;
	public GameObject figurOKPS;

	Dictionary<string, bool> enabledButtons;

	public FigurasData.Figura figura;

	public Tween runasTween;
	public Tween consignaTween;

	public int partidaGames;
	int gamesPlayeds;

	// Use this for initialization
	void Start () {		
		levelBarStep = 1f / times2FullBar;
		Events.OnMouseCollide += FigureSelect;
		Events.OnTimeOver = TimeOver;
		Invoke ("Init", 5);
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
		//consigna.SetActive (false);
		consignaTween.SetTween(new Vector3(-9f,-1000f,0f),0.1f);
		runasTween.SetTween(new Vector3(-4f,3f,1f),0.05f);
		state = states.PLAYING;
	}

	void OnDestroy(){
		Events.OnMouseCollide -= FigureSelect;
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
					r.material.color = figurOKColor;
					figurOKPS.SetActive (true);
					Invoke ("StopFiguraOKPS", 1f);
				} else {
					TimePenalty ();
				}

				if (done)
					Invoke ("FiguraComplete", 1.1f);

			}
		}
	}

	void StopFiguraOKPS(){
		figurOKPS.SetActive (false);
	}

	void FiguraComplete(){
		Events.FiguraComplete (figura.go.name);
		state = states.ENDED;
		Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f)
			colorDoneSign.SetActive (true);
		else
			doneSign.SetActive (true);
		foreach (GameObject go in runasButtons)
			Destroy (go);
		runasButtons.Clear ();
		Destroy (figuraGO);
		gamesPlayeds++;
		if (gamesPlayeds >= partidaGames) 
			Invoke ("BackToWorld", 3);
		else
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
		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
