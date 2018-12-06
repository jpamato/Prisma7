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
	void Start () {
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
		if (dlevel < 3) {
			return combinacionesLevels.basico [currentLevel [0]];
		} else if (dlevel < 5) {
			return combinacionesLevels.intermedio [currentLevel [1]];
		} else {
			return combinacionesLevels.avanzado [currentLevel [2]];
		}
	}

	public void AddCurrentLevel(){
		int dlevel = Data.Instance.levelsData.actualDiamondLevel;
		if (dlevel < 3) {
			currentLevel [0]++;
		} else if (dlevel < 5) {
			currentLevel [1]++;
		} else {
			currentLevel [2]++;
		}
	}
}
