﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigurasGame : MateGame {

	public GameObject figuraGO;
	public List<GameObject> runasButtons;
	public List<Material> figurOKMaterials;
	public GameObject figurOKPS1,figurOKPS2;
	public GameObject figurWrongPS1,figurWrongPS2;

	Dictionary<string, bool> enabledButtons;

	public FigurasData.Figura figura;
	public FigurasData.Level fLevelData;

	public Tween runasTween;
	public Tween consignaTween;

	public int partidaGames;
	int gamesPlayeds;

	public AudioClip figuraOK, figuraWrong,figurasDone;
	AudioSource audioSource;

    bool haveAllRunas;

	// Use this for initialization
	void Start () {
		Data.Instance.ui.HideCapture ();
		audioSource = GetComponent<AudioSource> ();
		Data.Instance.inputManager.raycastWorld = true;	
		levelBarStep = 1f / times2FullBar;
		Events.OnMouseCollide += FigureSelect;
		Events.OnTimeOver += TimeOver;
        Events.OnUpdateRunas += UpdateRunas;

        Invoke ("Init", 5);
	}

	void Init(){
		doneSign.SetActive (false);
		loseSign.SetActive (false);
		//figura = Data.Instance.figurasData.figuras.Find (x=>x.go.name == figuraGO.name);
		SetFigura();
        UpdateRunas();
        //CheckNeededRunas();
        SetBarColor ();
		if(fLevelData.time>0)
			InitTimer ();
		//consigna.SetActive (false);
		consignaTween.SetTween(new Vector3(-9f,-1000f,0f),0.1f);
		runasTween.SetTween(new Vector3(-4f,4.6f,1f),0.05f);
		Data.Instance.ui.ShowBack (true);
		state = states.PLAYING;
	}

    void UpdateRunas() {
        enabledButtons = new Dictionary<string, bool>();
        //figura = Data.Instance.figurasData.figuras.Find (x=>x.go.name == figuraGO.name);
        foreach (GameObject go in runasButtons) {
            bool enabled = Data.Instance.figurasData.runas.Find(x => x.go.name == go.name).enabled;
            if (!enabled) {
                Renderer r = go.GetComponent<Renderer>();
                if (r == null)
                    r = go.GetComponentInChildren<Renderer>();
                r.material.color = Color.black;
                r.material.DisableKeyword("_EMISSION");
                enabledButtons.Add(go.name, false);
            } else {
                Renderer r = go.GetComponent<Renderer>();
                if (r == null)
                    r = go.GetComponentInChildren<Renderer>();
                r.material.color = new Color(0.7994196f, 0.877589f, 0.8862745f);
                r.material.EnableKeyword("_EMISSION");
                enabledButtons.Add(go.name, true);
            }
        }
    }

	void OnDestroy(){
		Events.OnMouseCollide -= FigureSelect;
		Events.OnTimeOver -= TimeOver;
        Events.OnUpdateRunas -= UpdateRunas;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckNeededRunas() {
        haveAllRunas = true;
        if (enabledButtons.ContainsValue(false)) {
            haveAllRunas = false;
            List<string> keys = new List<string>(enabledButtons.Keys);
            foreach (string key in keys) 
                enabledButtons[key] = false;

            Events.NotRuna();
        }
    }

	void FigureSelect(GameObject hit){		
		if (hit.tag == "Runa" && state == states.PLAYING) {
			if (enabledButtons [hit.name]) {
				bool done = figura.CheckRuna (hit.name);
				Transform t = figuraGO.transform.Find (hit.name);
				if (t != null) {
                    foreach (Transform tm in figuraGO.transform) {
                        if (tm.name == hit.name) {
                            Renderer r = tm.GetComponent<Renderer>();
                            if (r == null)
                                r = tm.GetComponentInChildren<Renderer>();
                            r.material = figurOKMaterials[Data.Instance.levelsData.actualDiamondLevel% figurOKMaterials.Count];
                        }
                    }
					//r.material.color = figurOKColor;
					audioSource.PlayOneShot (figuraOK);					
					figurOKPS1.SetActive (true);
					figurOKPS2.SetActive (true);
					Vector3 h1 = hit.gameObject.transform.position;
					Vector3 h2 = figuraGO.transform.position;
					figurOKPS1.transform.position = new Vector3 (h1.x, h1.y + 2, h1.z);
					//figurOKPS2.transform.position = new Vector3(h2.x,h2.y+2,h2.z);
					figurOKPS2.transform.position = t.position;
					Invoke ("StopFiguraOKPS", 1f);
				} else {
					audioSource.PlayOneShot (figuraWrong);
					TimePenalty ();
					figurWrongPS1.SetActive (true);
					figurWrongPS2.SetActive (true);
					Vector3 h1 = hit.gameObject.transform.position;
					Vector3 h2 = figuraGO.transform.position;
					figurWrongPS1.transform.position = new Vector3 (h1.x, h1.y - 5, h1.z);
					figurWrongPS2.transform.position = new Vector3 (h2.x, h2.y - 5, h2.z);
					Invoke ("StopFigurWrongPS", 1f);
				}

				if (done) {					
					state = states.ENDED;
					Data.Instance.ui.ClockSfx (false);
					Invoke ("FiguraComplete", 1.1f);
				}

			} else {
				Events.NotRuna ();
			}
		}
	}

	void StopFigurWrongPS(){
		figurWrongPS1.SetActive (false);
		figurWrongPS2.SetActive (false);
	}

	void StopFiguraOKPS(){
		figurOKPS1.SetActive (false);
		figurOKPS2.SetActive (false);
	}

	void FiguraComplete(){
		audioSource.PlayOneShot (figurasDone);
		Data.Instance.ui.HideTimer ();
		//Events.FiguraComplete (figura.go.name);
		Events.OnMathGameComplete ();
		//Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		Data.Instance.levelsData.AddLevelPercent (levelBarStep);
		//colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		Data.Instance.ui.colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		foreach (GameObject go in runasButtons)
			Destroy (go);
		runasButtons.Clear ();
		Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			Data.Instance.figurasData.ResetFiguresDone ();
			Invoke ("BackToWorld", 3);
		} else {
			doneSign.SetActive (true);
			Data.Instance.figurasData.AddCurrentLevel ();
			gamesPlayeds++;
			if (gamesPlayeds >= partidaGames) {
				AchievementsEvents.NewPointToAchievement (Achievement.types.FIGURAS);
				Invoke ("BackToWorld", 3);
			}else
				Invoke ("Init", 3);
		}


        Events.SendData();
    }

	void SetFigura(){
		/*for (int i = 0; i < Data.Instance.figurasData.figuras.Count; i++) {			
			if (!Data.Instance.figurasData.figuras [i].done) {
				figura = Data.Instance.figurasData.figuras [i];
				figuraGO = Instantiate (figura.go);
				figuraGO.transform.SetParent(gameObject.transform.Find("Figura"));
				figuraGO.transform.localPosition = Vector3.zero;
				figuraGO.transform.localRotation = Quaternion.identity;
				i = Data.Instance.figurasData.figuras.Count;
			}
		}*/

		fLevelData = Data.Instance.figurasData.GetLevel ();
		figura = fLevelData.figura;
        Vector3 pos = figura.go.transform.localPosition;
        Quaternion rot = figura.go.transform.localRotation;
        figuraGO = Instantiate (figura.go);
		figuraGO.transform.SetParent(gameObject.transform.Find("Figura"));
		figuraGO.transform.localPosition = pos;
		figuraGO.transform.localRotation = rot;
		//figuraGO.transform.parent.transform.localScale = new Vector3 (1.5f,1.5f,1.5f);

		SetButtons ();
	}

	void SetButtons(){
		/*for (int i = 0; i < 5; i++) {
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
		}*/

		for (int i = 0; i < figura.runas.Count; i++)
			runasButtons.Add (figura.runas [i].go);

		for (int i = 0; i < fLevelData.opciones.Count; i++){
			FigurasData.Runa r = Data.Instance.figurasData.GetRuna (fLevelData.opciones [i].name);
			runasButtons.Add (r.go);
		}

		Utils.Shuffle (runasButtons);

		for(int i=0;i<runasButtons.Count;i++){
			string name = runasButtons [i].name;
			runasButtons[i] = Instantiate (runasButtons[i]);
			runasButtons [i].name = name;
			runasButtons[i].transform.SetParent(gameObject.transform.Find("Runas"));
			runasButtons[i].transform.localPosition = new Vector3(2*i,0,0);
			runasButtons[i].transform.localRotation = Quaternion.identity;
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
		Data.Instance.ui.HideTimer ();
		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){		
		Data.Instance.LoadScene ("World");
		Events.OnMinigameDone ();
	}

	public void CaptureScreen(){
		Data.Instance.CaptureScreen ();
	}
}
