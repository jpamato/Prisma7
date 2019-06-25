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
    public List<int>[] levelsDone;

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
        StartCoroutine(LoadFile(filepath));
        levelsDone = new List<int>[3];
        for(int i=0;i<levelsDone.Length;i++)
            levelsDone[i] = new List<int>();
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


        Utils.Shuffle<Level>(combinacionesLevels.basico);
        Utils.Shuffle<Level>(combinacionesLevels.intermedio);
        Utils.Shuffle<Level>(combinacionesLevels.avanzado);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public Level GetLevel(){
        int dlevel = Data.Instance.userData.actualWorld;
        Debug.Log(levelsDone[0]);
        if (dlevel == 1) {
            while(levelsDone[0].Contains(currentLevel[0])&&levelsDone[0].Count<combinacionesLevels.basico.Length) {
                currentLevel[0]++;
                if (currentLevel[0] >= combinacionesLevels.basico.Length)
                    currentLevel[0] = 0;
            }
			return combinacionesLevels.basico [currentLevel [0]];
		} else if (dlevel == 2) {
            while (levelsDone[1].Contains(currentLevel[1]) && levelsDone[1].Count < combinacionesLevels.intermedio.Length) {
                currentLevel[1]++;
                if (currentLevel[1] >= combinacionesLevels.intermedio.Length)
                    currentLevel[1] = 0;
            }
            return combinacionesLevels.intermedio [currentLevel [1]];
        } else if (dlevel == 3) {
            while (levelsDone[2].Contains(currentLevel[2]) && levelsDone[2].Count < combinacionesLevels.avanzado.Length) {
                currentLevel[2]++;
                if (currentLevel[2] >= combinacionesLevels.avanzado.Length)
                    currentLevel[2] = 0;
            }
            return combinacionesLevels.avanzado [currentLevel [2]];
        } else {
            return null;
        }
    }

	public void AddCurrentLevel(){
		int dlevel = Data.Instance.userData.actualWorld;
		if (dlevel == 1) {
            levelsDone[0].Add(currentLevel[0]);
            currentLevel [0]++;
			if (currentLevel [0] >= combinacionesLevels.basico.Length)
				currentLevel [0] = 0;
		} else if (dlevel == 2){
            levelsDone[1].Add(currentLevel[1]);
            currentLevel [1]++;
			if (currentLevel [1] >= combinacionesLevels.intermedio.Length)
				currentLevel [1] = 0;
        } else if (dlevel == 3) {
            levelsDone[2].Add(currentLevel[2]);
            currentLevel [2]++;
			if (currentLevel [2] >= combinacionesLevels.avanzado.Length)
				currentLevel [2] = 0;
		}
	}
}
