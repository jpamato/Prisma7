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
	public int sumaCentral;

	CombinatoriasData.Level cLevelData;

	// Use this for initialization
	void Start () {		
		levelBarStep = 1f / times2FullBar;
		Events.OnTimeOver += TimeOver;
		Events.DroppedUI += DroppedUI;
		Invoke ("Init", 5);
	}

	void Init(){
		doneSign.SetActive (false);
		loseSign.SetActive (false);

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
		int cLevel = Data.Instance.combinatoriasData.currentLevel;
		cLevelData = Data.Instance.combinatoriasData.combinatoriasLevels[cLevel];
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
		}

		ConsignaCombinatoria cs = consigna.GetComponent<ConsignaCombinatoria> ();
		cs.texto.text = "Encierra dentro de los anillos de poder "+cLevelData.combinaciones+"combinaciones que sumen";
		cs.valor.text = ""+cLevelData.resultado;
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
		sumaCentral = 0;
	}

	void FiguraComplete(){
		//Events.FiguraComplete (figura.go.name);
		Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		foreach (GameObject go in gemasButtons)
			Destroy (go);
		gemasButtons.Clear ();
		//Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			Data.Instance.figurasData.ResetFiguresDone ();
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
		foreach (GameObject go in gemasButtons)
			Destroy (go);
		gemasButtons.Clear ();

		/*Destroy (figuraGO);
		figura.ClearRunas ();*/

		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
