using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GrillaData : MonoBehaviour {

	public int[] currentLevel;
	public GrillaLevels grillaLevels;

	public string filename = "grillaLevels.json";

	[Serializable]
	public class GrillaLevels{
		public Level[] basico;
		public Level[] intermedio;
		public Level[] avanzado;
	}

	[Serializable]
	public class Level{
		public string consigna;
		public int area;
		public int columnas;
		public int filas;
		public Vector2 size;
		public Vector2 filled;
		public int time;
		public LevelType levelType;
	}

	public enum LevelType
	{
		tipo1=1,
		tipo2=2,
		tipo3=3
	}

	// Use this for initialization
	void Start () {
		string filepath = Path.Combine (Application.streamingAssetsPath + "/", filename);
		StartCoroutine (LoadFile (filepath));
	}
	
	// Update is called once per frame
	void Update () {
		
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

		grillaLevels = JsonUtility.FromJson<GrillaLevels>(text);

	}

	public Level GetLevel(){
		int dlevel = Data.Instance.levelsData.actualDiamondLevel;
		if (dlevel < 3) {
			return grillaLevels.basico [currentLevel [0]];
		} else if (dlevel < 5) {
			return grillaLevels.intermedio [currentLevel [1]];
		} else {
			return grillaLevels.avanzado [currentLevel [2]];
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
