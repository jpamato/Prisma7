using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinatoriasGame : MateGame {

	public List<GameObject> gemasButtons;
	public List<Material> gemasOKMaterials;
	public GameObject gemasOKPS1,gemasOKPS2;
	public GameObject gemasWrongPS1,gemasWrongPS2;

	public int partidaGames;
	int gamesPlayeds;

	public GameObject gemaItem;
	public GameObject gemaSlot;
	public Transform inventarioContent;
	public Transform centralContent;
	public List<Burbuja> burbujas;
	public int burbujasDone;
	public int sumaCentral;

	public CombinatoriasData.Level cLevelData;

	public List<GameObject> gemasCentral;

	public AudioClip combiOK,combiDone;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {	
		audioSource = GetComponent<AudioSource> ();
		Data.Instance.inputManager.raycastUI = true;
		levelBarStep = 1f / times2FullBar;
		Events.OnTimeOver += TimeOver;
		Events.DroppedUI += DroppedUI;
		Events.OnDropingOut += OnDropingOut;
		//Invoke ("Init", 0.1f);
		Init();
	}

	void Init(){
		doneSign.SetActive (false);
		loseSign.SetActive (false);

		gemasCentral = new List<GameObject> ();

		SetCombiLevel ();


		SetBarColor ();
		InitTimer ();

		state = states.PLAYING;
	}

	void OnDestroy(){
		Events.OnTimeOver -= TimeOver;
		Events.DroppedUI -= DroppedUI;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetCombiLevel(){				
		cLevelData = Data.Instance.combinatoriasData.GetLevel ();
		int[] vals = cLevelData.valores.Clone() as int[];
		System.Array.Sort (vals);
		List<CombinatoriasData.Gema> gemas = new List<CombinatoriasData.Gema> ();
		foreach(CombinatoriasData.Gema g in Data.Instance.combinatoriasData.gemas)
			gemas.Add(g);

		Utils.Shuffle (gemas);

		for (int i = 0; i < vals.Length; i++) {			
			GameObject slot = Instantiate (gemaSlot);
			slot.transform.SetParent (inventarioContent);
			slot.transform.localPosition = Vector3.zero;
			slot.transform.localScale = Vector3.one;
			slot.transform.SetAsLastSibling();
			CombinatoriasData.Gema g = gemas.Find (x => x.size == i);
			GameObject gi = Instantiate (gemaItem);
			gi.transform.SetParent (slot.transform);
			gi.transform.localPosition = Vector3.zero;
			gi.transform.localScale = Vector3.one;
			//gi.transform.SetAsLastSibling();*/
			GemaItem gis = gi.GetComponent<GemaItem> ();
			gis.image.sprite = g.sprite;
			gis.text.text = ""+vals [i];
			gis.val = vals [i];

			for (int j = 0; j < cLevelData.incluidos.Length; j++) {
				if(vals[i]==cLevelData.incluidos[j]){
					GameObject go = Instantiate (gemaItem);
					go.transform.SetParent (centralContent);
					go.transform.localPosition = Vector3.zero;
					go.transform.localScale = Vector3.one;
					//go.name = go.name + dropN;
					//dropN++;
					GemaItem git = go.GetComponent<GemaItem> ();
					git.image.sprite = g.sprite;
					git.val = vals [i];
					git.GetComponent<Image> ().raycastTarget = false;
					git.text.enabled = false;
					go.GetComponent<Draggable> ().dropable = false;
					sumaCentral += vals [i];
				}
			}
		}
		consigna.SetActive (true);
		ConsignaCombinatoria cs = consigna.GetComponent<ConsignaCombinatoria> ();
		cs.texto.text = "Encierra dentro de los anillos de poder ";
		if (cLevelData.combinaciones > 1)
			cs.texto.text += cLevelData.combinaciones + " combinaciones que sumen";
		else
			cs.texto.text += cLevelData.combinaciones + " combinación que sume";
		cs.valor.text = ""+cLevelData.resultado;

	}

	void OnDropingOut(){
		Invoke ("ResetSuma",0.1f);
	}

	void ResetSuma(){
		sumaCentral = 0;
		for (int i = 0; i < centralContent.childCount; i++) {
			sumaCentral += centralContent.GetChild (i).GetComponent<GemaItem> ().val;
		}
		if (sumaCentral == cLevelData.resultado)
			RingDone ();
	}

	void DroppedUI(GameObject dragged){
		GemaItem gi = dragged.GetComponentInParent<GemaItem> ();
		sumaCentral += gi.val;
		if (sumaCentral == cLevelData.resultado)
			RingDone ();
	}

	void StopFigurWrongPS(){
		gemasWrongPS1.SetActive (false);
		gemasWrongPS2.SetActive (false);
	}

	void StopFiguraOKPS(){
		gemasOKPS1.SetActive (false);
		gemasOKPS2.SetActive (false);
	}

	void RingDone(){
		if (burbujasDone > 0) {
			int[] vals = new int[centralContent.childCount];
			for (int i = 0; i < centralContent.childCount; i++) {
				vals [i] = centralContent.GetChild (i).GetComponent<GemaItem> ().val;
			}
			bool repetido;
			for (int i = 0; i < burbujasDone; i++) {
				repetido = true;
				if (burbujas [i].combinacion.Count == vals.Length) {
					System.Array.Sort (vals);
					List<int> l = burbujas [i].combinacion;
					for (int j = 0; j < vals.Length; j++) {
						if (vals [j] != l [j])
							repetido = false;
					}
				} else {
					repetido = false;
				}

				if (repetido) {
					for (int k = centralContent.childCount-1; k >-1 ; k--) 
						Destroy(centralContent.GetChild (k).gameObject);
					burbujas [i].done.SetActive (false);
					burbujas [i].done.SetActive (true);
					sumaCentral = 0;
					return;
				}
			}
		}

		for (int i = centralContent.childCount-1; i >-1 ; i--) {
			Transform t = centralContent.GetChild (i);
			t.SetParent(burbujas[burbujasDone].content.transform);
			t.localPosition = Vector3.zero;
			t.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			GemaItem gi = t.gameObject.GetComponent<GemaItem> ();
			gi.image.color = Data.Instance.levelsData.GetInProgressLevel ().color;
			burbujas [burbujasDone].combinacion.Add (t.GetComponent<GemaItem> ().val);
			burbujas [burbujasDone].combinacion.Sort ();
			burbujas [burbujasDone].WinEffect.SetActive (true);
		}



		burbujas [burbujasDone].done.SetActive (true);
		burbujasDone++;
		sumaCentral = 0;
		audioSource.PlayOneShot (combiOK);
		if (burbujasDone >= cLevelData.combinaciones)
			Invoke ("FiguraComplete", 1.0f);
	}

	void FiguraComplete(){
		audioSource.PlayOneShot (combiDone);
		Data.Instance.ui.HideTimer ();
		//Events.FiguraComplete (figura.go.name);
		Events.OnMathGameComplete ();
		//Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		Data.Instance.levelsData.AddLevelPercent (levelBarStep);
		//colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		Data.Instance.ui.colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		foreach (GameObject go in gemasButtons)
			Destroy (go);
		gemasButtons.Clear ();
		//Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			//Data.Instance.figurasData.ResetFiguresDone ();
			Invoke ("BackToWorld", 3);
		} else {
			doneSign.SetActive (true);
			Data.Instance.combinatoriasData.AddCurrentLevel ();
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
		foreach (GameObject go in gemasButtons)
			Destroy (go);
		gemasButtons.Clear ();

		/*Destroy (figuraGO);
		figura.ClearRunas ();*/
		Data.Instance.ui.ClockSfx (false);
		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
