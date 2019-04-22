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
		public int[] size;
		public int[] filled;
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
	void Awake () {
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
        int dlevel = Data.Instance.userData.actualWorld;
        if (dlevel == 1) {
			return grillaLevels.basico [currentLevel [0]];
		} else if (dlevel == 2) {
			return grillaLevels.intermedio [currentLevel [1]];
        }else if (dlevel == 2){
            return grillaLevels.avanzado [currentLevel [2]];
        } else {
            return null;
        }
    }

	public void AddCurrentLevel(){
        int dlevel = Data.Instance.userData.actualWorld;
        if (dlevel == 1) {
			currentLevel [0]++;
			if (currentLevel [0] >= grillaLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel == 2) {
			currentLevel [1]++;
			if (currentLevel [1] >= grillaLevels.intermedio.Length)
				currentLevel [1] = 0;
        } else if (dlevel == 3) {
            currentLevel [2]++;
			if (currentLevel [2] >= grillaLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}
}
