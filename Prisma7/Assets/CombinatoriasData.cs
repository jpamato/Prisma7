using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CombinatoriasData : MonoBehaviour {

	public List<Gema> gemas;
	public CombinacionesLevels combinacionesLevels;
	public int[] currentLevel;

	public string filename = "combinacionesLevels.json";

	[Serializable]
	public class CombinacionesLevels{
		public Level[] basico;
		public Level[] intermedio;
		public Level[] avanzado;
	}

	[Serializable]
	public class Gema{
		public Sprite sprite;
		public int size;
	}

	[Serializable]
	public class Level{
		public int[] valores;
		public int[] incluidos;
		public int combinaciones;
		public int resultado;
	}


	// Use this for initialization
	void Awake () {
		string filepath = Path.Combine (Application.streamingAssetsPath + "/", filename);
		StartCoroutine (LoadFile (filepath));
	}

	IEnumerator LoadFile(string filePath) {
		string text = "";

		if (filePath.Contains("://")) {
			using (WWW www = new WWW(filePath))
			{
				yield return www;
				text = www.text;
			}
		} else
			text = System.IO.File.ReadAllText(filePath);

		combinacionesLevels = JsonUtility.FromJson<CombinacionesLevels>(text);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Level GetLevel(){
		int dlevel = Data.Instance.levelsData.actualDiamondLevel;
		if (dlevel < 2) {
			return combinacionesLevels.basico [currentLevel [0]];
		} else if (dlevel < 4) {
			return combinacionesLevels.intermedio [currentLevel [1]];
		} else {
			return combinacionesLevels.avanzado [currentLevel [2]];
		}
	}

	public void AddCurrentLevel(){
		int dlevel = Data.Instance.levelsData.actualDiamondLevel;
		if (dlevel < 2) {
			currentLevel [0]++;
			if (currentLevel [0] >= combinacionesLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel < 4) {
			currentLevel [1]++;
			if (currentLevel [1] >= combinacionesLevels.intermedio.Length)
				currentLevel [1] = 0;
		} else {
			currentLevel [2]++;
			if (currentLevel [2] >= combinacionesLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}
}
