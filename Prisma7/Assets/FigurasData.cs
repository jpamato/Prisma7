using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FigurasData : MonoBehaviour {

	public List<Runa> runas;
	public List<Figura> figuras;

	public int[] currentLevel;
	public FigurasLevels figurasLevels;
	public string filename = "figurasLevels.json";

	// Use this for initialization
	void Start () {
		foreach (Runa r in runas) {
			string s = PlayerPrefs.GetString (r.go.name);
			r.enabled = s == "done"?true:false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Serializable]
	public class FigurasLevels{
		public Level[] basico;
		public Level[] intermedio;
		public Level[] avanzado;
	}

	[Serializable]
	public class Level{
		public Figura figura;
		public List<GameObject> opciones;
		public int time;
		public bool done;
	}

	[Serializable]
	public class Runa{
		public GameObject go;
		public bool enabled;
	}

	[Serializable]
	public class Figura{
		public GameObject go;
		public List<Runa> runas;
		public bool done;

		public bool CheckRuna(string n){
			done = true;
			foreach(Runa r in runas){
				if (r.go.name == n)
					r.enabled = true;
				if (!r.enabled)
					done = false;
			}
			return done;
		}

		public void ClearRunas(){
			foreach(Runa r in runas)
				r.enabled = false;
		}
	}

	public Runa GetRandomRuna(){
		return runas [UnityEngine.Random.Range (0,runas.Count)];
	}

	public Runa GetRuna(string s){
		return runas.Find(x => x.go.name == s);
	}

	public void ResetFiguresDone(){
		foreach (Figura f in figuras)
			f.done = false;
	}

	public Level GetLevel(){
		int dlevel = Data.Instance.levelsData.actualDiamondLevel;
		if (dlevel < 2) {
			return figurasLevels.basico [currentLevel [0]];
		} else if (dlevel < 4) {
			return figurasLevels.intermedio [currentLevel [1]];
		} else {
			return figurasLevels.avanzado [currentLevel [2]];
		}
	}

	public void AddCurrentLevel(){
		int dlevel = Data.Instance.levelsData.actualDiamondLevel;
		if (dlevel < 2) {
			currentLevel [0]++;
			if (currentLevel [0] >= figurasLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel < 4) {
			currentLevel [1]++;
			if (currentLevel [1] >= figurasLevels.intermedio.Length)
				currentLevel [1] = 0;
		} else {
			currentLevel [2]++;
			if (currentLevel [2] >= figurasLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}

	public void EnableRuna(string name){
		Runa r = runas.Find (x => x.go.name == name);
		if (r == null)
			return;

		if (!r.enabled) {
			r.enabled = true;
			Debug.Log (name);
			PlayerPrefs.SetString (name, "done");
			Events.OnRunaFound ();
		}
	}
}
