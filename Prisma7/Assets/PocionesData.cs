using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class PocionesData : MonoBehaviour {

	public List<Ingrediente> ingredientes;
	public int[] currentLevel;
    public List<int>[] levelsDone;

    public PocionesLevels pocionesLevels;

	public string filename = "pocionesLevels.json";

	[Serializable]
	public class Ingrediente{
		public string name;
		public Sprite sprite;
	}

	[Serializable]
	public class PocionesLevels{
		public Level[] basico;
		public Level[] intermedio;
		public Level[] avanzado;
	}

	[Serializable]
	public class Level{
		public int[] elementos;
		public int[] pistas_elementos_index;
		public float factor;
		public int[] respuestas_index;
		public int[] respuestas;
	}

	[Serializable]
	public class Valores{
		public int val;
		public int id;
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
	
		pocionesLevels = JsonUtility.FromJson<PocionesLevels>(text);

        Utils.Shuffle<Level>(pocionesLevels.basico);
        Utils.Shuffle<Level>(pocionesLevels.intermedio);
        Utils.Shuffle<Level>(pocionesLevels.avanzado);

    }

	public Level GetLevel(){

        int dlevel = Data.Instance.userData.actualWorld;
        Debug.Log(levelsDone[0]);
        if (dlevel == 1) {
            while (levelsDone[0].Contains(currentLevel[0]) && levelsDone[0].Count < pocionesLevels.basico.Length) {
                currentLevel[0]++;
                if (currentLevel[0] >= pocionesLevels.basico.Length)
                    currentLevel[0] = 0;
            }
            return pocionesLevels.basico[currentLevel[0]];
        } else if (dlevel == 2) {
            while (levelsDone[1].Contains(currentLevel[1]) && levelsDone[1].Count < pocionesLevels.intermedio.Length) {
                currentLevel[1]++;
                if (currentLevel[1] >= pocionesLevels.intermedio.Length)
                    currentLevel[1] = 0;
            }
            return pocionesLevels.intermedio[currentLevel[1]];
        } else if (dlevel == 3) {
            while (levelsDone[2].Contains(currentLevel[2]) && levelsDone[2].Count < pocionesLevels.avanzado.Length) {
                currentLevel[2]++;
                if (currentLevel[2] >= pocionesLevels.avanzado.Length)
                    currentLevel[2] = 0;
            }
            return pocionesLevels.avanzado[currentLevel[2]];
        } else {
            return null;
        }       
    }

	public void AddCurrentLevel(){
        int dlevel = Data.Instance.userData.actualWorld;
        if (dlevel == 1) {
            levelsDone[0].Add(currentLevel[0]);
            currentLevel [0]++;
			if (currentLevel [0] >= pocionesLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel == 2) {
            levelsDone[1].Add(currentLevel[1]);
            currentLevel [1]++;
			if (currentLevel [1] >= pocionesLevels.intermedio.Length)
				currentLevel [1] = 0;
		}else if (dlevel == 3){
            levelsDone[2].Add(currentLevel[2]);
            currentLevel [2]++;
			if (currentLevel [2] >= pocionesLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}
}
