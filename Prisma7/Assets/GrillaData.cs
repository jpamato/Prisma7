using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GrillaData : MonoBehaviour {

	public int[] currentLevel;
    public List<int>[] levelsDone;
    public GrillaLevels grillaLevels;

    public ActiveState overActiveState;
    public enum ActiveState {
        off,
        active,
        inactive
    }

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
        levelsDone = new List<int>[3];
        for (int i = 0; i < levelsDone.Length; i++)
            levelsDone[i] = new List<int>();
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

        Utils.Shuffle<Level>(grillaLevels.basico);
        Utils.Shuffle<Level>(grillaLevels.intermedio);
        Utils.Shuffle<Level>(grillaLevels.avanzado);
    }

	public Level GetLevel(){

        int dlevel = Data.Instance.userData.actualWorld;
        Debug.Log(levelsDone[0]);
        if (dlevel == 1) {
            while (levelsDone[0].Contains(currentLevel[0]) && levelsDone[0].Count < grillaLevels.basico.Length) {
                currentLevel[0]++;
                if (currentLevel[0] >= grillaLevels.basico.Length)
                    currentLevel[0] = 0;
            }
            return grillaLevels.basico[currentLevel[0]];
        } else if (dlevel == 2) {
            while (levelsDone[1].Contains(currentLevel[1]) && levelsDone[1].Count < grillaLevels.intermedio.Length) {
                currentLevel[1]++;
                if (currentLevel[1] >= grillaLevels.intermedio.Length)
                    currentLevel[1] = 0;
            }
            return grillaLevels.intermedio[currentLevel[1]];
        } else if (dlevel == 3) {
            while (levelsDone[2].Contains(currentLevel[2]) && levelsDone[2].Count < grillaLevels.avanzado.Length) {
                currentLevel[2]++;
                if (currentLevel[2] >= grillaLevels.avanzado.Length)
                    currentLevel[2] = 0;
            }
            return grillaLevels.avanzado[currentLevel[2]];
        } else {
            return null;
        }      
    }

	public void AddCurrentLevel(){
        int dlevel = Data.Instance.userData.actualWorld;
        if (dlevel == 1) {
            levelsDone[0].Add(currentLevel[0]);
            currentLevel [0]++;
			if (currentLevel [0] >= grillaLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel == 2) {
            levelsDone[1].Add(currentLevel[1]);
            currentLevel [1]++;
			if (currentLevel [1] >= grillaLevels.intermedio.Length)
				currentLevel [1] = 0;
        } else if (dlevel == 3) {
            levelsDone[2].Add(currentLevel[2]);
            currentLevel [2]++;
			if (currentLevel [2] >= grillaLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}
}
