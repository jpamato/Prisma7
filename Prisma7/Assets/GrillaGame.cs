using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GrillaGame : MateGame {

	public GameObject gemaGrid;
	public RectTransform gridContent;

	public Tween consignaTween;

	public int partidaGames;
	int gamesPlayeds;

	public GrillaData.Level gLevelData;

	public AudioClip combiOK,combiDone;
	AudioSource audioSource;

	public int[,] grid;
	public int gemasActivas;
	public int columnas;
	public int filas;

	public List<Rect> rects;

	public InputField respuesta;

    public Animator brujo;    

	[Serializable]
	public class Rect{
		public Vector2 topLeft;
		public Vector2 bottomRight;
	}

	// Use this for initialization
	void Start () {	
		Data.Instance.ui.HideCapture ();
		audioSource = GetComponent<AudioSource> ();
		Data.Instance.inputManager.raycastUI = true;
		levelBarStep = 1f / times2FullBar;
		Events.OnGridClick += OnGrillaClick;
        Events.OnGridOver += OnGrillaOver;
        Events.OnTimeOver += TimeOver;

		//Invoke ("Init", 0.1f);
		Init();
	}

	void Init(){
		Data.Instance.tipsManager.SawMago ();
		doneSign.SetActive (false);
		loseSign.SetActive (false);

		SetGrillaLevel ();


		SetBarColor ();
		InitTimer ();

        //consignaTween.SetTween(new Vector3(-9f,-1000f,0f),0.1f);

        Data.Instance.ui.ShowBack(true);

        brujo.Play ("idle");
		state = states.PLAYING;
	}

	void OnDestroy(){
		Events.OnTimeOver -= TimeOver;
		Events.OnGridClick -= OnGrillaClick;
        Events.OnGridOver -= OnGrillaOver;
    }

	// Update is called once per frame
	void Update () {

	}

	void SetGrillaLevel(){		
		gLevelData = Data.Instance.grillaData.GetLevel ();

		consigna.SetActive (true);
		ConsignaCombinatoria cs = consigna.GetComponent<ConsignaCombinatoria> ();
		cs.texto.text = gLevelData.consigna;
		//cs.valor.text = ""+cLevelData.resultado;

		foreach (Transform child in gridContent)
			Destroy (child.gameObject);

		RectTransform rt = gemaGrid.GetComponent<RectTransform> ();
		gridContent.sizeDelta = new Vector2 (gLevelData.size[0] * rt.sizeDelta.x, gLevelData.size[1] * rt.sizeDelta.y);

		respuesta.gameObject.SetActive (false);

		int fromX,fromY,toX,toY;
		fromX = fromY = toX = toY = -1;
		if ((int)gLevelData.levelType > 1) {
			fromX = UnityEngine.Random.Range (0, gLevelData.size[0] - gLevelData.filled[0]);
			fromY = UnityEngine.Random.Range (0, gLevelData.size[1] - gLevelData.filled[1]);

			toX = fromX + gLevelData.filled[0];
			toY = fromY + gLevelData.filled[1];

			if ((int)gLevelData.levelType == 2) 
				respuesta.gameObject.SetActive (true);
		}

		grid = new int[gLevelData.size[0],gLevelData.size[1]];
		
		for (int i = 0; i < gLevelData.size[0] * gLevelData.size[1]; i++) {
			GameObject go = Instantiate (gemaGrid);
			go.transform.SetParent (gridContent);
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			go.transform.SetAsLastSibling();

			GemaGrid gg = go.GetComponent<GemaGrid> ();
			gg.id = new Vector2 (i % gLevelData.size[0], Mathf.Floor (i / gLevelData.size[1]));

			if ((int)gLevelData.levelType == 2) {
				gg.SetInteractable (false);
				if (gg.id.x >= fromX && gg.id.x < toX && gg.id.y >= fromY && gg.id.y < toY)
					gg.SetActive (true);
			} else if ((int)gLevelData.levelType == 3) {
				if (gg.id.x >= fromX && gg.id.x < toX && gg.id.y >= fromY && gg.id.y < toY) {
					grid [(int)gg.id.x,(int)gg.id.y] = 1;
					gg.SetInteractable (false);
					gg.SetActive (true);
				}
			}
		}

		totalTime = gLevelData.time*3;
	}

	void StopFigurWrongPS(){
		
	}

	void StopFiguraOKPS(){
		
	}

    void OnGrillaClick(Vector2 id, bool active) {
        if(active)
            Data.Instance.grillaData.overActiveState = GrillaData.ActiveState.active;
        else
            Data.Instance.grillaData.overActiveState = GrillaData.ActiveState.inactive;
        grid[(int)id.x, (int)id.y] = active ? 1 : 0;
        CheckGrid();
    }

    void OnGrillaOver(Vector2 id) {
        bool active = false;
        if (Data.Instance.grillaData.overActiveState == GrillaData.ActiveState.active)
            active = true;
        grid[(int)id.x, (int)id.y] = active ? 1 : 0;
        CheckGrid();
    }

    void CheckGrid(){
		rects.Clear ();
		for (int x = 0; x < grid.GetLength(0); x++) {
			for (int y = 0; y < grid.GetLength(1); y++) {
				if (grid [x, y] == 1) {
					FindRectEnd (x, y);
				}
			}
		}


		for (int x = 0; x < grid.GetLength(0); x++) {
			for (int y = 0; y < grid.GetLength(1); y++) {
				if (grid [x, y] > 0)
					grid [x, y] = 1;
			}
		}

		foreach (Rect r in rects) {
			int width = (int)((r.bottomRight.x+1) - r.topLeft.x);
			int height = (int)((r.bottomRight.y+1) - r.topLeft.y);
			int area = width * height;

			if (area == gLevelData.area) {
				if (gLevelData.columnas < 1 || width == gLevelData.columnas) {
					if (gLevelData.filas < 1 || height == gLevelData.filas) {
						GrillaComplete ();
					}
				}
			}

		}
	}

	void FindRectEnd(int x, int y){
		Rect r = new Rect ();
		r.topLeft = new Vector2 (x, y);
		bool flagEndx = false;
		bool flagEndy = false;
		int ii = x;
		int jj = y;
		//Debug.Log (grid.GetLength (0) + " - " + grid.GetLength (1));
		for (int i = x; i < grid.GetLength(0); i++) {
			//Debug.Log (i + ":" + y+"="+grid [i, y]);
			if (grid [i, y] == 0) {
				flagEndx = true;
				ii=i;
				break;
			} else {
				//grid [i, y] = 1;
				ii=i;
			}
			if (!flagEndy) {
				for (int j = y; j < grid.GetLength (1); j++) {
					//Debug.Log (i + ":" + j + "=" + grid [i, j]);
					if (grid [i, j] == 0) {
						flagEndy = true;
						jj = j;
						break;
					} else {
						//grid [i, j] = 1;
						jj = j;
					}	
				}
			} else {
				for (int j = y; j < jj; j++) {
					if (grid [i, j] == 0) {
						jj = j;
						break;
					}
				}
			}
		}
		float right, bottom;
		if (flagEndx)
			right = ii-1;
		else
			right = ii;
			
		if (flagEndy)
			bottom = jj-1;
		else
			bottom = jj;


		for (int i = x; i < right+1; i++) {
			for (int j = y; j < bottom+1; j++) {
				grid [i, j] = 5;
			}
		}

		r.bottomRight = new Vector2 (right, bottom);
		rects.Add (r);
	}

	void GrillaComplete(){
		brujo.Play ("angry");
		audioSource.PlayOneShot (combiDone);
		Data.Instance.ui.HideTimer ();
		//Events.FiguraComplete (figura.go.name);
		Events.OnMathGameComplete ();
		//Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		Data.Instance.levelsData.AddLevelPercent (levelBarStep);
		//colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		Data.Instance.ui.colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);

		//Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			Invoke ("BackToWorld", 3);
		} else {
			doneSign.SetActive (true);
			Data.Instance.grillaData.AddCurrentLevel ();
			gamesPlayeds++;
			if (gamesPlayeds >= partidaGames) {
				AchievementsEvents.NewPointToAchievement (Achievement.types.GRILLA);
				Invoke ("BackToWorld", 3);
			}else {
				brujo.Play ("attack");
				Invoke ("Init", 3);
			}
		}

        Events.SendData();
    }

	void TimeOver(){
		brujo.Play ("celebrate");
		state = states.ENDED;
		loseSign.SetActive (true);
		Data.Instance.ui.ClockSfx (false);
		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		gridContent.gameObject.SetActive (false);
		consigna.gameObject.SetActive (false);
		Data.Instance.LoadScene ("World");
		Events.OnMinigameDone ();
	}

	public void Respuesta(){
		if (int.Parse (respuesta.text) == gLevelData.area) {
			GrillaComplete ();
		} else {
			respuesta.image.color = Color.red;
			Invoke ("ResetRespuesta", 2);
		}
	}

	void ResetRespuesta(){
		respuesta.image.color = Color.white;
		respuesta.text = "";
	}
}
