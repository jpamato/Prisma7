using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillaGame : MateGame {

	public GameObject gemaGrid;
	public RectTransform gridContent;

	public int partidaGames;
	int gamesPlayeds;

	public GrillaData.Level gLevelData;

	public AudioClip combiOK,combiDone;
	AudioSource audioSource;

	public bool[][] grid;

	// Use this for initialization
	void Start () {	
		audioSource = GetComponent<AudioSource> ();
		Data.Instance.inputManager.raycastUI = true;
		levelBarStep = 1f / times2FullBar;
		Events.OnGridClick += OnGrillaClick;
		//Events.OnTimeOver += TimeOver;

		//Invoke ("Init", 0.1f);
		Init();
	}

	void Init(){
		doneSign.SetActive (false);
		loseSign.SetActive (false);

		SetGrillaLevel ();


		SetBarColor ();
		InitTimer ();

		state = states.PLAYING;
	}

	void OnDestroy(){
		Events.OnTimeOver -= TimeOver;
		Events.OnGridClick -= OnGrillaClick;
	}

	// Update is called once per frame
	void Update () {

	}

	void SetGrillaLevel(){		
		int cLevel = Data.Instance.grillaData.currentLevel;
		gLevelData = Data.Instance.grillaData.grillaLevels[cLevel];

		consigna.SetActive (true);
		ConsignaCombinatoria cs = consigna.GetComponent<ConsignaCombinatoria> ();
		cs.texto.text = gLevelData.consigna;
		//cs.valor.text = ""+cLevelData.resultado;

		RectTransform rt = gemaGrid.GetComponent<RectTransform> ();
		gridContent.sizeDelta = new Vector2 (gLevelData.size.x * rt.sizeDelta.x, gLevelData.size.y * rt.sizeDelta.y);
		for (int i = 0; i < gLevelData.size.x * gLevelData.size.y; i++) {
			GameObject go = Instantiate (gemaGrid);
			go.transform.SetParent (gridContent);
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			go.transform.SetAsLastSibling();

			GemaGrid gg = go.GetComponent<GemaGrid> ();
			gg.id = new Vector2 (i % gLevelData.size.x, Mathf.Floor (i / gLevelData.size.y));
		}

	}

	void StopFigurWrongPS(){
		
	}

	void StopFiguraOKPS(){
		
	}

	void OnGrillaClick(Vector2 id, bool active){
		grid [(int)id.x][(int)id.y] = active;

		/*for (int x = 0; x < grid.Length; x++) {

		}*/
	}

	void GrillaComplete(){
		audioSource.PlayOneShot (combiDone);
		Data.Instance.ui.HideTimer ();
		//Events.FiguraComplete (figura.go.name);
		Events.OnMathGameComplete ();
		Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		//colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		Data.Instance.ui.colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);

		//Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			//Data.Instance.figurasData.ResetFiguresDone ();
			Invoke ("BackToWorld", 3);
		} else {
			doneSign.SetActive (true);
			gamesPlayeds++;
			if (gamesPlayeds >= partidaGames) 
				Invoke ("BackToWorld", 3);
			else
				Invoke ("Init", 3);
		}		
	}

	void TimeOver(){
		state = states.ENDED;
		loseSign.SetActive (true);

		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
