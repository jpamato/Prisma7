using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocionesGame : MateGame {

	public int partidaGames;
	int gamesPlayeds;

	List<GameObject> slots;
	public GameObject ingredienteItem;
	public GameObject ingredienteSlot;
	public Transform inventarioContent;
	public Transform centralContent;
	public GameObject receta;
	public ParticleSystem ingredientFx;
	public Animator caldero;

	public PocionesData.Level pLevelData;

	public AudioClip combiOK,combiDone;
	AudioSource audioSource;

	List<PocionesData.Ingrediente> ingredientes;
	List<PocionesData.Valores> valores;
	List<PocionesData.Valores> valoresParciales;

	// Use this for initialization
	void Start () {	
		audioSource = GetComponent<AudioSource> ();
		Data.Instance.inputManager.raycastUI = true;
		levelBarStep = 1f / times2FullBar;
		Events.OnTimeOver += TimeOver;
		Events.DroppedUI += DroppedUI;
		Events.OnDropingOut += OnDropingOut;
		slots = new List<GameObject> ();
		//Invoke ("Init", 0.1f);
		caldero.Play("idle");
		Init();
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
		int pLevel = Data.Instance.pocionesData.currentLevel;
		pLevelData = Data.Instance.pocionesData.pocionesLevels[pLevel];
		valores = new List<PocionesData.Valores> ();
		valoresParciales = new List<PocionesData.Valores> ();
		foreach (PocionesData.Valores v in pLevelData.valores)
			valores.Add (v);
		ingredientes = new List<PocionesData.Ingrediente> ();
		foreach(PocionesData.Ingrediente g in Data.Instance.pocionesData.ingredientes)
			ingredientes.Add(g);

		Utils.Shuffle (valores);
		Utils.Shuffle (ingredientes);

		consigna.SetActive (true);
		ConsignaCombinatoria cs = consigna.GetComponent<ConsignaCombinatoria> ();
		cs.texto.text = "Para un buen color lograr,\nestos elementos balancear\n";

		receta.SetActive (true);
		RecetaPociones rp = receta.GetComponent<RecetaPociones> ();
		rp.texto.text = "SIGUIENTE PREPARACIÓN\n";

		for (int i = 0; i < valores.Count; i++) {	
			PocionesData.Valores v = new PocionesData.Valores ();
			v.id = valores [i].id;
			valoresParciales.Add(v);
			if (i < pLevelData.slots) {
				GameObject slot = Instantiate (ingredienteSlot);
				slot.transform.SetParent (inventarioContent);
				slot.transform.localPosition = Vector3.zero;
				slot.transform.localScale = Vector3.one;
				slot.transform.SetAsLastSibling ();
				slots.Add (slot);
				//CombinatoriasData.Gema g = ingredientes.Find (x => x.size == i);
				GameObject gi = Instantiate (ingredienteItem);
				gi.transform.SetParent (slot.transform);
				gi.transform.localPosition = Vector3.zero;
				gi.transform.localScale = Vector3.one;
				//gi.transform.SetAsLastSibling();
				IngredienteItem gis = gi.GetComponent<IngredienteItem> ();
				gis.image.sprite = ingredientes [i].sprite;
				//gis.text.text = ingredientes[i].name;
				gis.id = valores [i].id;
				rp.texto.text += ingredientes [i].name + ": "+valoresParciales[i].val+"\n";
				cs.texto.text += ""+valores [i].val+" "+ingredientes [i].name+"\n";
			} else {
				Color c = Data.Instance.levelsData.GetNextLevel ().color;
				rp.texto.text += ingredientes [i].name+": <color="+Utils.rgb2Hex(c.r,c.g,c.b)+"><b><size=30>"+(valores [i].val/pLevelData.fraccion)+"</size></b></color>\n";
				cs.texto.text += "<color="+Utils.rgb2Hex(c.r,c.g,c.b)+"><b>"+valores [i].val+"</b></color> "+ingredientes [i].name+"\n";
			}

		}
	}

	void UpdateReceta(bool reset){
		RecetaPociones rp = receta.GetComponent<RecetaPociones> ();
		rp.texto.text = "SIGUIENTE PREPARACIÓN\n";
		for (int i = 0; i < valores.Count; i++) {
			if (reset)
				valoresParciales [i].val = 0;
			if (i < pLevelData.slots) {								
				rp.texto.text += ingredientes [i].name + ": "+valoresParciales[i].val+"\n";
			} else {
				Color c = Data.Instance.levelsData.GetNextLevel ().color;
				rp.texto.text += ingredientes [i].name+": <color="+Utils.rgb2Hex(c.r,c.g,c.b)+"><b><size=30>"+(valores [i].val/pLevelData.fraccion)+"</size></b></color>\n";
			}
		}
	}

	public void Reiniciar(){
		UpdateReceta (true);
	}

	public void Mezclar(){
		bool done = true;
		for (int i = 0; i < pLevelData.slots; i++) {
			if (valoresParciales [i].val * pLevelData.fraccion != valores [i].val) {
				i = valores.Count;
				done = false;
			}
		}
		if (done)
			PocionComplete ();
		else
			PocionFail ();

	}

	void OnDropingOut(){
		Invoke ("ResetSuma",0.1f);
	}

	void DroppedUI(GameObject dragged){
		ingredientFx.Play ();
		IngredienteItem ii = dragged.GetComponent<IngredienteItem> ();
		PocionesData.Valores v = valoresParciales.Find (x => x.id == ii.id);
		v.val++;
		UpdateReceta (false);
	}

	void PocionFail(){
		Debug.Log ("fail");
		caldero.Play("lose");
	}

	void PocionComplete(){
		caldero.Play("win");
		audioSource.PlayOneShot (combiDone);
		Data.Instance.ui.HideTimer ();
		//Events.FiguraComplete (figura.go.name);
		Events.OnMathGameComplete ();
		Data.Instance.levelsData.actualLevelPercent += levelBarStep;
		//colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);
		Data.Instance.ui.colorBar.SetValue (Data.Instance.levelsData.actualLevelPercent);

		foreach (GameObject go in slots)
			Destroy (go);
		slots.Clear ();
		//Destroy (figuraGO);
		if (Data.Instance.levelsData.actualLevelPercent >= 1f) {
			colorDoneSign.SetActive (true);
			Events.OnColorComplete ();
			//Data.Instance.figurasData.ResetFiguresDone ();
			Invoke ("BackToWorld", 3);
		} else {
			doneSign.SetActive (true);
			Data.Instance.pocionesData.currentLevel++;
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
		foreach (GameObject go in slots)
			Destroy (go);
		slots.Clear ();
		/*Destroy (figuraGO);
		figura.ClearRunas ();*/

		Invoke ("BackToWorld", 3);
		//Invoke ("Init", 3);
	}

	void BackToWorld(){
		Data.Instance.LoadScene ("World");
	}
}
